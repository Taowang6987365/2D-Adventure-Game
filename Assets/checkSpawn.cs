using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkSpawn : MonoBehaviour
{
    private GameObject player;
    public int keyID;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        foreach(int item in player.GetComponent<CollactableTracker>().collection)
        {
            if(item == keyID)
            {
                Destroy(gameObject);
            }
        }
    }
}
