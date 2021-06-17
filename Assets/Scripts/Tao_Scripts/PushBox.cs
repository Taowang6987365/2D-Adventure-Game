using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBox : MonoBehaviour
{
    public Vector3 velocity;
    public bool isBoxPushable;
    GameObject player;
    public BoxCollider2D itemCollider;
    public BoxCollider2D playerCollider;
    Controller2D playerController;
    public Controller2D boxController;
    public static float distanceX;
    public static float distance;
    public static bool isPushing;
    public pushablebox _pushableBox;
    private PlayerStatus playerStat;
    private Vector3 originPos;
    private Vector3 curVelocity;
    public float speedModifier = 0.6f;

    private void Awake()
    {
        _pushableBox = GetComponent<pushablebox>();
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponent<BoxCollider2D>();
        itemCollider = GetComponent<BoxCollider2D>();
        playerController = player.GetComponent<Controller2D>();
        boxController = GetComponent<Controller2D>();
        distance = BoundsCenterDistance();
        playerStat = player.GetComponent<PlayerStatus>();
        originPos = transform.position;
    }
    private void Update()
    {
        if (playerStat.isDead)
        {
            transform.position = originPos;
        }
    }

    void FixedUpdate()
    {
        if (boxController.collisions.above || boxController.collisions.below)
        {
            velocity.y = 0;
        }

        velocity.y += PlayerController.gravity * Time.fixedDeltaTime ;
        curVelocity = new Vector3(0, velocity.y, 0);

        boxController.Move(curVelocity * Time.fixedDeltaTime, false);

        distanceX = Mathf.Abs(player.transform.position.x - transform.position.x);
        if (isBoxPushable)
        {
            if (distanceX <= BoundsCenterDistance())
            {
                if (playerController.collisions.left || playerController.collisions.right)
                {
                    isPushing = true;
                    velocity.x = PlayerController.VelocityX * speedModifier;
                    boxController.Move(velocity * Time.fixedDeltaTime, false);
                }
            }
        }
    }

    float BoundsCenterDistance()
    {
        Bounds itemBounds = itemCollider.bounds;
        Bounds playerBounds = playerCollider.bounds;
        float itemHalfBounds = (itemBounds.max.x - itemBounds.min.x) / 2;
        float playerHalfBounds = (playerBounds.max.x - playerBounds.min.x) / 2;

        return (itemHalfBounds + playerHalfBounds + 0.1f);
    }
}
