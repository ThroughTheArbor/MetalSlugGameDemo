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
        //��ȡ��ǰ�����rigidbody
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        // ��ȡˮƽ���룬�����󣬻��� -1.0 f ; �����ң����� 1.0 f       
        horizontal = Input.GetAxis("Horizontal");
        // ��ȡ��ֱ���룬�����£����� -1.0 f ; �����ϣ����� 1.0 f
        vertical = Input.GetAxis("Vertical");
    }
    //�̶�ʱ����ִ��
    public static Vector2 position;
    
    void FixedUpdate()
    {

        position = transform.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        // ��λ�ø���Ϸ����
        rigidbody2D.position = position;
        
    }
}
