using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrence : MonoBehaviour
{
    public static string Next { get; set; }

    public string entrancePassword;
    public float timer;

    void Start()
    {
        timer = 0.5f;
        if (Next == entrancePassword)
        {
            PlayerController.isMoveable = false;
            PlayerStatus.instance.transform.position = transform.position;//entrance position
        }
    }

    internal static bool HasNext() => !string.IsNullOrWhiteSpace(Next);

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            PlayerController.isMoveable = true;
            timer = 0.5f;

            if (HasNext())
            {
                Next = "";
            }
        }
    }
}
