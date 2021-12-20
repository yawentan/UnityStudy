using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // 动画组件
    private Animator animator;
    // 是否在地面
    private bool isOnGround = false;

    // 第一帧就开始执行
    void Start()
    {
        // 获取组件
        animator = GetComponent<Animator>();

        // 让音效管理器检查下背景音乐
        AudioManager.Instance.CheckBG();
    }

    // 每帧都执行一次
    void Update()
    {
        // 获取玩家左右按键的值，-1 、0 、1
        float h = Input.GetAxis("Horizontal");
        // 如果不等于0，意味着玩家有移动
        if (h!=0)
        {
            // 移动，方向 * 上一帧花费的时间 * 玩家按键
            transform.Translate(transform.right * Time.deltaTime * h);
            // 如果往右
            if (h > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            // 如果往左
            if (h<0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            // 播放跑步动画
            animator.SetBool("Run", true);

        }
        // 玩家没移动
        else
        {
            // 退出跑步
            animator.SetBool("Run", false);
        }

        // 如果在地面 同时按了W 也就是跳跃
        if (isOnGround&&Input.GetKeyDown(KeyCode.W))
        {
            // 给刚体组件施加一个向上的力
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 180);
            // 让音效管理器播放第2个音效
            AudioManager.Instance.Play(1);
            
        }
    }

    // 碰撞进入则调用
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 如果我碰到的游戏物体 标签是 Ground
        if (collision.gameObject.tag == "Ground")
        {
            // 关闭跳跃动画
            animator.SetBool("Jump", false);
            // 当前在地面
            isOnGround = true;
        }
        // 如果碰到的是敌人
        else if (collision.gameObject.tag == "Enemy")
        {
            // 播放死亡动画
            animator.SetTrigger("Die");
            // 添加一个向上的力
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 100);
            // 销毁碰撞体
            Destroy(GetComponent<CapsuleCollider2D>());
            // 播放第一个游戏音效，并且希望去掉背景音乐
            AudioManager.Instance.Play(0, true);
            // 3.5秒后触发游戏结束
            Invoke("GameOver", 3.5f);

        }
    }
    // 游戏结束
    public void GameOver()
    {
        // 重新加载当前场景
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 碰撞退出则调用
    private void OnCollisionExit2D(Collision2D collision)
    {
        // 如果碰撞离开的游戏物体 标签是 Ground
        if (collision.gameObject.tag == "Ground")
        {
            // 播放跳跃动画
            animator.SetBool("Jump", true);
            // 不在地面
            isOnGround = false;
        }
    }

    // 触发进入则调用
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 如果触发的游戏物体的标签是 Gold
        if (collision.gameObject.tag == "Gold")
        {
            // UI金币数量加1
            UIManager.Instance.GoldNum += 1;
            // 销毁金币
            Destroy(collision.gameObject);
            // 播放第四个游戏音效
            AudioManager.Instance.Play(3);
        }
        // 接触到了胜利检测对象
        else if (collision.gameObject.tag == "WinObject")
        {
            // 得到当前是第几关
            int currLV = SceneManager.GetActiveScene().buildIndex;

            // 当前关卡是最后一关
            if (currLV== SceneManager.sceneCountInBuildSettings - 1)
            {
                // 去第一关
                SceneManager.LoadScene(0);
            }
            else
            {
                // 去下一关
                SceneManager.LoadScene(currLV + 1);
            }
            
        }
    }


}
