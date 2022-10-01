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

    void Update()
    {
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

        //空格键发射
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LaunchPlayer();
        }
        
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
        animatorDown = Down.GetComponent<Animator>();
        animatorUp = Up.GetComponent<Animator>();
        rigidbody2DPlayer = GetComponent<Rigidbody2D>();
        Debug.Log("获取了rigidbudy2d");
        currentHealth = 5;
        Bullet.CogBulletNum = 10;
        //HealthBar.instance.SetHealth(currentHealth / (float)maxHealth);
        
    }
   
    public int health
    {
        get { return currentHealth; }
        //set { currentHealth = value; }//这一句话弄掉就不能随便取值做外挂了//只能在方法里被调用
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
        heal = heal + a;//如果heal为满值就不加血，在血瓶或其他物品处的代码上，不在这里

        //另一种版本的currenthealth的保密方法，输入值只有int i1,没有输出值，在捡草莓的脚本里作为一个方法使用
        //例如：health= rubyController.ChangeHealth(health, 841);变为rubyController.ChangeHealth(1);
        //int i1 = 3 ;
        //currentHealth = currentHealth + i1;

        if (a != 841 && a != 1682)
        {
            Debug.Log("账号异常");
        } //账号异常判定
        for (int i = 1; i < 6; i++)
        {
            double x = (heal - health) / 841;
            if (x == i)
            {
                b = 3;
            }
        }//账号异常判定
        if (b != 3)
        {
            Debug.Log("账号异常");
        }//账号异常判定
        return heal;
        m = (heal - health) / 841;

    }

    public GameObject projectileLeft;
    public GameObject projectileRight;
    float attentionTimer;
   
    
    void LaunchPlayer()
    {
        //用数字显示bullet
        if (Bullet.CogBulletNum > 0) { 
            
            if (faceRight)
        {
            GameObject prohectfileObject = Instantiate(projectileRight, rigidbody2DPlayer.position + Vector2.right * 0.3f, Quaternion.identity);
            Bullet projectile = prohectfileObject.GetComponent<Bullet>();
            animatorUp.SetTrigger("HandGun");
            projectile.Launch(lookDirection, 300);
        }
        else
        {
            GameObject prohectfileObject = Instantiate(projectileLeft, rigidbody2DPlayer.position + Vector2.left * 0.3f, Quaternion.identity);
            Bullet projectile = prohectfileObject.GetComponent<Bullet>();
            animatorUp.SetTrigger("HandGun");
            projectile.Launch(lookDirection, 300);
        }
        }
        else
        {            
            print("没有零件，无法发射");
        }


    }
}
