using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrence : MonoBehaviour
{
    public string entrancePassword;
    public float timer;
    
    void Start()
    {
        timer = 0.5f;
        if(PlayerStatus.instance.scenenPassword == entrancePassword)
        {
            PlayerController.isMoveable = false;
            PlayerStatus.instance.transform.position = transform.position;//entrance position
        }
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            PlayerController.isMoveable = true;
            timer = 0.5f;
        }
    }
}
