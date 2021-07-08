using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller2D : RaycastController
{
    //velocity is depend on moveSpeed in PlayerController
    float maxClimbAngle = 80f;
    float maxDescendAngle = 75f;
    public CollisionInfo collisions;
    [HideInInspector]
    public Vector2 playerInput;
    public static bool loadLevel;
    public static Collider2D collideObject;
    public static bool isGounded;
    public static GameObject hitItem;
    public static bool isPlayerHit;
    public override void Start()
    {
        base.Start();
    }

    public void Move(Vector3 velocity, bool standingOnPlatform)
    {
        Move(velocity, Vector2.zero, standingOnPlatform);
    }

    public void Move(Vector3 velocity, Vector2 input, bool standingOnPlatform = false)
    {
        UpdateRaycastOrigins();
        collisions.Reset();
        collisions.velocityOld = velocity;
        playerInput = input;

        if (velocity.y < 0)
        {
            DescendSlope(ref velocity);
        }

        if (velocity.x != 0)
        {
            HorizontalCollisions(ref velocity);
        }
        if (velocity.y != 0)
        {
            VerticalCollisions(ref velocity);
        }

        //Debug.Log(velocity.x);
        transform.Translate(velocity);
        if (standingOnPlatform)
        {
            collisions.below = true;
        }
    }

    public void HorizontalCollisions(ref Vector3 velocity)
    {
        //Return value is 1 when f is positive or zero, -1 when f is negative.
        float directionX = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;


        //Show 4 average rays in front of the object
        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);
            UnityEngine.Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.blue);
            if (hit)
            {
                //hit.normal is a vector perpendicular to the surface the ray hit
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                //when there is only one ray shoot
                if (i == 0 && slopeAngle <= maxClimbAngle)
                {
                    if (collisions.descendSlop)
                    {
                        collisions.descendSlop = false;
                        velocity = collisions.velocityOld;
                    }
                    float distanceToSlopeStart = 0;

                    //Climbing new slop
                    if (slopeAngle != collisions.slopeAngleOld)
                    {
                        //When the ray hit the slop, but player does not actually touch the slop
                        //then player will go up,and it will create a gap between player and the slop
                        distanceToSlopeStart = hit.distance - skinWidth;
                        //use the velocity only when player reach the slop
                        velocity.x -= distanceToSlopeStart * directionX;
                    }
                    ClimbSlope(ref velocity, slopeAngle);
                    //After finishing climbing slop, return to the normal velocity
                    velocity.x += distanceToSlopeStart * directionX;
                }

                if (!collisions.climbingSlope || slopeAngle > maxClimbAngle)
                {
                    velocity.x = (hit.distance - skinWidth) * directionX;
                    rayLength = hit.distance;

                    if (collisions.climbingSlope)
                    {
                        velocity.y = Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x);
                    }

                    collisions.left = directionX == -1;
                    collisions.right = directionX == 1;
                }

                HorizontalHit(ref hit);
            }
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void VerticalCollisions(ref Vector3 velocity)
    {
        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            //Move up, start from topleft corner
            //Move down, start from bottomleft corner
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.green);

            if (hit)
            {
                velocity.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;

                if (collisions.climbingSlope)
                {
                    velocity.x = velocity.y / Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(velocity.x);
                }

                collisions.below = directionY == -1;
                collisions.above = directionY == 1;
                VerticalHit(ref hit);
            }
        }

        if (collisions.climbingSlope)
        {
            float directionX = Mathf.Sign(velocity.x);
            rayLength = Mathf.Abs(velocity.x) + skinWidth;
            Vector2 rayOrigin = ((directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight) + Vector2.up * velocity.y;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            if (hit)
            {
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                //collide with new slope
                if (slopeAngle != collisions.slopeAngle)
                {
                    velocity.x = (hit.distance - skinWidth) * directionX;
                    collisions.slopeAngle = slopeAngle;
                }
            }
        }


    }

    public void ClimbSlope(ref Vector3 velocity, float slopeAngle)
    {
        //Use the initial moving speed to calculate the speed value of moving up and down according to the trigonometric function
        float moveDistance = Mathf.Abs(velocity.x);
        //when climb, assign values to velocity.y
        float climbVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
        if (velocity.y <= climbVelocityY)
        {
            velocity.y = climbVelocityY;
            //get new velocity.x with direction
            velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
            collisions.below = true;//make player be able to jump during climbing slop
            collisions.climbingSlope = true;
            collisions.slopeAngle = slopeAngle;
        }
    }

    public void DescendSlope(ref Vector3 velocity)
    {
        float directionX = Mathf.Sign(velocity.x);
        Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomRight : raycastOrigins.bottomLeft;

        //shoot the ray down(Infinity length)
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, -Vector2.up, Mathf.Infinity, collisionMask);

        if (hit)
        {
            float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
            if (slopeAngle != 0 && slopeAngle <= maxDescendAngle)
            {
                //moving down the slope
                if (Mathf.Sign(hit.normal.x) == directionX)
                {
                    //make sure the ray shoot the surface is close enough to do some actions
                    if (hit.distance - skinWidth <= Mathf.Tan(slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x))
                    {
                        float moveDistance = Mathf.Abs(velocity.x);
                        float descendVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
                        velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
                        velocity.y -= descendVelocityY;//make sure the descending speed can change with the velocity.x in case the velocity is high

                        collisions.slopeAngle = slopeAngle;
                        collisions.descendSlop = true;
                        collisions.below = true;//make player be able to jump during climbing slop
                    }
                }
            }
        }
    }

    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;

        public bool climbingSlope;
        public bool descendSlop;
        public float slopeAngle, slopeAngleOld;
        public Vector3 velocityOld;

        public void Reset()
        {
            above = below = false;
            left = right = false;
            climbingSlope = false;
            descendSlop = false;

            slopeAngleOld = slopeAngle;
            slopeAngle = 0;
        }
    }

    void VerticalHit(ref RaycastHit2D hit)
    {
        if (gameObject.CompareTag("Player"))
        {
            isGounded = true;
            BreakingPlat bp = null;
            if (hit.collider.CompareTag("PressurePlate"))
            {
                PressurePlate.isStandingOnPressurePlate = true;
            }
            else
            {
                PressurePlate.isStandingOnPressurePlate = false;
            }

            if (hit.collider.CompareTag("BreakingPlat"))
            {
                bp = hit.collider.gameObject.GetComponent<BreakingPlat>();
                bp.isOnPlat = true;
            }

            if (hit.collider.CompareTag("Lava"))
            {
                Lava.lava.isReset = false;
                gameObject.GetComponent<PlayerStatus>().lives = 0;
            }

        }

        if (gameObject.CompareTag("PushItems"))
        {
            Enermy enermy = hit.collider.gameObject.GetComponent<Enermy>();
            if (hit.collider.CompareTag("Enemy"))
            {
                enermy.isDead = true;
            }
        }

        if(gameObject.CompareTag("FallingStone"))
        {
            if(hit.collider.CompareTag("Player"))
            {
                PlayerStatus.instance.lives -= 3;
                PlayerController.playerControllerInstance.boxCollider.enabled = false;
                PlayerController.playerControllerInstance.enabled = false;
            }
        }
    }

    void HorizontalHit(ref RaycastHit2D hit)
    {
        if (gameObject.CompareTag("Player"))
        {
            if (hit.collider.CompareTag("NewLevel"))
            {
                collideObject = characterCollider;
                loadLevel = true;
            }

            if (hit.collider.CompareTag("PushItems"))
            {
                hitItem = hit.collider.gameObject;
                isPlayerHit = true;
            }
        }

        if(gameObject.CompareTag("Enemy"))
        {
            Enermy enermy = gameObject.GetComponent<Enermy>();
            if(enermy.isBoss && hit.collider.CompareTag("Player"))
            {
                PlayerStatus.instance.lives -= 3;
                enermy.enemySpeed = 0f;
            }
        }

        if (gameObject.CompareTag("Bullets")) 
        {
            if(hit.collider.CompareTag("Environment"))
            {
                Destroy(gameObject);
            }
        }
    }

}
