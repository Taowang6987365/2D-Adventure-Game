using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public BoxCollider2D block;
    public BoxCollider2D trigger;
    public Enermy boss;
    void Start()
    {
        trigger = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            boss.isBoss = true;
            boss.enemySpeed = 8f;
            block.enabled = false;
        }
    }
}
