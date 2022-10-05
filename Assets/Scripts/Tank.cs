using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OFFSpriteRenderer()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        //GetComponent<SpriteRenderer>().enabled = true;   
    }
    public void ONSpriteRenderer()
    {
        //GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = true;   
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
