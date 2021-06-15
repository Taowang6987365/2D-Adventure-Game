using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : RaycastController
{
    public LayerMask passengerMask;
    [SerializeField] private Vector3 move;
    public float platformMoveDistance = 0f;
    public float setMoveDistance = 100f;
    Vector3 velocity;
    public bool isHorizontal;
    public bool resetOnPlayerDeath = true;
    [SerializeField] private PlayerStatus playerStat;
    private Vector3 originPos;
    private bool originDirection;
    private float originMoveDistance;

    List<PassengerMovement> passengerMovement;
    Dictionary<Transform, Controller2D> passengerDictionary = new Dictionary<Transform, Controller2D>();
    public override void Start()
    {
        
        base.Start();
        playerStat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        originPos = gameObject.transform.position;
        originDirection = isHorizontal;
        originMoveDistance = platformMoveDistance;
    }

    private void Update()
    {
        if(playerStat.isDead && resetOnPlayerDeath)
        {
            
            ResetPlateform();
            
        }
        if (isHorizontal)
        {
            move.y = 0;
            if (move.x != 0)
            {
                MoveingPlateformX();
            }
        }
        else
        {
            move.x = 0;
            if (move.x == 0)
            {
                MoveingPlateformY();
            }
        }

        UpdateRaycastOrigins();

        velocity = move * (Time.deltaTime * 0.6f);

        CalculatePassengerMovement(velocity);

        MovePassengers(true);

        transform.Translate(velocity);

        MovePassengers(false);
    }

    void MovePassengers(bool beforeMovePlatform)
    {
        foreach (PassengerMovement passenger in passengerMovement)
        {
            if (!passengerDictionary.ContainsKey(passenger.transform))
            {
                passengerDictionary.Add(passenger.transform, passenger.transform.GetComponent<Controller2D>());
            }
            if (passenger.moveBeforePlatform == beforeMovePlatform)
            {
                passengerDictionary[passenger.transform].Move(passenger.velocity, passenger.standingOnPlatform);
            }
        }
    }

    void CalculatePassengerMovement(Vector3 velocity)
    {
        //Store all of the passengers that we already moved this frame
        //HashSet:No duplicate values are stored
        HashSet<Transform> movePassengers = new HashSet<Transform>();
        passengerMovement = new List<PassengerMovement>();

        float directionX = Mathf.Sign(velocity.x);
        float directionY = Mathf.Sign(velocity.y);

        //Vertically moving platform
        if (velocity.y != 0)
        {
            float rayLength = Mathf.Abs(velocity.y) + skinWidth;
            for (int i = 0; i < verticalRayCount; i++)
            {
                //Move up, start from topleft corner
                //Move down, start from bottomleft corner
                Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
                rayOrigin += Vector2.right * (verticalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, passengerMask);

                if (hit)
                {
                    //in case player move vertically multiple times, looks like shaking
                    if (!movePassengers.Contains(hit.transform))
                    {
                        movePassengers.Add(hit.transform);
                        float pushX = (directionY == 1) ? velocity.x : 0;
                        float pushY = velocity.y - (hit.distance - skinWidth) * directionY;

                        passengerMovement.Add(new PassengerMovement(hit.transform, new Vector3(pushX, pushY), directionY == 1, true));
                    }
                }
            }
        }

        //Horizontally moving platform
        if (velocity.x != 0)
        {
            float rayLength = Mathf.Abs(velocity.x) + skinWidth;

            //Show 4 average rays in front of the object
            for (int i = 0; i < horizontalRayCount; i++)
            {
                Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
                rayOrigin += Vector2.up * (horizontalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, passengerMask);

                if (hit)
                {
                    //prevent when the platform pushes player, player moves multiple times
                    if (!movePassengers.Contains(hit.transform))
                    {
                        movePassengers.Add(hit.transform);
                        float pushX = velocity.x - (hit.distance - skinWidth) * directionX;
                        float pushY = -skinWidth;

                        passengerMovement.Add(new PassengerMovement(hit.transform, new Vector3(pushX, pushY), false, true));
                    }
                }
            }
        }

        //Passenger on top of a horizontally or downward moving platform
        if (directionY == -1 || velocity.y == 0 && velocity.x != 0)
        {
            float rayLength = skinWidth * 2f;
            for (int i = 0; i < verticalRayCount; i++)
            {
                //Move up, start from topleft corner
                //Move down, start from bottomleft corner
                Vector2 rayOrigin = raycastOrigins.topLeft + Vector2.right * (verticalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, rayLength, passengerMask);

                if (hit)
                {
                    if (!movePassengers.Contains(hit.transform))
                    {
                        movePassengers.Add(hit.transform);
                        float pushX = velocity.x;
                        float pushY = velocity.y;

                        passengerMovement.Add(new PassengerMovement(hit.transform, new Vector3(pushX, pushY), true, false));

                    }
                }
            }
        }

    }

    struct PassengerMovement
    {
        public Transform transform;
        public Vector3 velocity;
        public bool standingOnPlatform;
        public bool moveBeforePlatform;

        public PassengerMovement(Transform _transform, Vector3 _velocity, bool _standingOnPlatform, bool _moveBeforePlatform)
        {
            transform = _transform;
            velocity = _velocity;
            standingOnPlatform = _standingOnPlatform;
            moveBeforePlatform = _moveBeforePlatform;
        }
    }

    public void MoveingPlateformX()
    {
        platformMoveDistance += velocity.x;
        if (Mathf.Abs(platformMoveDistance) >= setMoveDistance)
        {
            move.x *= -1;
            platformMoveDistance = 0;
        }
    }
    public void MoveingPlateformY()
    {
        platformMoveDistance += velocity.y;
        if (Mathf.Abs(platformMoveDistance) >= setMoveDistance)
        {
            move.y *= -1;
            platformMoveDistance = 0;
        }
    }


    public void ChangeDirection(bool val, float MoveDistance)
    {
        isHorizontal = val;
        if(!val)
        {
            move.y = 2;
            move.x = 0;
            
        }
        else
        {
            move.y = 0;
            move.x = 2;
        }
        
        platformMoveDistance = 0;
        setMoveDistance = MoveDistance;
        
    }
    private void ResetPlateform()
    {
        platformMoveDistance = 0;
        isHorizontal = originDirection;
        platformMoveDistance = originMoveDistance;
        gameObject.transform.position = originPos;
        if (!isHorizontal)
        {
            move.y = 2;
            move.x = 0;

        }
        else
        {
            move.y = 0;
            move.x = 2;
        }
        gameObject.GetComponent<PlatformController>().enabled = false;

    }

    public void StopPlateform()
    {
        move.y = 0;
        move.x = 0;
    }
}
