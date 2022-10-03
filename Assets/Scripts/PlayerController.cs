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
    BoxCollider2D collider;
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
    //��Ծ
    public float JumpHeight;
    public GameObject SkyCollider;//���ǽ��
    bool isGround;//�Ƿ��ڵ���
    bool isCar;//�Ƿ��ڳ���
    bool isJumpBegin;

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

        //�Ƿ��ڵ����ж�
        isGround = collider.IsTouchingLayers(LayerMask.GetMask("floor"));
        //��Ծ���ػص���
        if (isGround&&isJumpBegin)
        {
            if (!isCar)
            {
                SkyCollider.SetActive(true);
                rigidbody2DPlayer.gravityScale = 0.0f;
            }

            //SkyCollider.SetActive(true);//�ڳ��ϲ�����Ϊtrue���ڶ��ز�Ҫ��Ϊtrue���ӳ�������ȥ��
            //�ùؿ��������Ƿ�����Ϊ0
            //rigidbody2DPlayer.gravityScale = 0.0f;
            print("����ĳ����");
            isJumpBegin = false;
        }
        //J����Ծ
        if (Input.GetKeyDown(KeyCode.J))
        {
            Jump();
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
        collider = GetComponent<BoxCollider2D>();
        currentHealth = 5;
        Bullet.CogBulletNum = 10;
        //HealthBar.instance.SetHealth(currentHealth / (float)maxHealth);
        
    }
   
    public int health
    {
        get { return currentHealth; }        
    }
    private int currentHealth;    
    float endTimer;
    int dieNum;
    //public AttentionControll attentionControll;
    internal void ChangeHealth(int acount)
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
    //��Ծ����
    void Jump()
    {
        SkyCollider.SetActive(false);
        animatorDown.SetTrigger("Jump");
        rigidbody2DPlayer.gravityScale = 0.7f;
        Vector2 jumpVel = new Vector2(0.0f, JumpHeight);
        rigidbody2DPlayer.velocity = Vector2.up * jumpVel;
        isJumpBegin=true;

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        
        GameObject gameObject = collision.gameObject;
        
        if (gameObject.CompareTag("Car"))
        {
            isCar = true;
        }
    }
}
