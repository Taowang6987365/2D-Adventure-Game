using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private float moveSpeed ;
    private float countDown = 0;
    public float doorSpeed = 0.8f;
    public bool isOpen = false;
    [SerializeField] bool isGuarded = false;
    [SerializeField] float guards = 0;
    private PlayerStatus playerStat;
    [SerializeField] bool resetOnPlayerDeath = true;
    private Vector3 originPos;
    private bool originStat;
    private bool originIsGuarded;
    private float originGuards;
   
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = doorSpeed;
        playerStat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        originPos = gameObject.transform.position;
        originStat = isOpen;
        originIsGuarded = isGuarded;
        originGuards = guards;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStat.isDead && resetOnPlayerDeath)
        {

            gameObject.transform.position = originPos;
            isOpen = originStat;
            isGuarded = originIsGuarded;
            //guards = originGuards;

        }

        if (countDown>0)
        {
            countDown -= Time.deltaTime;
        }
        if (countDown > 0)
        {
            transform.position += new Vector3(0, Time.deltaTime * moveSpeed, 0);
        }

        if(isGuarded && !isOpen)
        {
            CheckGuard();
        }

    }
    public void CloseDoor()
    {
        if (isOpen == true)
        {
            moveSpeed = -doorSpeed;
            isOpen = false;
            countDown = 4f;
        }
    }
    public void OpenDoor()
    {
        if (isOpen == false)
        {
            moveSpeed = doorSpeed;
            isOpen = true;
            countDown = 4f;
        }

    }
    public void GuardDead()
    {
        if (guards > 0)
        {
            guards -= 1;
        }
    }
    public void CheckGuard()
    {
        if(guards<=0)
        {
            isOpen = true;
            countDown = 4f;
        }
    }

    
}
