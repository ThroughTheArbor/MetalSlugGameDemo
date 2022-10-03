using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walk : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    public float speed = 0.1f;
    public static float horizontal;
    public static float vertical;
    void start()
    {
        //获取当前对象的rigidbody
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        // 获取水平输入，按向左，会获得 -1.0 f ; 按向右，会获得 1.0 f       
        horizontal = Input.GetAxis("Horizontal");
        // 获取垂直输入，按向下，会获得 -1.0 f ; 按向上，会获得 1.0 f
        vertical = Input.GetAxis("Vertical");
    }
    //固定时间间隔执行
    public static Vector2 position;
    
    void FixedUpdate()
    {

        position = transform.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        // 新位置给游戏对象
        rigidbody2D.position = position;
        
    }
}
