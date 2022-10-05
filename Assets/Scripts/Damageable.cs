using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetHitted()
    {
        GameObject Father = GetComponent<Transform>().parent.gameObject;        
        FSM fsm= Father.GetComponent<FSM>();
        if( fsm != null)
        {
            fsm.parameter.getHit = true;
        }
    }
}
