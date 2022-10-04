using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCamOnPlay : MonoBehaviour
{
    public GameObject gameObject;
    private GameObject fCam; //第一视角摄像机
    private GameObject tCam; //第三视角摄像机
                             //public Button[] buttons;
    bool flag = false; //fCam启用为true
    void Start()
    {

    }
    private void Awake()
    {
        //获取相机
        fCam = GameObject.Find("CM vcam1"); //获取第一视角相机
        tCam = GameObject.Find("Camera"); //获取第三视角相机
        fCam.SetActive(false); //禁用第一视角相机，第三视角作为主相机
    }


    private void FixedUpdate()
    {
        //空格键Space作为切换触发
        //每按下一次发生切换

        if (Input.GetKeyDown(KeyCode.Space))
        {
            cam();
        }

        void cam()
        {
            flag = !flag; //状态反转
            fCam.SetActive(flag);
            tCam.SetActive(!flag); //两个相机状态互斥
        }
    }
}
