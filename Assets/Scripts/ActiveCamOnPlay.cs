using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCamOnPlay : MonoBehaviour
{
    public GameObject gameObject;
    private GameObject fCam; //��һ�ӽ������
    private GameObject tCam; //�����ӽ������
                             //public Button[] buttons;
    bool flag = false; //fCam����Ϊtrue
    void Start()
    {

    }
    private void Awake()
    {
        //��ȡ���
        fCam = GameObject.Find("CM vcam1"); //��ȡ��һ�ӽ����
        tCam = GameObject.Find("Camera"); //��ȡ�����ӽ����
        fCam.SetActive(false); //���õ�һ�ӽ�����������ӽ���Ϊ�����
    }


    private void FixedUpdate()
    {
        //�ո��Space��Ϊ�л�����
        //ÿ����һ�η����л�

        if (Input.GetKeyDown(KeyCode.Space))
        {
            cam();
        }

        void cam()
        {
            flag = !flag; //״̬��ת
            fCam.SetActive(flag);
            tCam.SetActive(!flag); //�������״̬����
        }
    }
}
