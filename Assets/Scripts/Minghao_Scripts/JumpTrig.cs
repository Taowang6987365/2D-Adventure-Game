using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTrig : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player.GetComponent<PlayerController>().DoubleJump();
    }
}
