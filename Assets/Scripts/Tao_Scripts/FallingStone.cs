using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FallingStone : MonoBehaviour
{
    public Vector3 stoneVelocity;
    public Controller2D controller;
    public float fallingSpeed;
    public float dirction;
    public float timer;

    void Start()
    {
        controller = GetComponent<Controller2D>();
        fallingSpeed = 0.1f;
        timer = 1f;
    }

    private void FixedUpdate()
    {
        StoneMove(ref stoneVelocity);

        if (controller.collisions.above || controller.collisions.below)
        {
            stoneVelocity.y = 0;
        }
    }

    void StoneMove(ref Vector3 velocity)
    {
        if (controller.collisions.below)
        {
            ResetDir(1);
        }
        if (controller.collisions.above)
        {
            ResetDir(-1);
        }

        if (dirction < 0)
        {
            velocity.y += PlayerController.gravity * Time.fixedDeltaTime * fallingSpeed;
        }
        else
        {
            velocity.y -= PlayerController.gravity * Time.fixedDeltaTime * fallingSpeed;
        }

        controller.Move(velocity * Time.fixedDeltaTime, false);
    }

    void ResetDir(int dir)
    {
        timer -= Time.fixedDeltaTime;
        if (timer <= 0)
        {
            dirction = dir;
            timer = 1;
        }
    }
}
