using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enermy : MonoBehaviour
{
    public int hp;
    public int maxHp;
    public float setMoveDistance;
    public float detectDistance;
    public float bulletSpeed;
    public float bulletTimer;
    public static bool facingRight;
    public static bool canAttack;
    public static bool isPlayerDetected;
    public static float speed;
    public static float boundsCenterDistance;
    public static GameObject player;
    public GameObject bullet;
    public static Controller2D controller;
    public static Vector3 velocity;
    public Transform shootPos;
    public bool isFollowPlayer;
    public bool isDead;
    public bool runOnce;
    public bool createOnce;
    public float movementSpeed;
    public BoxCollider2D enermyCollider;
    public BoxCollider2D playerCollider;
    float accelerationTimeAirborne = 0.2f;
    float accelerationTimeGrounded = 0.1f;
    float velocityXSmoothing;
    float moveDistance;
    float timer;
    public EnemyType enemyType;
    public List<GameObject> goList;


    public enum EnemyType
    {
        patrolEnemy,
        standEnemy,
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        controller = GetComponent<Controller2D>();
        enermyCollider = GetComponent<BoxCollider2D>();
        playerCollider = player.GetComponent<BoxCollider2D>();
        boundsCenterDistance = BoundsCenterDistance();

        timer = bulletTimer;
        movementSpeed = 5f;
        setMoveDistance = 400f;
        canAttack = false;
        isPlayerDetected = false;
        speed = movementSpeed;
        shootPos = transform.GetChild(0).GetComponent<Transform>();
    }
    private void Update()
    {
        EnemyAttack();
    }

    private void FixedUpdate()
    {
        velocity.y += PlayerController.gravity * Time.fixedDeltaTime;
        detectDistance = Vector3.Distance(this.transform.position, player.transform.position);
        switch (enemyType)
        {
            case EnemyType.patrolEnemy:
                PatrolEnemyBehaviour();
                break;

            case EnemyType.standEnemy:
                StandEnemyBehaviour();
                break;

            default:
                break;
        }


        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        if (isFollowPlayer)
        {
            MoveToPlayer(player.transform.position);
        }
    }

    void EnermyMove(ref Vector3 velocity)
    {
        Vector2 input = Vector2.right;
        float targetVelocityX = input.x * movementSpeed;
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

        if (!isPlayerDetected)
        {
            controller.Move(velocity * Time.fixedDeltaTime, input);
            //Patrol
            if (Mathf.Abs(moveDistance) >= setMoveDistance)
            {
                movementSpeed *= -1;
                moveDistance = 0;
            }
        }
    }

    void CharacterFlip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
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
            canAttack = true;
            runOnce = true;
        }
        else
        {
            canAttack = false;
        }

        if (Mathf.Abs(distanceX) > boundsCenterDistance)
        {
            runOnce = false;
        }
    }

    void MoveToPlayer(Vector3 playerPos)
    {
        if (detectDistance < 5f)
        {
            Enermy.isPlayerDetected = true;
            float distanceX = playerPos.x - transform.position.x;
            if (Mathf.Abs(distanceX) < BoundsCenterDistance())
            {
                canAttack = true;
                return;
            }
            else
            {
                canAttack = false;
            }
            float orientation = Mathf.Sign(distanceX);
            Vector3 currentPos = transform.position;
            Vector3 newPos = playerPos + 5 * orientation * Vector3.right;
            Vector3 CurrentPos = currentPos;
            CurrentPos.y -= PlayerController.gravity;
            Enermy.controller.Move((newPos - CurrentPos).normalized * Time.fixedDeltaTime * 20, false);
        }
        else
        {
            Enermy.isPlayerDetected = false;
        }
    }

    void PatrolEnemyBehaviour()
    {
        if (!isPlayerDetected)
        {
            EnermyMove(ref velocity);
            //flip sprite
            if (velocity.x > 0 && !facingRight)
            {
                CharacterFlip();
            }
            else if (velocity.x < 0 && facingRight)
            {
                CharacterFlip();
            }
        }
        else
        {
            if (this.transform.position.x - player.transform.position.x > 0 && facingRight)
            {
                CharacterFlip();
            }
            else if (this.transform.position.x - player.transform.position.x < 0 && !facingRight)
            {
                CharacterFlip();
            }
        }
    }

    void StandEnemyBehaviour()
    {
        controller.Move(velocity, false);
        if (createOnce)
        {
            timer -= Time.fixedDeltaTime;
        }
        else
        {
            GameObject go = Instantiate(bullet, shootPos.position, transform.rotation);
            goList.Add(go);
            createOnce = true;
        }
        if (goList.Count > 10)
        {
            for (int i = 0; i < goList.Count - 1; i++)
            {
                Destroy(goList[i]);
                goList.Remove(goList[i]);
            }
        }
        else
        {
            foreach (var go in goList)
            {
                go.transform.Translate(Vector3.left * Time.deltaTime * bulletSpeed);
            }
        }
        if (timer <= 0)
        {
            createOnce = false;
            timer = bulletTimer;
        }
    }

}
