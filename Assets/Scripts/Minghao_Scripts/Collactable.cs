using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collactable : MonoBehaviour
{
   [SerializeField] private int ID = 001;

    public int ItemID { get => ID; set => ID = value; }

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
        if(collision.gameObject.tag=="Player")
        {
            collision.GetComponent<CollactableTracker>().AddItem(this);
            Destroy(gameObject);
        }
    }
}
