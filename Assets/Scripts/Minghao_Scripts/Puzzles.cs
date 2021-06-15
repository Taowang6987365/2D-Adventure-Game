using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzles : MonoBehaviour
{
    [SerializeField] GameObject controledDoor;
    [SerializeField] bool close=false;
    [SerializeField] bool canTriggerByPlayer = false;
    [SerializeField] bool requireKey = false;
    [SerializeField] int keyID = 0;
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
        if (collision.transform.tag == "PushItems" && !canTriggerByPlayer)
        {
            //Debug.Log("open");
            if (!close)
            {
                controledDoor.GetComponent<Door>().OpenDoor();
            }
            else
            {
                Debug.Log("close");
                controledDoor.GetComponent<Door>().CloseDoor();
            }

            
        }
        else if(collision.transform.tag == "Player" && canTriggerByPlayer)
        {
            if (requireKey)
            {
                foreach (int item in collision.GetComponent<CollactableTracker>().collection)
                {
                    if (item == keyID)
                    {
                        Debug.Log("key founded");
                        if (!close)
                        {
                            controledDoor.GetComponent<Door>().OpenDoor();
                        }
                        else
                        {
                            Debug.Log("close");
                            controledDoor.GetComponent<Door>().CloseDoor();
                        }
                        gameObject.GetComponent<Puzzles>().enabled = false;
                    }
                    else
                    {
                        Debug.Log("Key not found");
                    }
                }
            }
            else
            {
                if (!close)
                {
                    controledDoor.GetComponent<Door>().OpenDoor();
                }
                else
                {
                    Debug.Log("close");
                    controledDoor.GetComponent<Door>().CloseDoor();
                }
                gameObject.GetComponent<Puzzles>().enabled = false;
            }
        }
    }

}
