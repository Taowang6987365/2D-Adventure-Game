using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    public GameObject checkPoint;
    public GameObject checkPointAfter;
    public int lives;
    private int maxLives;
    public static PlayerStatus instance;
    public Transform transformPos;


    public float resteTime = 0.2f;
    public float playerYPos;
    public bool isDead;
    public Controller2D controller2D;

    [SerializeField] private Text liveText;

    public string scenenPassword;//store the name when player move to another scene


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        isDead = false;
        controller2D = GetComponent<Controller2D>();
        maxLives = 3;
        lives = maxLives;
        transformPos = GetComponent<Transform>();
    }

    void Update()
    {
        if (liveText != null)
        {
            liveText.text = "X " + lives.ToString();
        }
        if (lives >= maxLives)
        {
            lives = maxLives;
        }
        if (lives <= 0)
        {
            lives = 0;
            ResetGame();
        }
        if (controller2D.collisions.below)
        {
            playerYPos = this.transformPos.position.y;
        }
        if (isDead)
        {
            PlayerController.isMoveable = false;
            resteTime -= Time.deltaTime;
        }

        if (resteTime <= 0)
        {
            PlayerController.isMoveable = true;
            isDead = false;
            resteTime = 0.2f;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeadZone"))
        {
            lives -= 1;
            transformPos.position = InGameSaveManager.instance.activeSave.respawnPosition;
            //transformPos.position = GameObject.FindGameObjectWithTag("Spawn").transform.position;
            InGameSaveManager.instance.activeSave.playerLives = lives;
            isDead = true;
            InGameSaveManager.instance.Load();
        }
        if (collision.tag == ("CheckPoint"))
        {
            Debug.Log("Save111");
            //checkPoint = GameObject.FindGameObjectWithTag("CheckPoint");
            //checkPointAfter = GameObject.FindGameObjectWithTag("CheckPointAfter");
            InGameSaveManager.instance.activeSave.respawnPosition = new Vector2(checkPoint.transform.position.x, playerYPos);

            InGameSaveManager.instance.Save();
            //checkPoint.SetActive(false);
        }
    }

    private void ResetGame()
    {
        SceneManager.LoadScene("Title");
    }
}
