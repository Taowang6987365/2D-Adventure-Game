using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnter : MonoBehaviour
{

    [SerializeField] Vector3 spawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && !Entrence.HasNext())
        {
            player.transform.position = spawnPosition;
        }
        
    }
    
}
