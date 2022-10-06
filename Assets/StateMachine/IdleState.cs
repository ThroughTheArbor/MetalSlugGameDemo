using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IdleState : IState
{
    private FSM manager;
    private Parameter parameter;

    private float timer;
    public IdleState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        Debug.Log("进入idle");
        if (parameter.enemyType == 0)
        {
            //要缓慢移动到posy=0.96的位置//或者设置它的gravityScale
            //然后进入攻击状态
            //parameter.rigidbody2.gravityScale = 0.7;
            PlaneRunControll.isDown = true;            
        }
        else
        {
            parameter.animator.Play("Idle");
        }        
    }

    public void OnUpdate()
    {
        timer += Time.deltaTime;

        if (parameter.getHit)
        {
            manager.TransitionState(StateType.Hit);
        }
        if(parameter.enemyType == 0)
        {
            if (parameter.transform.position.y<=0.96f)
            {
                //parameter.rigidbody2.gravityScale = 0;
                PlaneRunControll.isDown = false;
                manager.TransitionState(StateType.Attack);
            }
        }
        else
        {
            if (parameter.target != null)
            {
                if (parameter.enemyType == 2)
                {
                    manager.TransitionState(StateType.Attack);
                }
                else
                {
                    //manager.TransitionState(StateType.React);
                    manager.TransitionState(StateType.Chase);
                }
                
            }
        }
        
        
        if (timer >= parameter.idleTime)
        {
            timer = 0;
            //manager.TransitionState(StateType.Chase);
            //游走
            //manager.TransitionState(StateType.Patrol);
        }
    }

    public void OnExit()
    {
        timer = 0;
    }
}
#region  PatrolState
/*
public class PatrolState : IState
{
    private FSM manager;
    private Parameter parameter;

    private int patrolPosition;
    public PatrolState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("Patrol");
    }

    public void OnUpdate()
    {
        manager.FlipTo(parameter.patrolPoints[patrolPosition]);

        manager.transform.position = Vector2.MoveTowards(manager.transform.position,
            parameter.patrolPoints[patrolPosition].position, parameter.moveSpeed * Time.deltaTime);

        if (parameter.getHit)
        {
            manager.TransitionState(StateType.Hit);
        }
        if (parameter.target != null &&
            parameter.target.position.x >= parameter.chasePoints[0].position.x &&
            parameter.target.position.x <= parameter.chasePoints[1].position.x)
        {
            manager.TransitionState(StateType.React);
        }
        if (Vector2.Distance(manager.transform.position, parameter.patrolPoints[patrolPosition].position) < .1f)
        {
            manager.TransitionState(StateType.Idle);
        }
    }

    public void OnExit()
    {
        patrolPosition++;

        if (patrolPosition >= parameter.patrolPoints.Length)
        {
            patrolPosition = 0;
        }
    }
}*/
#endregion
public class ChaseState : IState
{
    private FSM manager;
    private Parameter parameter;

    public ChaseState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        Debug.Log("进入chase");
        if (parameter.enemyType == 0||parameter.enemyType==2)
        {
            
                manager.TransitionState(StateType.Attack);            
        }
        else
        {
            parameter.animator.Play("Chase");
        }
    }

    public void OnUpdate()
    {
        if (parameter.getHit)
        {
            manager.TransitionState(StateType.Hit);
        }
        if (parameter.enemyType == 0)
        {
            
        }
        else
        {
            manager.FlipTo(parameter.target);
            if (parameter.target)
            {
                manager.transform.position = Vector2.MoveTowards(manager.transform.position,
                parameter.target.position, parameter.moveSpeed * Time.deltaTime);
            }
        }        
        
        if (parameter.enemyType == 1) { 
        if(parameter.target != null && parameter.target.position.y >= parameter.transform.position.y)
        {
            parameter.animator.Play("AttackDown");
        }
        }
        if (parameter.target == null )
        {
            manager.TransitionState(StateType.Idle);
        }
        if (Physics2D.OverlapCircle(parameter.attackPoint.position, parameter.attackArea, parameter.targetLayer))
        {
            manager.TransitionState(StateType.Attack);
        }
    }

    public void OnExit()
    {

    }
}

public class ReactState : IState
{
    private FSM manager;
    private Parameter parameter;

    private AnimatorStateInfo info;
    public ReactState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("React");
    }

    public void OnUpdate()
    {
        info = parameter.animator.GetCurrentAnimatorStateInfo(0);

        if (parameter.getHit)
        {
            manager.TransitionState(StateType.Hit);
        }
        if (info.normalizedTime >= .95f)
        {
            manager.TransitionState(StateType.Chase);
        }
    }

    public void OnExit()
    {

    }
}

public class AttackState : IState
{
    private FSM manager;
    private Parameter parameter;
    private bool isAttackDone;
    Vector2 pos1;
    Vector2 tar1;
    private AnimatorStateInfo info;
    public Transform bulletCloneT;
    float timer;
    public AttackState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {        
        Debug.Log("进入attack");
        timer = parameter.attackSpeed;
        if (parameter.enemyType == 0)
        {

        }
        else
        {
            parameter.animator.Play("Attack");
        }            
        //发射子弹

        /*
            //飞机发射子弹//这个是实现子弹出仓，到与player同一个y值后再发射//已经实现player在炮台上的跳跃闪避
            GameObject bullet = Bullet.InstantiateBullet(parameter.BulletPrefab, parameter.attackPoint.transform);
            Bullet bulletCS = bullet.GetComponent<Bullet>();
            bulletCS.isEnemy = true;
            pos1 = new Vector2(parameter.attackPoint.transform.position.x, parameter.attackPoint.transform.position.y);
            tar1 = new Vector2(parameter.attackPoint.transform.position.x, parameter.target.position.y);
            bullet.transform.position = Vector2.MoveTowards(pos1,tar1, parameter.moveSpeed * Time.deltaTime);
            //是否在执行完上一行再执行下一行？
            bulletCloneT = bullet.GetComponent<Transform>();
            //Vector2 vector2 = new Vector2(parameter.target.position.x, parameter.target.position.y);
            if (parameter.target.position.x <= parameter.transform.position.x)
            {
                Vector2 vector2 = new Vector2(0, -1);
                bulletCS.Launch(vector2, 300);
            }
            else
            {
                Vector2 vector2 = new Vector2(0, 1);
                bulletCS.Launch(vector2, 300);
            }
            Debug.Log("飞机发射完了一次子弹");*/
        
            if (parameter.target != null) { 
            //普通发射子弹
            GameObject bullet = Bullet.InstantiateBullet(parameter.BulletPrefab, parameter.attackPoint.transform);
            Bullet bulletCS = bullet.GetComponent<Bullet>();
            bulletCS.isEnemy = true;
            if (parameter.enemyType == 3) { 
            if (parameter.target.position.x <= parameter.transform.position.x)
            {
                Vector2 vector2 = new Vector2(-1,0);
                bulletCS.Launch(vector2, 500);
            }
            else
            {
                Vector2 vector2 = new Vector2(1, 0);
                bulletCS.Launch(vector2, 500);
            }
                }else if (parameter.enemyType==1||parameter.enemyType==0)
                {
                    Vector2 vector2 = new Vector2(parameter.target.position.x - parameter.transform.position.x, parameter.target.position.y - parameter.transform.position.y);
                    bulletCS.Launch(vector2, 500);
                }else if(parameter.enemyType == 2)
            {
                Vector2 vector2 = new Vector2(-1, 0);
                bulletCS.Launch(vector2, 500);
            }
            }
            //Vector2 vector2 = new Vector2(parameter.target.position.x, parameter.target.position.y);
            //bulletCS.Launch(vector2, 300);            
        
    }
    
    public void OnUpdate()
    {
        info = parameter.animator.GetCurrentAnimatorStateInfo(0);
        if (timer<=0)
        {
            isAttackDone = true;
        }
        timer-=Time.deltaTime;
        if (parameter.getHit)
        {
            manager.TransitionState(StateType.Hit);
        }

        if (parameter.enemyType == 0)
        {
            
        }
        if (parameter.enemyType == 1)
        {
            if (parameter.target != null && parameter.target.position.y >= parameter.transform.position.y)
            {
                parameter.animator.Play("AttackDown");
            }
        }
        if (isAttackDone && info.normalizedTime >= .95f)
        {
            manager.TransitionState(StateType.Chase);
        }
    }

    public void OnExit()
    {
        isAttackDone=false;
    }
}

public class HitState : IState
{
    private FSM manager;
    private Parameter parameter;

    private AnimatorStateInfo info;
    public HitState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        
        Debug.Log("进入hit");
        //parameter.animator.Play("Hit");
        if (parameter.enemyType == 2)
        {
            //获取所有的子物体的spritrrenderer
            manager.GetChildColorRed();
        }
        parameter.spriteRenderer.DOColor(Color.red, 0.2f).SetLoops(2, LoopType.Yoyo).ChangeStartValue(Color.white);
        parameter.health -= Parameter.hittedNum;
        if (parameter.health <= 0)
        {
            manager.TransitionState(StateType.Death);
        }
    }

    public void OnUpdate()
    {
        info = parameter.animator.GetCurrentAnimatorStateInfo(0);

        
        if (info.normalizedTime >= .95f)
        {
            parameter.target = GameObject.FindWithTag("Player").transform;

            manager.TransitionState(StateType.Chase);
        }
    }

    public void OnExit()
    {
        parameter.getHit = false;
    }
}

public class DeathState : IState
{
    private FSM manager;
    private Parameter parameter;
    private AnimatorStateInfo info;
    float timer;
    float fadeDuration = 0.03f;
    
    public DeathState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        Debug.Log("进入death");
        parameter.animator.Play("Death");
        BoxCollider2D boxCollider2 = parameter.enemy.GetComponent<BoxCollider2D>();
        if (boxCollider2 != null)
        {
            boxCollider2.enabled = false;
        }        
        if (parameter.enemyType == 1)
        {
            GameObject parachute = parameter.transform.Find("parachute").gameObject;
            parameter.transform.rotation = Quaternion.Euler(0, 0, -30);
            
        }
        parameter.dead = true;
        if (parameter.enemyType == 0)
        {
            //旋转
            //加速坠落            
            PlaneRunControll.isDown = true;
            parameter.transform.rotation = Quaternion.Euler(0, 0, 30);
        }
        
    }

    public void OnUpdate()
    {       
        
            timer += Time.deltaTime;
            //parameter.parachuteCanvasGroup.alpha = 1 - (timer / fadeDuration);
        parameter.parachuteObject.SetActive(false);
        if(parameter.enemyType == 1|| parameter.enemyType == 0)
        {
            parameter.rigidbody2.gravityScale = 0.7f;
        }
        info = parameter.animator.GetCurrentAnimatorStateInfo(0);
        if(parameter.enemyType == 0)
        {
            if (info.normalizedTime >= .95f && timer >= 3.0f)
            {
                manager.OnDestroy();
            }
        }
        else
        {
            if (info.normalizedTime >= .95f && timer >= 0.7f)
            {
                manager.OnDestroy();
            }
        }
        
    }

    public void OnExit()
    {

    }
}