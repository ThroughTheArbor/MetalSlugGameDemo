using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneRunControll : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public float enemySpeed = 0.1f;
    public float downSpeed = 0.1f;
    public float changeDirectionTime=2;
    float timer;
    public bool isVertical = false;
    public static bool isDown = false;
    float direction = 1;
    Vector2 position;
    FSM manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<FSM>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        position = transform.position;
        timer = changeDirectionTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!manager.parameter.dead)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                direction = -direction;//调转方向
                timer = changeDirectionTime;//时间重置
            }
        }
        
    }
    private void FixedUpdate()
    {
        if (!manager.parameter.dead)
        {
            EnemyMove();
        }
           
        if (isDown)
        {
            position.y = position.y - downSpeed;
        }
    }


    void EnemyMove()
    {
        if (isVertical)
        {
            position.y = position.y + enemySpeed * direction;
            rigidbody2d.MovePosition(position);
        }
        else
        {
            position.x = position.x + enemySpeed * direction;
            rigidbody2d.MovePosition(position);
        }
    }
}
