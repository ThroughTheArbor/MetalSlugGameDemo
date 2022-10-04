using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public static int CogBulletNum;
    public bool isDG=false;
    public  Vector3 targetPosition;
    public float speed =2;
    public static bool isFort;
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
    private void FixedUpdate()
    {
        if (isDG)
        {
            rigidbody2d.DOMove(targetPosition, speed).SetSpeedBased();
        }
    }
    internal void Launch(Vector2 direction, float force)
    {
        if (CogBulletNum > 0||isFort)
        {
            rigidbody2d.AddForce(direction * force);
            CogBulletNum--;
        }
        else
        {            
            print("û��������޷�����");
        }

    }
    

    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyController enemyController = collision.GetComponent<EnemyController>();
        if (enemyController != null)
        {          
            enemyController.Hitted(1);          

        }
        Debug.Log("���ӵ�������ײ����" + collision.gameObject);

        Destroy(gameObject);
        
    }
    /*
     //��һ���ӵ�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.GetComponent<PlayerController>();
        CogBulletNum += 6;        
        Destroy(gameObject);
        print("���ڵ��ӵ�����" + CogBulletNum);        
    }*/
}
