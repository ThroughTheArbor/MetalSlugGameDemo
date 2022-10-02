using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHurtZone : MonoBehaviour
{
    public float timeInvincible = 1.50f;    
    float invincibleTimer;

    void OnTriggerEnter2D(Collider2D collision)
    {
        //�Ƿ���EnemyController
        EnemyController enemyController = collision.GetComponent<EnemyController>();
        if (enemyController != null)
        {
                enemyController.Hitted(1);             
                
                invincibleTimer = timeInvincible;
            
        }
        else
        {
            print("δ��õ���");
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
