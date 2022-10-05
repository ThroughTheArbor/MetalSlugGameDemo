using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHurtZone : MonoBehaviour
{
    //public float timeInvincible = 1.50f;    
    //float invincibleTimer;
    //bool isInvincible = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        
        Damageable da = collision.GetComponent<Damageable>();
        if (da != null)
        {
            da.GetHitted();
            /*if (!isInvincible)
            {
                enemyController.parameter.getHit = true;
                isInvincible = true;
                invincibleTimer = timeInvincible;
            }*/
        }

        //Debug.Log("与子弹发生碰撞的是" + collision.gameObject);

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {/*
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer <= 0)
            {
                isInvincible = false;
            }
        }*/
    }
}
