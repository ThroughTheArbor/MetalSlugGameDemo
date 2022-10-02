using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    internal class Player
    {        
        internal int maxHealth = 3;
        internal int currentHealth = 3;
        internal int bulletNum;
    }
    Vector2 lookDirection = new Vector2(1, 0);
    Vector2 move;
    public GameObject Up;
    public GameObject Down;
    Animator animatorUp;
    Animator animatorDown;
    public Rigidbody2D rigidbody2DPlayer;
    public static bool isWalking;
    public bool faceRight = true;
    //�����ж����
    bool isHandGun;
    bool isBox;
    bool isHandGrenade;
    //Box����˴�����triger
    public GameObject BoxLeft;
    public GameObject BoxRight;
    //Box��ʱ��
    float boxTimer;
    //����
    public GameObject HandGrenade;


    void Update()
    {
        //�ƶ�ʱ���ж�
        move = new Vector2(walk.horizontal, walk.vertical);        
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            //������һ��
            lookDirection.Normalize();
            animatorUp.SetFloat("Look X", move.x);
            animatorDown.SetFloat("Look X", move.x);
            animatorUp.SetFloat("Look Y", move.y);
            animatorDown.SetFloat("Look Y", move.y);
            animatorUp.SetFloat("Speed", move.magnitude);
            animatorDown.SetFloat("Speed", move.magnitude);
            isWalking = true;
            if (move.x > 0)
            {
                faceRight = true;
            }
            if(move.x < 0)
            {
                faceRight = false;
            }
        }
        else
        {
            isWalking = false;
        }
        //�����ж�
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            isHandGun = true;
            animatorUp.SetBool("isHandGun", true);
            isHandGrenade = false;
            animatorUp.SetBool("isHandGrenade", false);
            isBox = false;
            animatorUp.SetBool("isBox", false);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            isHandGun = false;
            animatorUp.SetBool("isHandGun", false);
            isHandGrenade = false;
            animatorUp.SetBool("isHandGrenade", false);
            isBox = true;
            animatorUp.SetBool("isBox", true);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            isHandGun = false;
            animatorUp.SetBool("isHandGun", false);
            isHandGrenade = true;
            animatorUp.SetBool("isHandGrenade", true);
            isBox = false;
            animatorUp.SetBool("isBox", false);
        }
        //�ո������
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LaunchPlayer();
        }
        //boxʱ���ж�
        if (boxTimer > 0)
        {
            boxTimer -= Time.deltaTime;
            if (boxTimer < 0)
            {
                BoxLeft.SetActive(false);
                BoxRight.SetActive(false);
                print("ȭ������");
            }
        }


        //ui������Ͻ���
        if (endTimer > 0)
        {
            endTimer -= Time.deltaTime;
            if (endTimer <= 0)
            {
                Debug.Log("ʧ���˳���Ϸ");
                Application.Quit();
                //UnityEditor.EditorApplication.isPlaying = false;
            }
        }
        
    }
    
    internal void Start()
    {        
        animatorDown = Down.GetComponent<Animator>();
        animatorUp = Up.GetComponent<Animator>();
        rigidbody2DPlayer = GetComponent<Rigidbody2D>();
        Debug.Log("��ȡ��rigidbudy2d");
        currentHealth = 5;
        Bullet.CogBulletNum = 10;
        //HealthBar.instance.SetHealth(currentHealth / (float)maxHealth);
        
    }
   
    public int health
    {
        get { return currentHealth; }
        //set { currentHealth = value; }//��һ�仰Ū���Ͳ������ȡֵ�������//ֻ���ڷ����ﱻ����
    }
    private int currentHealth;    
    float endTimer;
    int dieNum;
    //public AttentionControll attentionControll;
    internal void ChangeHealth2(int acount)
    {

        if (acount < 0)
        {

            if (currentHealth <= 0)
            {
                if (dieNum <= 0)
                {
                    //attentionControll.RubyDie();
                    endTimer = 3.0f;
                }
                dieNum++;
                return;
            }

            animatorUp.SetTrigger("Hit");
        }
        currentHealth = currentHealth + acount;
        //HealthBar.instance.SetHealth(currentHealth / (float)maxHealth);
    }
    internal double ChangeHealth(double heal, double a)
    {
        double health = 67;
        double m;
        float b = 0;
        heal = heal + a;//���healΪ��ֵ�Ͳ���Ѫ����Ѫƿ��������Ʒ���Ĵ����ϣ���������

        //��һ�ְ汾��currenthealth�ı��ܷ���������ֵֻ��int i1,û�����ֵ���ڼ��ݮ�Ľű�����Ϊһ������ʹ��
        //���磺health= rubyController.ChangeHealth(health, 841);��ΪrubyController.ChangeHealth(1);
        //int i1 = 3 ;
        //currentHealth = currentHealth + i1;

        if (a != 841 && a != 1682)
        {
            Debug.Log("�˺��쳣");
        } //�˺��쳣�ж�
        for (int i = 1; i < 6; i++)
        {
            double x = (heal - health) / 841;
            if (x == i)
            {
                b = 3;
            }
        }//�˺��쳣�ж�
        if (b != 3)
        {
            Debug.Log("�˺��쳣");
        }//�˺��쳣�ж�
        return heal;
        m = (heal - health) / 841;

    }

    public GameObject projectileLeft;
    public GameObject projectileRight;
    float attentionTimer;
   
    
    void LaunchPlayer()
    {
        if (isHandGun) { 
        //��������ʾbullet
        if (Bullet.CogBulletNum > 0) { 
            
            if (faceRight)
        {
            GameObject prohectfileObject = Instantiate(projectileRight, rigidbody2DPlayer.position + Vector2.right * 0.3f, Quaternion.identity);
            Bullet projectile = prohectfileObject.GetComponent<Bullet>();
            animatorUp.SetTrigger("Fire");
            projectile.Launch(lookDirection, 300);
        }
        else
        {
            GameObject prohectfileObject = Instantiate(projectileLeft, rigidbody2DPlayer.position + Vector2.left * 0.3f, Quaternion.identity);
            Bullet projectile = prohectfileObject.GetComponent<Bullet>();
            animatorUp.SetTrigger("Fire");
            projectile.Launch(lookDirection, 300);
        }
        }
        else
        {            
            print("û��������޷�����");
        }
        }
        if (isBox)
        {
            //active TRUEһ��ʱ��󣬱�Ϊfalse
            if (faceRight)
            {
                BoxRight.SetActive(true);
                animatorUp.SetTrigger("Fire");
                boxTimer = 0.5f;
                print("����ȭ����ʼ");
            }
            else
            {
                BoxLeft.SetActive(true);
                animatorUp.SetTrigger("Fire");
                boxTimer = 0.5f;
                print("����ȭ����ʼ");
            }

        }
        if (isHandGrenade)
        {
            animatorUp.SetTrigger("Fire");
            if (faceRight)
            {
                GameObject prohectfileObject = Instantiate(HandGrenade, rigidbody2DPlayer.position + Vector2.right * 0.3f, Quaternion.identity);
                HandGrenade projectile = prohectfileObject.GetComponent<HandGrenade>();
                projectile.Launch(lookDirection);
                
            }
            else
            {
                GameObject prohectfileObject = Instantiate(HandGrenade, rigidbody2DPlayer.position + Vector2.left * 0.3f, Quaternion.identity);
                HandGrenade projectile = prohectfileObject.GetComponent<HandGrenade>();
                projectile.Launch(lookDirection);
            }
        }
    }
}
