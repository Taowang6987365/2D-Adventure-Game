using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformTurnpoint : MonoBehaviour
{
    public bool TurningDirection; //set true if want the plateform to go horizontal, false for it to go vertical
    public float MovingVelocity; //positive if go up or right, negative if go left or down

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MovingPlateform")
        {
            collision.GetComponent<PlateformActivePoint>().TurnDirection(TurningDirection,MovingVelocity);
            Destroy(gameObject);
        }
    }
}
