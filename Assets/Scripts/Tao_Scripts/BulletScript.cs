using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    float bulletSpeed;
    float playerBounds = 0f;
    bool runOnce;
   
    BoxCollider2D playerCollider;
    Controller2D controller2D;
    
    void Start()
    {
        bulletSpeed = 2f;
        controller2D = GetComponent<Controller2D>();
        playerCollider = Enermy.player.GetComponent<BoxCollider2D>();
        playerBounds = (playerCollider.bounds.max.y - playerCollider.bounds.min.y) / 2;
    }
 
    void Update()
    {
        controller2D.Move(Vector3.left * Time.deltaTime * bulletSpeed, false);
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
            Destroy(gameObject);
        }
    }
}
