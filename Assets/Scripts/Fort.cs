using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fort : MonoBehaviour
{
    public GameObject SkyCollider;
    public GameObject Environment;
    public float EnvironmentMoveSpeed;
    //��̨�ƶ�
    Vector2 position;
    Vector3 center=new Vector3(0,0,0);
    Vector3 direction =new Vector3(0,1,0);
    Vector3 right = new Vector3(1, 0, 0);
    Vector3 left = new Vector3(-1, 0, 0);
    public int moveNum;//��Ϊ�ң���Ϊ��
    SpriteRenderer spriteRenderer;
    Animator animator;
    public static bool isLevelOne = false;
    // Start is called before the first frame update
    void Start()
    {
        position = Environment.transform.position;
        //center = transform.position;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isLevelOne = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (isLevelOne)
        {
            //���������ƶ�
            position.x = position.x - EnvironmentMoveSpeed*0.01f;
            Environment.transform.position = position;
            //���ҿ�����̨
            //A��B��
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (direction!=right)
                {
                    //Χ��ԭ��˳ʱ����ת22.5��
                    direction = Quaternion.AngleAxis(11.25f, Vector3.up) * (direction - center) + center;
                    moveNum++;
                    print("������ת��");
                }
                
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (direction != left)
                {
                    //Χ��ԭ��˳ʱ����ת22.5��
                    direction = Quaternion.AngleAxis(-11.25f, Vector3.up) * (direction - center) + center;
                    moveNum--;
                    print("������ת��");
                }

            }
            //��̨��������
            if(moveNum >= 0)
            {
                spriteRenderer.flipX = false;
                animator.Play("Fort"+moveNum);
            }
            else
            {
                spriteRenderer.flipX = true;
                int n = Mathf.Abs(moveNum);
                print("����ȡ����ֵ���n"+n);
                animator.Play("Fort" + n);
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        walk run = collision.GetComponent<walk>();
        if(run != null)
        {
            print("��ʼ��һ��");
            //����������ϸ���
            //��ʱȫ����������
            //�л���һ������ͷ
            //������wasd���ad
            run.enabled = false;
            //fire����Ϊ��̨�����ڵ�//�ڵ����ñ�ը������Bullet�ű���һ�����
            SkyCollider.SetActive(false);
            //��Ҳ��ű������ǵĶ���
            //Environment��ʼ�����ƶ�
            isLevelOne = true;

        }
    }
}
