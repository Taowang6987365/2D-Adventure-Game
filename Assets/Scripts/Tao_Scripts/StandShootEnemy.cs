using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandShootEnemy : MonoBehaviour
{
    public GameObject bullet;
    public Transform shootPos;
    public float timer;

    void Start()
    {
        timer = 3f;
        shootPos = transform.GetChild(0).GetComponent<Transform>();
        GameObject go = Instantiate(bullet, shootPos.position, transform.rotation);
    }

    private void Update()
    {
        StandEnemyBehaviour();
    }

    void StandEnemyBehaviour()
    {
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            GameObject go = Instantiate(bullet, shootPos.position, transform.rotation);
            timer = 3f;
        }
    }
}
