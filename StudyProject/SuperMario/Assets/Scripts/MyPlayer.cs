using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : MonoBehaviour
{

    private Animator animator;
    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        float h = Input.GetAxis("Horizontal");

        if(h!=0){
            // 移动，方向 * 上一帧花费的时间 * 玩家按键
            transform.Translate(transform.right*Time.deltaTime*h);
            // 如果往右
            if(h>0){
                GetComponent<SpriteRenderer>().flipX = false;
            }else if (h<0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            // 播放跑步动画
            animator.SetBool("Run",true);
        } else {
            animator.SetBool("Run",false);
        }

        // jump
        if(Input.GetKeyDown(KeyCode.W)){
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 180);
            animator.SetBool("Jump",true);
        }else{
            animator.SetBool("Jump",false);
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag=="Mushroom"){
            //1.播放死亡动画
            animator.SetTrigger("Die");
            //2.删除碰撞器和刚体组件
            BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
            foreach(var item in colliders){
                Destroy(item);
            }
            Destroy(GetComponent<Rigidbody2D>());
            //3.一秒后删除游戏物体
            Destroy(gameObject, 1f);
        }
    }
}
