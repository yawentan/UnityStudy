using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 动画
    private Animator animator;
    // 方向 1：右边 ，0：左边
    private int dir = 1;
    // 开始时X轴的坐标
    private float startPosX;
    // 是否已经死亡
    private bool isDie = false;

    // 第一帧就开始执行
    void Start()
    {
        // 获取动画组件
        animator = GetComponent<Animator>();
        // 开始时X轴的坐标
        startPosX = transform.position.x;
    }

    // 每帧都执行一次
    void Update()
    {
        // 如果已经死亡
        if (isDie)
        {
            // 什么都不敢，直接跳出Update，后面都不会执行
            return;
        }
        // 到这里说明没死

        // 通过方向位移
        transform.Translate(transform.right * Time.deltaTime * 0.8f * dir);

        // 如果X轴超过了开始坐标X+0.7
        if (transform.position.x> startPosX + 0.7f)
        {
            // 方向取反
            dir = dir * -1;
        }
        // 如果X轴超过了开始坐标X-0.7
        else if (transform.position.x < startPosX - 0.7f)
        {
            // 方向取反
            dir = dir * -1;
        }
    }

    // 碰撞进入，身体部分是碰撞体
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 只要我碰到的不是地面
        if (collision.gameObject.tag!="Ground")
        {
            // 方向取反
            dir = dir * -1;
        }
    }

    // 触发进入，只有头上顶着的是触发器
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 碰到敌人了
        if (collision.gameObject.tag=="Player")
        {
            // 自己播放死亡动画
            animator.SetTrigger("Die");
            // 播放第三个音效
            AudioManager.Instance.Play(2);
            // 往下偏移一些，为了配合死亡动画
            transform.position = new Vector3(transform.position.x,transform.position.y-0.0387F, 0);
            // 已经死亡
            isDie = true;
        
            BoxCollider2D[] collider2Ds;
            // 得到所有的碰撞体
            collider2Ds = GetComponents<BoxCollider2D>();
            // 循环所有碰撞体
            foreach (var item in collider2Ds)
            {
                // 挨个销毁
                Destroy(item);
            }
            // 销毁刚体
            Destroy(GetComponent<Rigidbody2D>());
            // 1秒后销毁自己
            Destroy(gameObject, 1F);
        }
    }
}
