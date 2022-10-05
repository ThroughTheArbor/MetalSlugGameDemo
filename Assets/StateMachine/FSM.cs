using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType
{
    Idle, Patrol, Chase, React, Attack, Hit, Death
}
//挂上这个脚本要注意的事情
//加一个collider设为trigger作为视野
//射击攻击范围（原点attackPoint/也是子弹发出去的位置 半径attackArea）与其碰撞的layer（Player）
//
//相对应的动画名 Idle, Patrol, Chase, React, Attack, Hit, Death
[Serializable]
public class Parameter
{
    public int enemyType;//0是空中飞机，1是空中普通人形敌人；2是地上坦克，3是地面人形敌人
    public int health;
    public static int hittedNum;//这个在玩家用不同子弹时设置不同的值，调用时Parameter.hittedNum
    public float idleTime;
    public float moveSpeed;
    public float attackSpeed;
    public GameObject BulletPrefab;
    public Transform transform;//获取FSM当前挂的物体的transform    
    public Transform target;
    public Transform attackPoint;
    public float attackArea;
    public LayerMask targetLayer;    
    internal Animator animator;
    internal SpriteRenderer spriteRenderer;
    internal bool getHit;
    internal bool dead;
}
public class FSM : MonoBehaviour
{

    private IState currentState;
    private Dictionary<StateType, IState> states = new Dictionary<StateType, IState>();

    public Parameter parameter;
    private void Awake()
    {
        parameter.animator = transform.GetComponent<Animator>();//这里放在Start里面，会因为初始化顺序问题在parameter.animator.Play("Idle");这里报错
    }
    void Start()
    {
        parameter.transform = GetComponent<Transform>();
        parameter.spriteRenderer = GetComponent<SpriteRenderer>();  
        states.Add(StateType.Idle, new IdleState(this));
        //states.Add(StateType.Patrol, new PatrolState(this));
        states.Add(StateType.Chase, new ChaseState(this));
        states.Add(StateType.React, new ReactState(this));
        states.Add(StateType.Attack, new AttackState(this));
        states.Add(StateType.Hit, new HitState(this));
        states.Add(StateType.Death, new DeathState(this));

        TransitionState(StateType.Idle);

        
    }

    void Update()
    {
        currentState.OnUpdate();
        
    }

    public void TransitionState(StateType type)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }            
        currentState = states[type];
        currentState.OnEnter();
    }

    //这个针对的图像是朝左的;如果图像超又，两个if里换一换
    //如果想要实现里面所有的组件都改变朝向，就要用rotation
    public void FlipTo(Transform target)
    {
        if (target != null)
        {
            if (transform.position.x > target.position.x)
            {
                //transform.localScale = new Vector3(1, 1, 1);
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (transform.position.x < target.position.x)
            {
                //transform.localScale = new Vector3(-1, 1, 1);
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    //玩家进入敌人的trigger视线里
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            parameter.target = other.transform;
            print("玩家进入敌人的trigger视线里");
        }
    }

    //玩家离开视线范围
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            parameter.target = null;
            print("玩家离开敌人的trigger视线里");
        }
    }

    //enemy的攻击范围
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(parameter.attackPoint.position, parameter.attackArea);
    }
    public void OnDestroy()
    {
        Destroy(gameObject);
    }
}