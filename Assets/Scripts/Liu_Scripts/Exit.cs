using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public string sceneName;
    [SerializeField] private string newScecnePassword;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerStatus.instance.scenenPassword = newScecnePassword;
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }
    }
}
