using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTeleporter : MonoBehaviour
{
    [SerializeField] string nextLevelName;
    bool isLoading;
    private string currentLevelName;
    public static LevelTeleporter instance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Controller2D.loadLevel)
        {
            LevelLoad();
            Controller2D.loadLevel = false;
        }
    }

    private void LevelLoad()
    {
        if (isLoading)
        {
            return;
        }

        //InGameSaveManager.instance.levelName = LevelName.Level2;
        Debug.Log("activate");
        isLoading = true;
        Controller2D.collideObject.GetComponent<PlayerController>().ResetPosition();
        if (nextLevelName != "Title")
        {
            currentLevelName = nextLevelName;
            InGameSaveManager.instance.activeSave.currentLevel = currentLevelName;
            GameManager.Instance.LoadLevel(nextLevelName);
            InGameSaveManager.instance.Save();
            Debug.Log("current  level is " + currentLevelName);
            //InGameSaveManager.instance.Load();
        }
        else
        {
            GameManager.Instance.ReturnToTitle();
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isLoading)
        {
            return;
        }

        if (collision.CompareTag("Player"))
        {
            //InGameSaveManager.instance.levelName = LevelName.Level2;
            Debug.Log("activate");
            isLoading = true;
            collision.GetComponent<PlayerController>().ResetPosition();
            if (nextLevelName != "Title")
            {
                currentLevelName = nextLevelName;
                InGameSaveManager.instance.activeSave.currentLevel = currentLevelName;
                GameManager.Instance.LoadLevel(nextLevelName);
                InGameSaveManager.instance.Save();
                Debug.Log("current  level is " + currentLevelName);
                //InGameSaveManager.instance.Load();
            }
            else
            {
                GameManager.Instance.ReturnToTitle();
            }
        }
    }
}
