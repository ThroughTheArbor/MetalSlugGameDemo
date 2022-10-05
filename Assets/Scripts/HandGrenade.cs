using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGrenade : MonoBehaviour
{
    public float timeInvincible = 1.50f;
    public GameObject hurtZone;
    Animator animator;
    float invincibleTimer;
    bool isInvincible = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer <= 0)
            {
                isInvincible = false;
            }
        }
    }
    //手雷落地之前
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("发生了碰撞");
        GameObject go = collision.gameObject;
        print(go.name);
        if (go.CompareTag("floor"))
        {
            print("碰到了地板");
            hurtZone.SetActive(true);
            animator.SetTrigger("Fire");
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    //进入hurtZone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable da = collision.GetComponent<Damageable>();
        if (da != null)
        {

            if (!isInvincible)
            {
                da.GetHitted();
                isInvincible = true;
                invincibleTimer = timeInvincible;
            }
            
        }
        else
        {
            print("未获得敌人");
        }
        hurtZone.SetActive(false);
    }
    public void Damage()
    {
        Destroy(gameObject);
    }
    internal void Launch(Vector2 lookDirection)
    {
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        if (Bullet.CogBulletNum > 0)
        {
            body.AddForce((lookDirection + Vector2.up * 0.5f) * 200);
            Bullet.CogBulletNum--;
        }
        else
        {
            print("没有手榴弹，无法发射");
        }
    }
}
