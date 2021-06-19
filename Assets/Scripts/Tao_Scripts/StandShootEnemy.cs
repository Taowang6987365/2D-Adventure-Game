using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandShootEnemy : MonoBehaviour
{
    public GameObject bullet;
    public Transform leftShootPos;
    public Transform rightShootPos;
    public float timer;
    [SerializeField] private bool isLeft;

    void Start()
    {
        isLeft = false;
        timer = 3f;
        leftShootPos = transform.GetChild(0).GetComponent<Transform>();
        rightShootPos = transform.GetChild(1).GetComponent<Transform>();
        GameObject go = Instantiate(bullet, leftShootPos.position, transform.rotation);
    }

    private void Update()
    {
        StandEnemyBehaviour();
    }

    void StandEnemyBehaviour()
    {
        timer -= Time.deltaTime;

        if(timer <= 0f && isLeft)
        {
            GameObject go = Instantiate(bullet, leftShootPos.position, transform.rotation);
            isLeft = false;
            timer = 3f;
        }

        if (timer <= 0f && !isLeft)
        {
            GameObject go = Instantiate(bullet, rightShootPos.position, transform.rotation);
            isLeft = true;
            timer = 3f;
        }
    }
}
