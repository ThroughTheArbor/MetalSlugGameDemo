using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTwoBegin : MonoBehaviour
{
    public GameObject SkyCollider;
    int inTimes;
    public static bool isLevel2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (inTimes==0)
            {
                PlayerController player = collision.GetComponent<PlayerController>();
                player.ChangeGravity(0);
                SkyCollider.SetActive(true);
                inTimes++;
                isLevel2 = true;
            }
            
        }
    }
}
