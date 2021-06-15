using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    void Awake()
    {
        if (GameManager.currentLevel == "")
        {
            GameManager.currentLevel = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("Game");
        }
    }
}
