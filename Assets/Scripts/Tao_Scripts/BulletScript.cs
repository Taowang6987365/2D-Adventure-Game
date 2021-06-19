using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    float bulletSpeed;
    
    private void Start()
    {
        bulletSpeed = 2f;   
    }

    void Update()
    {
        this.transform.Translate(Vector3.left * Time.deltaTime * bulletSpeed);
        Destroy(gameObject, 10f);
    }

    void HitPlayer()
    {

    }
}
