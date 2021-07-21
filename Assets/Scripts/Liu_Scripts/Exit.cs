using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public string sceneName;
    public string currentScene;
    [SerializeField] private string newScecnePassword;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Entrence.Next = newScecnePassword;
            PlayerStatus.instance.scenenPassword = newScecnePassword;
            FindObjectOfType<SceneFader>().FadeTo(sceneName);
            //SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(currentScene);
            InGameSaveManager.currentSaveData.currentLevel = sceneName;
            Time.timeScale = 0;
        }
    }
}
