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
    //OnCollisionEnter2D��OnTriggerEnter2D���Ⱥ�trigger�ȣ�collider����colliderʱ��bullet���ytigger�Ѿ���gameobjectɾ�ˣ�������collider���ȡ����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("PlayerBullet"))
        {
            print("������һ��ľͰ");
            animator.enabled = true;
            animator.Play("Box");
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("PlayerBullet"))
        {
            print("������һ��ľͰ");
            animator.enabled = true;
            animator.Play("GroundBombOnLand");
        }

    }
    public void Damage()
    {
        Destroy(gameObject);
    }
}
