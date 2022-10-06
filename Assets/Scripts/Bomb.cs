using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    Animator animator;
    public GameObject hurtZone;
    //从天而降的炸弹
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        print("发生了碰撞");
        GameObject go = collision.gameObject;
        print(go.name);
        if (go.CompareTag("floor")||go.CompareTag("Player"))
        {            
            hurtZone.SetActive(true);
            //GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Transform>().rotation=Quaternion.Euler(0, 0, 0);
            GetComponent<Transform>().localScale = new Vector3(2, 2, 2);
            animator.enabled = true;
            animator.Play("GroundBomb");            
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.ChangeHealth(-3);
            }
            //动画结束自动销毁
        }
        

    }
    public void Damage()
    {
        Destroy(gameObject);
    }
}
