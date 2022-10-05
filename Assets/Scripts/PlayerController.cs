using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    internal class Player
    {        
        internal int maxHealth ;
        internal int currentHealth ;
        internal int bulletNum;      

    }
    Player player;
    
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
    //武器判定序号
    bool isHandGun;
    bool isBox;
    bool isHandGrenade;
    //Box与敌人触碰的triger
    public GameObject BoxLeft;
    public GameObject BoxRight;
    //Box计时器
    float boxTimer;
    //手雷
    public GameObject HandGrenade;
    //跳跃
    public float JumpHeight;
    public GameObject SkyCollider;//天空墙的
    bool isGround;//是否在地面
    bool isCar;//是否在车上
    bool isJumpBegin;
    //改变玩家位置

    float endTimer;
    int dieNum;
    public GameObject projectileLeft;
    public GameObject projectileRight;
    float attentionTimer;
    void Update()
    {
        
        //子弹实时更新
        Bullet.CogBulletNum= player.bulletNum;
              
        //移动时的判定
        move = new Vector2(walk.horizontal, walk.vertical);        
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            //向量归一化
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
        //武器判定
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
        
        //box时间判定
        if (boxTimer > 0)
        {
            boxTimer -= Time.deltaTime;
            if (boxTimer < 0)
            {
                BoxLeft.SetActive(false);
                BoxRight.SetActive(false);
                print("拳击结束");
            }
        }

        //是否在地面判定
        isGround = collider.IsTouchingLayers(LayerMask.GetMask("floor"));
        //跳跃后重回地面
        if (isGround&&isJumpBegin)
        {
            if (!isCar)
            {
                SkyCollider.SetActive(true);
                rigidbody2DPlayer.gravityScale = 0.0f;
            }

            //SkyCollider.SetActive(true);//在车上不能设为true，第二关才要设为true（从车上跳下去）
            //用关卡来控制是否重力为0
            //rigidbody2DPlayer.gravityScale = 0.0f;
            print("到达某个面");
            isJumpBegin = false;
        }
        //J键跳跃
        if (Input.GetKeyDown(KeyCode.J))
        {
            Jump();
        }

            //ui播放完毕结束
            if (endTimer > 0)
        {
            endTimer -= Time.deltaTime;
            if (endTimer <= 0)
            {
                Debug.Log("失败退出游戏");
                Application.Quit();
                //UnityEditor.EditorApplication.isPlaying = false;
            }
        }
        
    }
    
    internal void Start()
    {        
        
        rigidbody2DPlayer = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        GenerateData();
        animatorUp = Up.GetComponent<Animator>();
        animatorDown = Down.GetComponent<Animator>();
        //Bullet.CogBulletNum = 10;
        //HealthBar.instance.SetHealth(currentHealth / (float)maxHealth);
        
    }
    void GenerateData()
    {
        player = new Player();
        player.maxHealth = 3;
        player.currentHealth = 3;
        player.bulletNum = 10;
    }
    internal void ChangeHealth(int acount)
    {

        if (acount < 0)
        {

            if (player.currentHealth <= 0)
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
        player.currentHealth = player.currentHealth + acount;
        print("玩家血量改变"+acount+"现在血量是"+player.currentHealth);
        //HealthBar.instance.SetHealth(currentHealth / (float)maxHealth);
    }

    public void LaunchPlayer()
    {
        if (isHandGun) { 
        //用数字显示bullet
        if (Bullet.CogBulletNum > 0) {
                Parameter.hittedNum = 1;
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
                print("没有子弹了，当前子弹数是" + Bullet.CogBulletNum);
            }

        }
        if (isBox)
        {
            Parameter.hittedNum = 1;
            //active TRUE一定时间后，变为false
            if (faceRight)
            {
                BoxRight.SetActive(true);
                animatorUp.SetTrigger("Fire");
                boxTimer = 0.5f;
                print("向右拳击开始");
            }
            else
            {
                BoxLeft.SetActive(true);
                animatorUp.SetTrigger("Fire");
                boxTimer = 0.5f;
                print("向左拳击开始");
            }

        }
        if (isHandGrenade)
        {
            Parameter.hittedNum = 2;
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
    //跳跃方法
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
    public void ChangePlayerPosition(Vector3 pos)
    {
        rigidbody2DPlayer.position = pos;
    }
    public void ChangeGravity(float num)
    {
        rigidbody2DPlayer.gravityScale = num;
    }
}
