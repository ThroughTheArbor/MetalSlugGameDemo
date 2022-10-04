using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScale : MonoBehaviour
{
    //速度    
    public static float CameraSize;
    public Camera vc;
    public float ChangeSpeed = 0.5f;

    private float maximum = 13;

    private float minmum = 7;

    void Update()

    {
        Camera.main.orthographicSize = CameraSize;
        /*
        if (Input.GetAxis("Mouse ScrollWheel") != 0)

        {

            //限制size大小    

            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minmum, maximum);

            //滚轮改变    

            Camera.main.orthographicSize =

            Camera.main.orthographicSize - Input.GetAxis

            ("Mouse   ScrollWheel") * ChangeSpeed;

        }*/

    }
    private void Start()
    {
        Camera.main.orthographicSize = CameraSize; 
        //Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, 2.0f, Camera.main.transform.position.y);
        
    }

}

    
