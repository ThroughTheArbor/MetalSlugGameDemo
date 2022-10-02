using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public class Enemy
    {
        public string name;
        public int maxHealth;
        public int currentHealth;
        public float enemySpeed;
        public bool isHitted;
    }    
    public Enemy enemy;
    void Start()
    {
        enemy.maxHealth = 3;
        enemy.currentHealth = enemy.maxHealth;
    }


    void Update()
    {

    }

    internal void Hitted(int hitNum)
    {
        enemy.isHitted = true;
        enemy.currentHealth-= hitNum;
        print("敌人扣血了"+ hitNum);
        print("现在敌人的血量是" + enemy.currentHealth);
        if (enemy.currentHealth <= 0)
        {
            Die();
        }

    }   
    public float dieTimer;
    public static bool dieEnd = false;    
    void Die()
    {
        //rigidbody2d.simulated = false;
        //animator.SetTrigger("Die");
    }
    private void FixedUpdate()
    {

    }


}
