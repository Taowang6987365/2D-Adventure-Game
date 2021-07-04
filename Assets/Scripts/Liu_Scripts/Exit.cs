using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public string targetSceneName;
    public string currentScene;
    [SerializeField] private string newScecnePassword;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerStatus.instance.scenenPassword = newScecnePassword;
            //SceneManager.LoadScene(targetSceneName, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(currentScene);
            FindObjectOfType<SceneFader>().FadeTo(targetSceneName);
        }
    }
}
