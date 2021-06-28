using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public float lavaSpeed;
    public Controller2D lavaController;
    void Start()
    {
        lavaSpeed = 1f;
    }

    void Update()
    {
        lavaController.Move(Vector3.up * Time.deltaTime * lavaSpeed, false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Lava"))
        {
            lavaSpeed = 0;
        }
    }
}
