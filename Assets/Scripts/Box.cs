using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        GetComponent<Rigidbody2D>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //OnCollisionEnter2D和OnTriggerEnter2D有先后，trigger先，collider后，用collider时，bullet里的ytigger已经把gameobject删了，所以在collider里获取不到
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("PlayerBullet"))
        {
            print("销毁了一个木桶");
            animator.enabled = true;
            animator.Play("Box");
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("PlayerBullet"))
        {
            print("销毁了一个木桶");
            animator.enabled = true;
            animator.Play("GroundBombOnLand");
        }

    }
    public void Damage()
    {
        Destroy(gameObject);
    }
}
