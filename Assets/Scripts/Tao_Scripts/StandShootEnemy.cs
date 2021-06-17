using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandShootEnemy : MonoBehaviour
{
    public float bulletSpeed;
    public float bulletTimer;
    public bool createOnce;
    float timer;
    public GameObject bullet;
    public Transform shootPos;
    public List<GameObject> goList;

    void Start()
    {
        timer = bulletTimer;
        shootPos = transform.GetChild(0).GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        StandEnemyBehaviour();
    }

    void StandEnemyBehaviour()
    {
        if (createOnce)
        {
            timer -= Time.fixedDeltaTime;
        }
        else
        {
            GameObject go = Instantiate(bullet, shootPos.position, transform.rotation);
            goList.Add(go);
            createOnce = true;
        }
        if (goList.Count > 10)
        {
            for (int i = 0; i < goList.Count - 1; i++)
            {
                Destroy(goList[i]);
                goList.Remove(goList[i]);
            }
        }
        else
        {
            foreach (var go in goList)
            {
                go.transform.Translate(Vector3.left * Time.deltaTime * bulletSpeed);
            }
        }
        if (timer <= 0)
        {
            createOnce = false;
            timer = bulletTimer;
        }
    }
}
