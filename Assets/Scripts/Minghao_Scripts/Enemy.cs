using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]private bool isGuard = false;
    [SerializeField] GameObject? guardDoor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.transform.tag == "PushItems")
        {
           // Debug.Log("Ouch");
           if(isGuard)
            {
                guardDoor.GetComponent<Door>().GuardDead();
            }


            Destroy(this.gameObject);
        }
    }

}
