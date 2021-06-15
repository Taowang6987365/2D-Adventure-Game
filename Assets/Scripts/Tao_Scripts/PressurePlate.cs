using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject player;
    public GameObject deadZone;
    private Vector3 pressureMovement;
    public float pressurePlateMovementDistance;
    public float setDistance;
    public float speed;
    public static bool isStandingOnPressurePlate;

    public bool isReachMaxDistance;
    public bool isReachBottom;
    public bool isReachTop;
    void Start()
    {
        isReachBottom = false;
        isReachTop = true;
        setDistance = 150f;
        player = GameObject.FindGameObjectWithTag("Player");
        deadZone = GameObject.FindGameObjectWithTag("DeadZone");
        speed = 2;
    }

    void FixedUpdate()
    {
        PressureMove();
    }

    void PressureMove()
    {
        pressureMovement = new Vector3(0, -1 * speed, 0);

        if (isStandingOnPressurePlate && !isReachBottom && isReachTop)
        {
            isReachMaxDistance = false;
            if (Mathf.Abs(pressurePlateMovementDistance) <= setDistance && !isReachMaxDistance)
            {
                transform.Translate(pressureMovement * Time.fixedDeltaTime * 0.6f);
                pressurePlateMovementDistance += pressureMovement.y;
                deadZone.transform.Translate(pressureMovement * Time.fixedDeltaTime, Space.World);
            }
            else
            {
                isReachMaxDistance = true;
                isReachBottom = true;
                isReachTop = false;
                pressurePlateMovementDistance = 0;
            }
        }
        else if (!isStandingOnPressurePlate && !isReachTop)
        {
            isReachMaxDistance = false;
            if (Mathf.Abs(pressurePlateMovementDistance) <= setDistance)
            {
                pressurePlateMovementDistance += pressureMovement.y;
                transform.Translate(-1 * pressureMovement * Time.fixedDeltaTime * 0.6f);
                deadZone.transform.Translate(-1 * pressureMovement * Time.fixedDeltaTime, Space.World);
            }
            else
            {
                isReachMaxDistance = true;
                isReachTop = true;
                isReachBottom = false;
                pressurePlateMovementDistance = 0;
            }
        }
    }
}
