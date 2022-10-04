using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //¿Õ¸ñ¼ü·¢Éä
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerController.LaunchPlayer();
        }
    }
}
