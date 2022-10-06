using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hostage : MonoBehaviour
{
    Animator animator;
    bool isHelped;
    bool isRewarding;
    public float runSpeed;
    
    Rigidbody2D rigidbody2d;    
    Vector2 position;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //transform = GetComponent<Transform>();

        rigidbody2d = GetComponent<Rigidbody2D>();
        position = transform.position;        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("PlayerBullet"))
        {
            
            animator.Play("Run");
            isHelped = true;
        }
        if (isHelped)
        {
            if (collision.CompareTag("Player"))
            {
                isRewarding = true;
                animator.Play("Reward");
                PlayerController playerController = collision.GetComponent<PlayerController>();
                playerController.ChangeBulletNum(5);
            }
        }

    }
    private void FixedUpdate()
    {
       
        if (isHelped&&!isRewarding)
        {
            position.x = position.x - runSpeed;
            rigidbody2d.MovePosition(position);
            if (transform.position.magnitude >= 50)
            {
                Destroy(gameObject);
            }
        }
    }
    public void RewardEnd()
    {
        isRewarding = false;
    }
}
