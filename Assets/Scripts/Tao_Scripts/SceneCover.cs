using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneCover : MonoBehaviour
{
    public float timer;
    public Image image;
    void Start()
    {
        timer = 1f;
        image = gameObject.GetComponent<Image>();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            image.enabled = false;
            timer = 1f;
            this.enabled = false;
        }
    }
}
