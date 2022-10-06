using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2PartIn : MonoBehaviour
{
    public GameObject Clone;
    int InNum;
    
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
        if(InNum == 0)
        {
            if (collision.CompareTag("Player"))
            {
                Clone.SetActive(true);

            }
            InNum++;
        }
        
    }
}
