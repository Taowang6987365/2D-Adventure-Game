using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformActivePoint : MonoBehaviour
{
    [SerializeField] private PlatformController control;
    private bool alreadyActive = false;
    private bool resetOnPlayerDeath;
    private PlayerStatus playerStat;

    // Start is called before the first frame update
    void Start()
    {
        control.enabled = false;
        playerStat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        resetOnPlayerDeath = control.resetOnPlayerDeath;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStat.isDead && resetOnPlayerDeath)
        {

            alreadyActive = false;

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"&&alreadyActive==false)
        {
            control.enabled = true;
            alreadyActive = true;
        }
    }
    public void StopPlateform()
    {
        control.StopPlateform();

    }
    public void TurnDirection(bool isHorizontal, float moveDistance)
    {
        control.ChangeDirection(isHorizontal,moveDistance);
    }    
}
