using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    public GameObject BossHpSlider;
    public GameObject winPanel;

    private float timer = 1f;
    private bool isFinished;

    private void Start()
    {
        winPanel.SetActive(false);
    }

    private void Update()
    {
        if (BossFightController.GetInstance().isWin)
        {
            BossHpSlider.SetActive(false);
            timer -= Time.deltaTime;
            if (timer <= 0 && !isFinished)
            {
                winPanel.SetActive(true);
                timer = 1f;
                isFinished = true;
            }
            Time.timeScale = 0;
        }
        else
        {
            winPanel.SetActive(false);
            isFinished = false;
        }
    }
}
