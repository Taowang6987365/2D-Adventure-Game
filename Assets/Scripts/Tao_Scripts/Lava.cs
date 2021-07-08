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

    void Start()
    {
        lavaSpeed = 1f;
        initialPos = transform.position;
        lava = this;
    }

    void Update()
    {
        lavaController.Move(Vector3.up * Time.deltaTime * lavaSpeed, false);
        if(PlayerStatus.instance.lives <= 0 && !isReset)
        {
            this.transform.position = initialPos;
            isReset = true;
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
