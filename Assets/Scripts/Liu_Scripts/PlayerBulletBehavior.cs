using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletBehavior : MonoBehaviour
{
    private BossFightController bc;
    public GameObject explosionPrefab;
    public float speed;
    void Start()
    {
        speed = 4f;
    }

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);//up  
        Destroy(this,5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boss")
        {
            Instantiate(explosionPrefab,transform.position,transform.rotation);
            BossFightController.GetInstance().bossHP -= 10;
            BossFightController.GetInstance().booHPSlider.value -= 0.2f;
            Destroy(this.gameObject,0.5f);
        }
    }
}
