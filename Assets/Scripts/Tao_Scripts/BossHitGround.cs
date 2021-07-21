using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitGround : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private Vector3 deadPos;
    private bool isHit;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isHit)
        {
            transform.position = deadPos;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Environment"))
        {
            deadPos = transform.position;
            rigidbody2D.velocity = Vector2.zero;
            isHit = true;
        }
    }
}
