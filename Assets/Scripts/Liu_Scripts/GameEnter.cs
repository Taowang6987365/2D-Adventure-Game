using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnter : MonoBehaviour
{
    bool isStart;
    // Start is called before the first frame update
    void Start()
    {
        isStart = true;
        if(isStart)
        {
            PlayerStatus.instance.transform.position = transform.position;
            isStart = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
