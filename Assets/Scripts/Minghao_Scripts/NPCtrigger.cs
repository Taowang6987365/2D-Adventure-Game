using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCtrigger : MonoBehaviour
{
    [SerializeField] private GameObject player;
    //[SerializeField] private string phrase = "";
    [SerializeField]private string logID;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            //Debug.Log("trig");
            player.GetComponent<PlayerController>().ShowGuide(logID);
            Destroy(gameObject);
        }
    }
}
