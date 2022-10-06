using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Fort : MonoBehaviour
{
    public GameObject SkyCollider;
    public GameObject Environment;
    public float EnvironmentMoveSpeed;
    //��̨�ƶ�
    Vector2 position;
    Vector3 center;
    Vector3 Fdirection;
    Vector3 begin = new Vector3(0, 1, 0);
    Vector3 right = new Vector3(1, 0, 0);
    Vector3 left = new Vector3(-1, 0, 0);
    public int moveNum;//��Ϊ�ң���Ϊ��
    public float fortMoveSpeed;//һ���Ƕ�
    Transform transform;//��̨��
    Transform transformP;//��ҵ�
    float alpha;
    float arcAngle;
    float horizontal;
    SpriteRenderer spriteRenderer;
    Animator animator;
    //�����������Ĺ���
    PlayerController playerController;
    Animator animatorP;
    SpriteRenderer SpriteRendererP;
    public GameObject Up;
    public GameObject Down;
    public GameObject ccollider;
    public GameObject AirWallForP;
    Rigidbody2D rb;

    public GameObject vcCamera;
    public static bool isLevelOne = false;
    public bool isLevelTwo = false;
    public int InTime = 0;
    //�ӵ�
    public GameObject bullet;
    //�ؿ�����������
    //һ��ʼ��Ϊfalse,�����һ���ֵ���һ����������Ϊtrue
    public GameObject L1P1;
    public GameObject L1P2;
    public GameObject L1P3;
    //public GameObject L1P3;
    Instantiate instantiate1;
    Instantiate instantiate2;
    Instantiate instantiate3;
    public GameObject L2Begin;
    // Start is called before the first frame update
    void Start()
    {
        position = Environment.transform.position;
        //center = transform.position;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isLevelOne = false;
        Fdirection = new Vector3(0, 1, 0);
        center = new Vector3(0, 0, 0);
        animator.SetFloat("Pos X", Fdirection.x);
        animator.SetFloat("Pos Y", Fdirection.y);
        transform = GetComponent<Transform>();
        //�ؿ���ʼ��
    }

    // Update is called once per frame
    void Update()
    {

        if (isLevelOne)
        {
            transformP.rotation = Quaternion.Euler(0, 0, 0);
            //���������ƶ�
            position.x = position.x - EnvironmentMoveSpeed * 0.01f;
            Environment.transform.position = position;
            //���ҿ�����̨
            #region  ����ʽ
            /*
            //����ʽ
            horizontal = Input.GetAxis("Horizontal");
            
            //A��B��
            if (Input.GetKey(KeyCode.D))
            {
                
                if (Fdirection.y>0)
                {
                    //Fdirection = Quaternion.AngleAxis(fortMoveSpeed * 0.5f, Vector3.up) * (Fdirection - center) + center;
                    //Fdirection = Quaternion.Euler(0, fortMoveSpeed * 0.5f, 0) * Vector3.up;
                    
                    arcAngle = alpha * 3.14f / 180.0f;
                    Fdirection = new Vector3(Fdirection.x * Mathf.Cos(arcAngle) + Fdirection.y * Mathf.Sin(arcAngle), Fdirection.x * Mathf.Sin(arcAngle) - Fdirection.y * Mathf.Cos(arcAngle), 0);

                    print("������ת��");
                    print(Fdirection.x + "," + Fdirection.y+","+Fdirection.z);
                }
                
                

            }
            if (Input.GetKey(KeyCode.A))
            {

                if (Fdirection.y > 0)
                {
                    //Fdirection = Quaternion.AngleAxis(fortMoveSpeed * 1f, Vector3.up) * (Fdirection - center) + center;
                    //Fdirection = Quaternion.Euler(0, -fortMoveSpeed * 0.5f, 0) * Vector3.up;
                    arcAngle = alpha* 3.14f / 180.0f;//���ɻ�����
                    Fdirection = new Vector3(Fdirection.x * Mathf.Cos(arcAngle) - Fdirection.y * Mathf.Sin(arcAngle), Fdirection.x * Mathf.Sin(arcAngle) + Fdirection.y * Mathf.Cos(arcAngle), 0);

                    print("������ת��");
                    print(Fdirection.x + "," + Fdirection.y + "," + Fdirection.z);

                }
            

            }*/
            #endregion //
            if (Input.GetKeyDown(KeyCode.D))
            {
                //�ϵ�ʽ��ת

                if (moveNum < 8)
                {
                    //Χ��ԭ��˳ʱ����ת22.5��
                    Fdirection = Quaternion.AngleAxis(11.25f, Vector3.up) * (Fdirection - center) + center;
                    //arcAngle = fortMoveSpeed * 3.14f / 180.0f;//���ɻ�����
                    //Fdirection = new Vector3(Fdirection.x * Mathf.Cos(arcAngle) - Fdirection.y * Mathf.Sin(arcAngle), Fdirection.x * Mathf.Sin(arcAngle) + Fdirection.y * Mathf.Cos(arcAngle), 0);

                    moveNum++;
                    //print("������ת��");
                    //print(Fdirection.x + "," + Fdirection.y + "," + Fdirection.z);
                }
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                //�ϵ�ʽ��ת

                if (moveNum > -8)
                {
                    //Χ��ԭ��˳ʱ����ת22.5��
                    Fdirection = Quaternion.AngleAxis(-11.25f, Vector3.up) * (Fdirection - center) + center;
                    //arcAngle = fortMoveSpeed * 3.14f / 180.0f;
                    //Fdirection = new Vector3(Fdirection.x * Mathf.Cos(arcAngle) + Fdirection.y * Mathf.Sin(arcAngle), Fdirection.x * Mathf.Sin(arcAngle) - Fdirection.y * Mathf.Cos(arcAngle), 0);

                    moveNum--;
                    //print("������ת��");
                    //print(Fdirection.x + "," + Fdirection.y + "," + Fdirection.z);
                }
            }

            //�ϵ�ʽ��ת

            //��̨��������
            if (moveNum >= 0)
            {
                spriteRenderer.flipX = false;
                animator.Play("Fort" + moveNum);
                SpriteRendererP.flipX = true;
                animatorP.Play("Fire" + moveNum);
            }
            else
            {
                spriteRenderer.flipX = true;
                int n = Mathf.Abs(moveNum);
                animator.Play("Fort" + n);
                SpriteRendererP.flipX = false;
                animatorP.Play("Fire" + n);
            }
            #region ����ʽ
            /*
             //����ʽ
            if (Fdirection.x < 0)
            {
                spriteRenderer.flipX = true;
                animator.SetFloat("Pos X", Mathf.Abs(Fdirection.x));
                animator.SetFloat("Pos Y", Mathf.Abs(Fdirection.y));
            }
            if(Fdirection.x >= 0)
            {
                spriteRenderer.flipX = false;
                animator.SetFloat("Pos X", Fdirection.x);
                animator.SetFloat("Pos Y", Fdirection.y);
            }*/
            #endregion 
            //����
            if (Input.GetKeyDown(KeyCode.Space))
            {
                
                arcAngle = (11.25f * moveNum * 3.14f) / 180.0f;
                Fdirection = new Vector3(begin.x * Mathf.Cos(arcAngle) + begin.y * Mathf.Sin(arcAngle), begin.y * Mathf.Cos(arcAngle) - begin.x * Mathf.Sin(arcAngle), 0);

                //�ӵ�ֻ��enemy������ײ�����ú�����layer
                GameObject prohectfileObject = Instantiate(bullet, transform.position + Fdirection * 1.0f, Quaternion.identity);
                print("��֮ǰ��rotation" + prohectfileObject.GetComponent<Transform>().rotation);
                prohectfileObject.GetComponent<Transform>().localEulerAngles = Fdirection;
                print("��֮���rotation" + prohectfileObject.GetComponent<Transform>().rotation);

                //Transform pt = prohectfileObject.GetComponent<Transform>();                
                //pt.rotation = Quaternion.LookRotation(Vector3.up,Fdirection);//��һ�д������������ʧ
                //projectile.targetPosition = transform.position + Fdirection * 30;
                //projectile.isDG = true;
                Bullet projectile = prohectfileObject.GetComponent<Bullet>();
                Bullet.isFort = true;
                projectile.Launch(Fdirection, 300);

            }
            //���ո����Ծ
            if (Input.GetKeyDown(KeyCode.J))
            {
                SpriteRendererP.enabled = false;
                Up.SetActive(true);
                Down.SetActive(true);
                Vector3 vector3 = new Vector3(-21.82f, -1.76f, 1);
                playerController.ChangePlayerPosition(vector3);
                rb.constraints -= RigidbodyConstraints2D.FreezePositionY;
                rb.isKinematic = false;
                //changeGravity��player��jump���
                animatorP.enabled = false;
                playerController.Jump();

            }
            if (instantiate1.isEnd)
            {
                L1P2.SetActive(true);
                instantiate2 = L1P2.GetComponent<Instantiate>();
                if (instantiate2.isEnd)
                {
                    L1P3.SetActive(true);
                    instantiate3 = L1P3.GetComponent<Instantiate>();
                    if (instantiate3.isEnd)
                    {
                        isLevelTwo = true;
                        OnLevelTwo();
                    }
                }
            }
            if (isLevelTwo)
            {
                OnLevelTwo();
            }
        }
        

    }
    /*
    //����ʽ
    void FixedUpdate()
    {
        if (isLevelOne)
        {
            alpha = fortMoveSpeed * horizontal * Time.deltaTime;
        }
    }*/
    Fire fire;
    walk run;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isLevelTwo) { 
        if (InTime == 0)
        {
            run = collision.GetComponent<walk>();

            if (run != null)
            {
                print("��ʼ��һ��");
                //�����������
                //��ʱȫ����������
                //��Ҳ��ű������ǵĶ���
                playerController = collision.GetComponent<PlayerController>();
                animatorP = collision.GetComponent<Animator>();
                SpriteRendererP = collision.GetComponent<SpriteRenderer>();
                SpriteRendererP.enabled = true;//�ڶ���Ҫ�Ļ���
                rb = collision.GetComponent<Rigidbody2D>();
                transformP = collision.GetComponent<Transform>();
                //transformP.rotation = Quaternion.identity;
                //transformP.rotation = Quaternion.Euler(0, 0, 0);
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                rb.isKinematic = true;//�ڶ���Ҫ�Ļ���
                //֮ǰ��fire������ //�ڶ���Ҫ�Ļ���//��Ծ�����øĻ���
                fire = collision.GetComponent<Fire>();
                fire.enabled = false;
                run.enabled = false;//�ڶ���Ҫ�Ļ���//��Ծ�����øĻ���
                Up.SetActive(false);//�ڶ���Ҫ�Ļ���
                Down.SetActive(false);//�ڶ���Ҫ�Ļ���
                //ccollider.SetActive(false);
                //�ڶ��ز��øĻ�������һ�ص��ڶ��صļ�϶Ҫ��
                playerController.ChangeGravity(0);
                Vector3 vector3 = new Vector3(-21.77f, -1.84f, 1);
                playerController.ChangePlayerPosition(vector3);
                //�ڶ���Ҫ�Ļ���
                animatorP.enabled = true;
                //�л���һ������ͷ//�ڶ���Ҫ�Ļ���
                vcCamera.SetActive(false);

                CameraScale.CameraSize = 3.0f;


                //fire����Ϊ��̨�����ڵ�//�ڵ����ñ�ը������Bullet�ű���һ�����
                //�ڶ���Ҫ�Ļ�������һ�ص��ڶ��صļ�϶���ø�
                SkyCollider.SetActive(false);
                //�ڶ���Ҫ�Ļ���
                //Environment��ʼ�����ƶ�//������ʽ�ı�
                isLevelOne = true;
                //isFirstIn = false;

                //BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
                //boxCollider.enabled = false;
                //�ڶ���Ҫ�Ļ���
                AirWallForP.SetActive(true);
                Parameter.hittedNum = 2;
                L1P1.SetActive(true);
                instantiate1 = L1P1.GetComponent<Instantiate>();               

                InTime++;
            }
        }
        else
        {            
            SpriteRendererP.enabled = true;
            Up.SetActive(false );
            Down.SetActive(false );
            //transformP.rotation = Quaternion.Euler(0, 0, 0);
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            rb.isKinematic = true;
            //changeGravity��player��jump���
            animatorP.enabled = true;
            playerController.ChangeGravity(0);
            Vector3 vector3 = new Vector3(-21.77f, -1.84f, 1);
            playerController.ChangePlayerPosition(vector3);
            Parameter.hittedNum = 2;
            InTime++;
        }
        }
    }
    public void OnLevelTwo()
    {
        SpriteRendererP.enabled = false;
        Vector3 vector3 = new Vector3(-21.82f, -1.76f, 1);
        playerController.ChangePlayerPosition(vector3);
        rb.constraints -= RigidbodyConstraints2D.FreezePositionY;
        rb.isKinematic = true;
        fire.enabled = true;
        run.enabled = true;
        Up.SetActive(true);
        Down.SetActive(true);
        playerController.ChangeGravity(1f);
        animatorP.enabled = false;
        vcCamera.SetActive(true );
        isLevelOne = false;
        AirWallForP.SetActive(false );
        L2Begin.SetActive(true);
    }
}
