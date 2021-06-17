using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enermy : MonoBehaviour
{

    public bool isFollowPlayer;
    public bool isDead;
    public bool runOnce;
    public bool facingRight;
    public bool isBoss;
    public float patrolEnemySpeed;
    public float moveDistance;

    public Vector3 velocity;
    public Controller2D controller;
    public BoxCollider2D enermyCollider;
    public BoxCollider2D playerCollider;

    public static float boundsCenterDistance;
    public static GameObject player;

    float accelerationTimeAirborne = 0.2f;
    float accelerationTimeGrounded = 0.1f;
    float velocityXSmoothing;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        controller = GetComponent<Controller2D>();
        enermyCollider = GetComponent<BoxCollider2D>();
        boundsCenterDistance = BoundsCenterDistance();
    }
    private void Update()
    {
        EnemyAttack();
    }

    private void FixedUpdate()
    {
        velocity.y += PlayerController.gravity * Time.fixedDeltaTime;

        Movement();
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }
    }

    float BoundsCenterDistance()
    {
        Bounds enermyBounds = enermyCollider.bounds;
        Bounds playerBounds = playerCollider.bounds;
        float enermyHalfBounds = (enermyBounds.max.x - enermyBounds.min.x) / 2;
        float playerHalfBounds = (playerBounds.max.x - playerBounds.min.x) / 2;
        return (enermyHalfBounds + playerHalfBounds);
    }

    void EnemyAttack()
    {
        float distanceX = player.transform.position.x - transform.position.x;
        if (Mathf.Abs(distanceX) <= 0.1f && !runOnce)
        {
            player.GetComponent<PlayerController>().isHitByEnemy = true;
            runOnce = true;
        }
        else
        {
            player.GetComponent<PlayerController>().isHitByEnemy = false;
        }

        if (Mathf.Abs(distanceX) > boundsCenterDistance)
        {
            runOnce = false;
        }
    }

    void Movement()
    {
        Vector2 input = Vector2.right;
        float targetVelocityX = input.x * patrolEnemySpeed;
        //Mathf.SmoothDamp:
        //Gradually changes a value towards a desired goal over time.
        velocity.x = Mathf.SmoothDamp(
            velocity.x,
            targetVelocityX,
            ref velocityXSmoothing,
            (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne
            );
        moveDistance += velocity.x;
        velocity.y += PlayerController.gravity * Time.fixedDeltaTime;
        controller.Move(velocity * Time.fixedDeltaTime, input);
    }

    void ChasingEnemyBehaviour()
    {
        isBoss = true;
        Movement();
    }
}
