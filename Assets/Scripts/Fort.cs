using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Fort : MonoBehaviour
{
    public GameObject SkyCollider;
    public GameObject Environment;
    public float EnvironmentMoveSpeed;
    //炮台移动
    Vector2 position;
    Vector3 center;
    Vector3 Fdirection;
    Vector3 begin = new Vector3(0, 1, 0);
    Vector3 right = new Vector3(1, 0, 0);
    Vector3 left = new Vector3(-1, 0, 0);
    public int moveNum;//正为右，负为左
    public float fortMoveSpeed;//一个角度
    Transform transform;//炮台的
    Transform transformP;//玩家的
    float alpha;
    float arcAngle;
    float horizontal;
    SpriteRenderer spriteRenderer;
    Animator animator;
    //关于玩家物体的挂载
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
    //子弹
    public GameObject bullet;
    //关卡敌人生成器
    //一开始都为false,完成上一部分到下一部分再设置为true
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
        //关卡初始化
    }

    // Update is called once per frame
    void Update()
    {

        if (isLevelOne)
        {
            transformP.rotation = Quaternion.Euler(0, 0, 0);
            //环境向左移动
            position.x = position.x - EnvironmentMoveSpeed * 0.01f;
            Environment.transform.position = position;
            //左右控制炮台
            #region  连续式
            /*
            //连续式
            horizontal = Input.GetAxis("Horizontal");
            
            //A左B右
            if (Input.GetKey(KeyCode.D))
            {
                
                if (Fdirection.y>0)
                {
                    //Fdirection = Quaternion.AngleAxis(fortMoveSpeed * 0.5f, Vector3.up) * (Fdirection - center) + center;
                    //Fdirection = Quaternion.Euler(0, fortMoveSpeed * 0.5f, 0) * Vector3.up;
                    
                    arcAngle = alpha * 3.14f / 180.0f;
                    Fdirection = new Vector3(Fdirection.x * Mathf.Cos(arcAngle) + Fdirection.y * Mathf.Sin(arcAngle), Fdirection.x * Mathf.Sin(arcAngle) - Fdirection.y * Mathf.Cos(arcAngle), 0);

                    print("向右旋转了");
                    print(Fdirection.x + "," + Fdirection.y+","+Fdirection.z);
                }
                
                

            }
            if (Input.GetKey(KeyCode.A))
            {

                if (Fdirection.y > 0)
                {
                    //Fdirection = Quaternion.AngleAxis(fortMoveSpeed * 1f, Vector3.up) * (Fdirection - center) + center;
                    //Fdirection = Quaternion.Euler(0, -fortMoveSpeed * 0.5f, 0) * Vector3.up;
                    arcAngle = alpha* 3.14f / 180.0f;//换成弧度制
                    Fdirection = new Vector3(Fdirection.x * Mathf.Cos(arcAngle) - Fdirection.y * Mathf.Sin(arcAngle), Fdirection.x * Mathf.Sin(arcAngle) + Fdirection.y * Mathf.Cos(arcAngle), 0);

                    print("向左旋转了");
                    print(Fdirection.x + "," + Fdirection.y + "," + Fdirection.z);

                }
            

            }*/
            #endregion //
            if (Input.GetKeyDown(KeyCode.D))
            {
                //断点式旋转

                if (moveNum < 8)
                {
                    //围绕原点顺时针旋转22.5度
                    Fdirection = Quaternion.AngleAxis(11.25f, Vector3.up) * (Fdirection - center) + center;
                    //arcAngle = fortMoveSpeed * 3.14f / 180.0f;//换成弧度制
                    //Fdirection = new Vector3(Fdirection.x * Mathf.Cos(arcAngle) - Fdirection.y * Mathf.Sin(arcAngle), Fdirection.x * Mathf.Sin(arcAngle) + Fdirection.y * Mathf.Cos(arcAngle), 0);

                    moveNum++;
                    //print("向右旋转了");
                    //print(Fdirection.x + "," + Fdirection.y + "," + Fdirection.z);
                }
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                //断点式旋转

                if (moveNum > -8)
                {
                    //围绕原点顺时针旋转22.5度
                    Fdirection = Quaternion.AngleAxis(-11.25f, Vector3.up) * (Fdirection - center) + center;
                    //arcAngle = fortMoveSpeed * 3.14f / 180.0f;
                    //Fdirection = new Vector3(Fdirection.x * Mathf.Cos(arcAngle) + Fdirection.y * Mathf.Sin(arcAngle), Fdirection.x * Mathf.Sin(arcAngle) - Fdirection.y * Mathf.Cos(arcAngle), 0);

                    moveNum--;
                    //print("向左旋转了");
                    //print(Fdirection.x + "," + Fdirection.y + "," + Fdirection.z);
                }
            }

            //断点式旋转

            //炮台动画朝右
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
            #region 连续式
            /*
             //连续式
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
            //开火
            if (Input.GetKeyDown(KeyCode.Space))
            {
                
                arcAngle = (11.25f * moveNum * 3.14f) / 180.0f;
                Fdirection = new Vector3(begin.x * Mathf.Cos(arcAngle) + begin.y * Mathf.Sin(arcAngle), begin.y * Mathf.Cos(arcAngle) - begin.x * Mathf.Sin(arcAngle), 0);

                //子弹只与enemy发生碰撞，设置好它的layer
                GameObject prohectfileObject = Instantiate(bullet, transform.position + Fdirection * 1.0f, Quaternion.identity);
                print("变之前的rotation" + prohectfileObject.GetComponent<Transform>().rotation);
                prohectfileObject.GetComponent<Transform>().localEulerAngles = Fdirection;
                print("变之后的rotation" + prohectfileObject.GetComponent<Transform>().rotation);

                //Transform pt = prohectfileObject.GetComponent<Transform>();                
                //pt.rotation = Quaternion.LookRotation(Vector3.up,Fdirection);//这一行代码会让物体消失
                //projectile.targetPosition = transform.position + Fdirection * 30;
                //projectile.isDG = true;
                Bullet projectile = prohectfileObject.GetComponent<Bullet>();
                Bullet.isFort = true;
                projectile.Launch(Fdirection, 300);

            }
            //按空格键跳跃
            if (Input.GetKeyDown(KeyCode.J))
            {
                SpriteRendererP.enabled = false;
                Up.SetActive(true);
                Down.SetActive(true);
                Vector3 vector3 = new Vector3(-21.82f, -1.76f, 1);
                playerController.ChangePlayerPosition(vector3);
                rb.constraints -= RigidbodyConstraints2D.FreezePositionY;
                rb.isKinematic = false;
                //changeGravity在player的jump里改
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
    //连续式
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
                print("开始第一关");
                //可以在天空上
                //此时全程重力在线
                //玩家播放背朝我们的动画
                playerController = collision.GetComponent<PlayerController>();
                animatorP = collision.GetComponent<Animator>();
                SpriteRendererP = collision.GetComponent<SpriteRenderer>();
                SpriteRendererP.enabled = true;//第二关要改回来
                rb = collision.GetComponent<Rigidbody2D>();
                transformP = collision.GetComponent<Transform>();
                //transformP.rotation = Quaternion.identity;
                //transformP.rotation = Quaternion.Euler(0, 0, 0);
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                rb.isKinematic = true;//第二关要改回来
                //之前的fire键禁用 //第二关要改回来//跳跃键不用改回来
                fire = collision.GetComponent<Fire>();
                fire.enabled = false;
                run.enabled = false;//第二关要改回来//跳跃键不用改回来
                Up.SetActive(false);//第二关要改回来
                Down.SetActive(false);//第二关要改回来
                //ccollider.SetActive(false);
                //第二关不用改回来，第一关到第二关的间隙要改
                playerController.ChangeGravity(0);
                Vector3 vector3 = new Vector3(-21.77f, -1.84f, 1);
                playerController.ChangePlayerPosition(vector3);
                //第二关要改回来
                animatorP.enabled = true;
                //切换下一个摄像头//第二关要改回来
                vcCamera.SetActive(false);

                CameraScale.CameraSize = 3.0f;


                //fire键改为炮台发射炮弹//炮弹不用爆炸，放在Bullet脚本里一起管理
                //第二关要改回来，第一关到第二关的间隙不用改
                SkyCollider.SetActive(false);
                //第二关要改回来
                //Environment开始左右移动//按键方式改变
                isLevelOne = true;
                //isFirstIn = false;

                //BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
                //boxCollider.enabled = false;
                //第二关要改回来
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
            //changeGravity在player的jump里改
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
