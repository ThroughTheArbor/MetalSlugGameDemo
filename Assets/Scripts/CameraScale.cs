using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScale : MonoBehaviour
{
    //�ٶ�    
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

            //����size��С    

            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minmum, maximum);

            //���ָı�    

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

    
