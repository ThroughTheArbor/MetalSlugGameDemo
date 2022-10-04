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
    Rigidbody2D rb;
    
    public GameObject vcCamera;
    public static bool isLevelOne = false;
    public bool isFirstIn = true;
    //子弹
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        position = Environment.transform.position;
        //center = transform.position;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isLevelOne = false;
        Fdirection = new Vector3(0,1,0);
        center = new Vector3(0,0,0);
        animator.SetFloat("Pos X", Fdirection.x);
        animator.SetFloat("Pos Y", Fdirection.y);
        transform = GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {

        if (isLevelOne)
        {
            //环境向左移动
            position.x = position.x - EnvironmentMoveSpeed*0.01f;
            Environment.transform.position = position;
            //左右控制炮台

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
            
            if (Input.GetKeyDown(KeyCode.D))
            {
                //断点式旋转
                
                if (moveNum<8)
                {
                    //围绕原点顺时针旋转22.5度
                    Fdirection = Quaternion.AngleAxis(11.25f, Vector3.up) * (Fdirection - center) + center;
                    //arcAngle = fortMoveSpeed * 3.14f / 180.0f;//换成弧度制
                    //Fdirection = new Vector3(Fdirection.x * Mathf.Cos(arcAngle) - Fdirection.y * Mathf.Sin(arcAngle), Fdirection.x * Mathf.Sin(arcAngle) + Fdirection.y * Mathf.Cos(arcAngle), 0);

                    moveNum++;
                    print("向右旋转了");
                    print(Fdirection.x + "," + Fdirection.y + "," + Fdirection.z);
                }
            }
            
            if (Input.GetKeyDown(KeyCode.A))
            {
                //断点式旋转
                
                if (moveNum>-8)
                {
                    //围绕原点顺时针旋转22.5度
                    Fdirection = Quaternion.AngleAxis(-11.25f, Vector3.up) * (Fdirection - center) + center;
                    //arcAngle = fortMoveSpeed * 3.14f / 180.0f;
                    //Fdirection = new Vector3(Fdirection.x * Mathf.Cos(arcAngle) + Fdirection.y * Mathf.Sin(arcAngle), Fdirection.x * Mathf.Sin(arcAngle) - Fdirection.y * Mathf.Cos(arcAngle), 0);

                    moveNum--;
                    print("向左旋转了");
                    print(Fdirection.x + "," + Fdirection.y + "," + Fdirection.z);
                }
            }

            //断点式旋转
            
            //炮台动画朝右
            if(moveNum >= 0)
            {
                spriteRenderer.flipX = false;
                animator.Play("Fort"+moveNum);
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
            
            //开火
            if (Input.GetKeyDown(KeyCode.Space))
            {
                arcAngle = (11.25f * moveNum * 3.14f) / 180.0f;
                Fdirection = new Vector3(begin.x * Mathf.Cos(arcAngle) + begin.y * Mathf.Sin(arcAngle), begin.y * Mathf.Cos(arcAngle) - begin.x * Mathf.Sin(arcAngle),0);

                //子弹只与enemy发生碰撞，设置好它的layer
                GameObject prohectfileObject = Instantiate(bullet, transform.position+Fdirection*1.0f, Quaternion.identity);
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isFirstIn) { 
        walk run = collision.GetComponent<walk>();
        
        if (run != null)
        {
            print("开始第一关");
            //可以在天空上
            //此时全程重力在线
            //玩家播放背朝我们的动画
            playerController = collision.GetComponent<PlayerController>();
            animatorP = collision.GetComponent<Animator>();
            SpriteRendererP = collision.GetComponent<SpriteRenderer>();
            SpriteRendererP.enabled = true;
                rb = collision.GetComponent<Rigidbody2D>();
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            //之前的fire键禁用
            Fire fire = collision.GetComponent<Fire>();
            fire.enabled = false;
            run.enabled = false;
            Up.SetActive(false);
            Down.SetActive(false);
            //ccollider.SetActive(false);
            playerController.ChangeGravity(0);
            Vector3 vector3 = new Vector3(-21.77f,-1.84f,1);
            playerController.ChangePlayerPosition(vector3);
            animatorP.enabled = true;
            //切换下一个摄像头
            vcCamera.SetActive(false);

            CameraScale.CameraSize = 3.0f;
            
            
            //fire键改为炮台发射炮弹//炮弹不用爆炸，放在Bullet脚本里一起管理
            SkyCollider.SetActive(false);            
            //Environment开始左右移动
            isLevelOne = true;
            isFirstIn = false;
                //把这个trigger删除，下次不进入了，也不会和子弹发生碰撞
            BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
            boxCollider.enabled = false;
        }
        }
    }
}
