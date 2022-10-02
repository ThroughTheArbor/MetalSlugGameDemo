using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGrenade : MonoBehaviour
{
    public GameObject hurtZone;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //�������֮ǰ
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("��������ײ");
        GameObject go = collision.gameObject;
        print(go.name);
        if (go.CompareTag("floor"))
        {
            print("�����˵ذ�");
            hurtZone.SetActive(true);
            animator.SetTrigger("Fire");
        }
    }
    //����hurtZone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyController enemyController = collision.GetComponent<EnemyController>();
        if (enemyController != null)
        {
            enemyController.Hitted(1);
        }
        else
        {
            print("δ��õ���");
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
            print("û��������޷�����");
        }
    }
}
