using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrence : MonoBehaviour
{
    public static string Next { get; set; }

    public string entrancePassword;
    public float timer;

    private void Awake()
    {
        timer = 0.5f;
        if (Next == entrancePassword)
        {
            Time.timeScale = 1;
            PlayerStatus.instance.transform.position = transform.position;//entrance position
        }
    }

    internal static bool HasNext() => !string.IsNullOrWhiteSpace(Next);

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 0.5f;

            if (HasNext())
            {
                Next = "";
            }
        }
    }
}
