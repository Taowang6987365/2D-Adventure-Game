using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeHit : MonoBehaviour
{
    private PlayerController player;
    private bool hurtfull = true;
    private float cd = 0f;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.playerControllerInstance;
    }

    // Update is called once per frame
    void Update()
    {
        if(cd>0)
        {
            hurtfull = false;
        }
        else
        {
            hurtfull = true;
        }
        if(cd>0)
        {
            cd -= Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag=="Player")
        {
            player.HitPlayer();
            cd = 2f;
        }
    }
}
