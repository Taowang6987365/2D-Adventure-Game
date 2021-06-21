using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    float bulletSpeed;
    float playerBounds = 0f;
    bool runOnce;
   
    BoxCollider2D playerCollider;
    
    void Start()
    {
        bulletSpeed = 2f;
        playerCollider = Enermy.player.GetComponent<BoxCollider2D>();
        playerBounds = (playerCollider.bounds.max.y - playerCollider.bounds.min.y) / 2;
    }
 
    void Update()
    {
        this.transform.Translate(Vector3.left * Time.deltaTime * bulletSpeed);
        HitPlayer();
        Destroy(gameObject, 10f);
    }

    void HitPlayer()
    {
        float distanceX = Enermy.player.transform.position.x - transform.position.x;
        float distanceY = Enermy.player.transform.position.y - transform.position.y;

        if (Mathf.Abs(distanceX) <= 0.1f && Mathf.Abs(distanceY) <= playerBounds && !runOnce)
        {
            Enermy.player.GetComponent<PlayerController>().HitPlayer();
            runOnce = true;
        }
        if (Mathf.Abs(distanceX) > 1f)
        {
            runOnce = false;
        }
    }
}
