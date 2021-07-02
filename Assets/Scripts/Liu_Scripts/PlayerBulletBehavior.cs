using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletBehavior : MonoBehaviour
{
    private BossFightController bc;
    
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);//up  
        Destroy(this,5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boss")
        {
            BossFightController.instance.bossHP -= 1;
            BossFightController.instance.booHPSlider.value -= 0.01f;
            Destroy(this.gameObject,0.5f);
        }

        
    }
}
