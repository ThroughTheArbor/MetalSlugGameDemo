using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    //bulletҪ��Rigidbody2D��trigger
    Rigidbody2D rigidbody2d;
    public static int CogBulletNum;
    public bool isDG=false;
    public  Vector3 targetPosition;
    public float speed =2;
    public static bool isFort;
    public bool isEnemy;//�����ӵ�����
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
        if(isFort || isEnemy)
        {
            rigidbody2d.AddForce(direction * force);
        }
        else
        if(CogBulletNum > 0)
        {
            rigidbody2d.AddForce(direction * force);
            CogBulletNum--;
            print("��ǰ�ӵ�����" + CogBulletNum);
        }
        
        else
        {
            print("û���ӵ��ˣ���ǰ�ӵ�����" + CogBulletNum);
        }

    }
    public static GameObject InstantiateBullet(GameObject bulletPrefab,Transform transform)
    {
        GameObject gameObject = Instantiate(bulletPrefab, transform);
        return gameObject;  
    }

    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (isEnemy)
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();
            if(playerController != null)
            {
                playerController.ChangeHealth(-1);
            }
            Destroy(gameObject);
        }
        else
        {
            Damageable da = collision.GetComponent<Damageable>();
            if (da != null)
            {
                da.GetHitted();
            }
            //Debug.Log("���ӵ�������ײ����" + collision.gameObject);

            Destroy(gameObject);
        }
        
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
