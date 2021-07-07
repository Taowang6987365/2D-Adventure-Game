using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image blackImage;
    [SerializeField] private float alpha;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeTo(string _sceneName)
    {
        StartCoroutine(FadeOut(_sceneName));
    }

    public void PlayerDeathFade()
    {
        StartCoroutine((FadeIn()));
    }
    IEnumerator FadeIn()
    {
        alpha = 2.5f;
        while(true)
        {
            alpha -= Time.deltaTime;
            blackImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }

    IEnumerator FadeOut(string sceneName)
    {
        alpha = 0;
        while (true)
        {
            alpha += Time.deltaTime;
            blackImage.color = new Color(0, 0, 0, alpha);
            SceneManager.LoadScene((sceneName),LoadSceneMode.Additive);
            yield return null;
        }
    }
}
