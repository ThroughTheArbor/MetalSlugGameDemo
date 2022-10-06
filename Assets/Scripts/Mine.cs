using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();
            if (playerController != null)
            {
                animator.enabled = true;
                animator.Play("GroundBomb");
                playerController.ChangeHealth(-3);
            }
            //���������Զ�����
        }
        if (collision.CompareTag("PlayerBullet"))
        {
            print("������һ������");
            animator.enabled = true;
            animator.Play("GroundBombOnLand");
        }

    }

    public void Damage()
    {
        Destroy(gameObject);
    }
}
