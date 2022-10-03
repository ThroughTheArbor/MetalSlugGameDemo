using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fort : MonoBehaviour
{
    public GameObject SkyCollider;
    public GameObject Environment;
    public float EnvironmentMoveSpeed;
    //炮台移动
    Vector2 position;
    Vector3 center=new Vector3(0,0,0);
    Vector3 direction =new Vector3(0,1,0);
    Vector3 right = new Vector3(1, 0, 0);
    Vector3 left = new Vector3(-1, 0, 0);
    public int moveNum;//正为右，负为左
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
            //环境向左移动
            position.x = position.x - EnvironmentMoveSpeed*0.01f;
            Environment.transform.position = position;
            //左右控制炮台
            //A左B右
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (direction!=right)
                {
                    //围绕原点顺时针旋转22.5度
                    direction = Quaternion.AngleAxis(11.25f, Vector3.up) * (direction - center) + center;
                    moveNum++;
                    print("向右旋转了");
                }
                
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (direction != left)
                {
                    //围绕原点顺时针旋转22.5度
                    direction = Quaternion.AngleAxis(-11.25f, Vector3.up) * (direction - center) + center;
                    moveNum--;
                    print("向左旋转了");
                }

            }
            //炮台动画朝右
            if(moveNum >= 0)
            {
                spriteRenderer.flipX = false;
                animator.Play("Fort"+moveNum);
            }
            else
            {
                spriteRenderer.flipX = true;
                int n = Mathf.Abs(moveNum);
                print("这是取绝对值后的n"+n);
                animator.Play("Fort" + n);
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        walk run = collision.GetComponent<walk>();
        if(run != null)
        {
            print("开始第一关");
            //可以在天空上搞事
            //此时全程重力在线
            //切换下一个摄像头
            //操作由wasd变成ad
            run.enabled = false;
            //fire键改为炮台发射炮弹//炮弹不用爆炸，放在Bullet脚本里一起管理
            SkyCollider.SetActive(false);
            //玩家播放背朝我们的动画
            //Environment开始左右移动
            isLevelOne = true;

        }
    }
}
