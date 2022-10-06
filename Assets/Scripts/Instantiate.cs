using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate : MonoBehaviour
{
    //在3个地点每隔一段时间生成3个不同的敌人
    public GameObject Prefab1;
    public GameObject Prefab2;
    public GameObject Prefab3;
    public Transform pos1;
    public Transform pos2;
    public Transform pos3;
    float timer1;
    float timer2;
    float timer3;
    public float CloneTime1;
    public float CloneTime2;
    public float CloneTime3;
    public int CloneNum1;
    public int CloneNum2;
    public int CloneNum3;
    public bool isTransfromChange;
    public Vector3 tChange1;
    public Vector3 tChange2;
    public Vector3 tChange3;
    public static int PartNum;
    public bool isEnd;
    // Start is called before the first frame update
    void Start()
    {
        timer1 = CloneTime1;
        timer2 = CloneTime2;
        timer3 = CloneTime3;
        if (LevelTwoBegin.isLevel2)
        {
            Instantiate(Prefab1,pos1.position,Quaternion.identity);           
            
            Instantiate(Prefab2, pos2.position, Quaternion.identity);
            Instantiate(Prefab3,pos3.position, Quaternion.identity);
            CloneNum1--;
            CloneNum2--;
            CloneNum3--;
        }
        else
        {
            Instantiate(Prefab1, pos1);
            timer1 = CloneTime1;
            CloneNum1--;
            Instantiate(Prefab2, pos2);
            timer2 = CloneTime2;
            CloneNum2--;
            Instantiate(Prefab3, pos3);
            timer3 = CloneTime3;
            CloneNum3--;
        }
        //子物体的transform是世界坐标，不用加父物体
        //pos1.position = GetComponent<Transform>().position + pos1.position;
        //pos2.position = GetComponent<Transform>().position + pos2.position;
        //pos3.position = GetComponent<Transform>().position + pos3.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    { 
        if (CloneNum1 > 0)
        {
            timer1 -= Time.deltaTime;
        }
        if (CloneNum2 > 0)
        {
            timer2 -= Time.deltaTime;
        }
        if(CloneNum3 > 0)
        {
            timer3 -= Time.deltaTime;
        }
        if (timer1 <= 0)
        {
            if (!isTransfromChange)
            {
                if (LevelTwoBegin.isLevel2)
                {
                    Instantiate(Prefab1, pos1.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(Prefab1, pos1);
                }
                //Instantiate(Prefab1, pos1);
                //Instantiate(Prefab1, pos1.position, Quaternion.identity);
                timer1 = CloneTime1;
                CloneNum1--;
            }
            if (isTransfromChange)
            {
                if (LevelTwoBegin.isLevel2)
                {
                    Instantiate(Prefab1, pos1.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(Prefab1, pos1);
                }
                //Instantiate(Prefab1, pos1);
                //Instantiate(Prefab1, pos1.position, Quaternion.identity);
                pos1.position = pos1.position + tChange1;
                timer1 = CloneTime1;
                CloneNum1--;
            }
        }
        if(timer2 <= 0)
        {
            if (!isTransfromChange)
            {
                if (LevelTwoBegin.isLevel2)
                {
                    Instantiate(Prefab2, pos2.position, Quaternion.identity);
                    //Instantiate(Prefab3, pos3.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(Prefab2, pos2);
                }
                //Instantiate(Prefab2, pos2);
                timer2 = CloneTime2;
                CloneNum2--;
            }
            if (isTransfromChange)
            {
                if (LevelTwoBegin.isLevel2)
                {
                    Instantiate(Prefab2, pos2.position, Quaternion.identity);
                    //Instantiate(Prefab3, pos3.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(Prefab2, pos2);
                }
                //Instantiate(Prefab2, pos2);

                pos2.position = pos2.position + tChange2;
                timer2 = CloneTime2;
                CloneNum2--;
            }

        }
        if(timer3 <= 0)
        {
            if (!isTransfromChange)
            {
                if (LevelTwoBegin.isLevel2)
                {
                    //Instantiate(Prefab2, pos2.position, Quaternion.identity);
                    Instantiate(Prefab3, pos3.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(Prefab3, pos3);
                }
                //Instantiate(Prefab3, pos3);
                timer3 = CloneTime3;
                CloneNum3--;
            }
            if (isTransfromChange)
            {
                if (LevelTwoBegin.isLevel2)
                {
                    //Instantiate(Prefab2, pos2.position, Quaternion.identity);
                    Instantiate(Prefab3, pos3.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(Prefab3, pos3);
                }
                //Instantiate(Prefab3, pos3);
                pos3.position = pos3.position + tChange3;
                timer3 = CloneTime3;
                CloneNum3--;

            }
        }
        if(CloneNum1 <= 0 && CloneNum2 <= 0 && CloneNum3 <= 0){
            isEnd = true;
        }
    }
    public void Clone()
    {
        GameObject Object1 = Instantiate(Prefab1, pos1.position, Quaternion.identity);
        GameObject Object2 = Instantiate(Prefab2, pos2.position, Quaternion.identity);
        GameObject Object3 = Instantiate(Prefab3, pos3.position, Quaternion.identity);
    }

}
