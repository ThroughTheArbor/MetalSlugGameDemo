using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public static int CogBulletNum;    
    
    // Start is called before the first frame update
    private void Start()
    {
        

    }
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.magnitude >= 50)
        {
            Destroy(gameObject);
        }
    }
    internal void Launch(Vector2 direction, float force)
    {
        if (CogBulletNum > 0)
        {
            rigidbody2d.AddForce(direction * force);
            CogBulletNum--;
        }
        else
        {            
            print("没有零件，无法发射");
        }

    }
    public ParticleSystem hitEffect;

    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyController enemyController = collision.collider.GetComponent<EnemyController>();
        if (enemyController != null)
        {          
            enemyController.Hitted(1);          

        }
        Debug.Log("与子弹发生碰撞的是" + collision.gameObject);

        Destroy(gameObject);
        Instantiate(hitEffect, transform.position, Quaternion.identity);
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.GetComponent<PlayerController>();
        CogBulletNum += 6;        
        Destroy(gameObject);
        print("现在的子弹数是" + CogBulletNum);        
    }*/
}
