using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalScript : MonoBehaviour
{
    [SerializeField] private GameObject controlledDoor;
    private pushablebox box;
    private int life;

    
    // Start is called before the first frame update
    void Start()
    {

        box = gameObject.GetComponent<pushablebox>();
        
    }

    // Update is called once per frame
    void Update()
    {
        life = box.life;

            if(life<=0)
            {
                controlledDoor.GetComponent<Door>().OpenDoor();
            }
            
        

        
    }
}
