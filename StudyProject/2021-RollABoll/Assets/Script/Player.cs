using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Rigidbody rd;
    public int score = 0;
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody>();
        scoreText.text = "score:"+score;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        rd.AddForce(new Vector3(h, 0, Input.GetAxis("Vertical")));
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag=="Food"){
            Destroy(other.gameObject);
            score++;
            scoreText.text = "score:"+score;
        }
    }
}
