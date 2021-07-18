using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public float lavaSpeed;
    public Controller2D lavaController;
    public bool isReset;
    public Vector3 initialPos;
    public static Lava lava;
    public GameObject lavaGo;
    public bool isLavaMove;
    private float timer;

    void Start()
    {
        lavaSpeed = 1f;
        initialPos = lavaGo.transform.position;
        lava = this;
        isLavaMove = false;
        timer = 2f;
    }

    void Update()
    {
        if (isLavaMove)
        {
            lavaController.Move(Vector3.up * Time.deltaTime * lavaSpeed, false);
            if(PlayerStatus.instance.lives <= 0 && !isReset)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    lavaGo.transform.position = initialPos;
                    isLavaMove = false;
                    isReset = true;
                    timer = 2f;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Lava"))
        {
            lavaSpeed = 0;
        }
    }
}
