using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMushroom : MonoBehaviour
{
    private bool isDie = false;
    private Animator animator;
    private int dir = -1;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if(isDie){
            return;
        }

        transform.Translate(transform.right * Time.deltaTime * 0.8f * dir);
    }

    private void OnCollisionEnter2D(Collision2D collision2D) {
        if(collision2D.gameObject.tag != "Ground"){
            dir = -1*dir;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision2D) {
        //触发者为Player
        if(collision2D.gameObject.tag == "Mario"){
            //1.播放死亡动画,设置标志位
            animator.SetTrigger("Die");
            isDie = true;
            // 往下偏移一些，为了配合死亡动画
            transform.position = new Vector3(transform.position.x,transform.position.y-0.0387F, 0);
            //2.删除所有box collision2D
            BoxCollider2D[] boxCollider2Ds = GetComponents<BoxCollider2D>();
            foreach(var boxCollider2D in boxCollider2Ds){
                Destroy(boxCollider2D);
            }
            //3.给mario一个向上的力
            collision2D.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 300);
            //4.销毁rigidBody
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(gameObject, 1F);
        }
    }
}
