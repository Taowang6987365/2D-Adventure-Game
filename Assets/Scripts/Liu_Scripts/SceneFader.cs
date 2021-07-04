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

    IEnumerator FadeIn()
    {
        alpha = 1;
        while(true)
        {
            alpha -= Time.deltaTime;
            blackImage.color = new Color(0, 0, 0, alpha);
            yield return new WaitForSeconds(0f);
        }
    }

    IEnumerator FadeOut(string sceneName)
    {
        alpha = 0;
        while (true)
        {
            alpha += Time.deltaTime;
            blackImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        SceneManager.LoadScene((sceneName));
    }
}
