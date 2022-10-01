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
            //BulletAttention.SetActive(true);//������ܷ������Ϊû���ӵ�ʱ����Ӧ�ò����κε�CogBullet,Ҳ��û��CogBullet����Ľű�������������BulletAttention
            print("û��������޷�����");
        }

    }
    public ParticleSystem hitEffect;

    /*
    public void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyController enemyController = collision.collider.GetComponent<EnemyController>();
        if (enemyController != null)
        {
            EnemyController.hitTimer = 0.3f;
            //print("Ͷ������hitTimer=" + EnemyController.hitTimer);
            enemyController.Hitted();
            //print("Ͷ������hitTimer=" + EnemyController.hitTimer);
            //enemyController.Die();

        }
        Debug.Log("���ɫ���ַ�����ײ����" + collision.gameObject);

        Destroy(gameObject);
        Instantiate(hitEffect, transform.position, Quaternion.identity);
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.GetComponent<PlayerController>();
        CogBulletNum += 6;
        //GetComponent<AudioSource>().Play();//�������ʧ����
        Destroy(gameObject);
        print("���ڵ��ӵ�����" + CogBulletNum);        
    }
}
