using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    public int lives;
    private int maxLives;
    public float resteTime = 0.2f;
    public float playerYPos;
    public bool isDead;
    public string scenenPassword;//store the name when player move to another scene
    public Canvas fadeInCanvas;

    public GameObject checkPoint;
    public GameObject checkPointAfter;
    public Controller2D controller2D;
    public PlayerController playerController;
    public Transform transformPos;

    public static PlayerStatus instance;
    [SerializeField] private Text liveText;
    float deathTimer = 3f;



    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        isDead = false;
        maxLives = 3;
        lives = maxLives;
        transformPos = GetComponent<Transform>();
        controller2D = GetComponent<Controller2D>();
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (liveText != null)
        {
            liveText.text = "X " + lives.ToString();
        }

        ////Add lives
        //if (lives >= maxLives)
        //{
        //    lives = maxLives;
        //}

        if (lives <= 0)
        {
            lives = 0;
            
            playerController.Death();
            deathTimer -= Time.deltaTime;
            if(deathTimer <= 0)
            {
                if(!PlayerController.playerControllerInstance.enabled)
                {
                    PlayerController.playerControllerInstance.enabled = true;
                    PlayerController.playerControllerInstance.boxCollider.enabled = true;
                }
                //fadeInCanvas.gameObject.SetActive(true);
                FindObjectOfType<SceneFader>().PlayerDeathFade();
                ResetGame();
                deathTimer = 3f;
            }
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
            lives = 3;
            resteTime = 0.2f;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeadZone"))
        {
            InGameSaveManager.currentSaveData.playerLives = lives;
            playerController.HitPlayer();
        }
        if (collision.tag == ("CheckPoint"))
        {
            if(checkPoint != null)
            {
                InGameSaveManager.currentSaveData.respawnPosition = new Vector2(checkPoint.transform.position.x, playerYPos);
            }

            InGameSaveManager.Save();
        }
    }

    private void ResetGame()
    {
        isDead = true;
        transformPos.position = InGameSaveManager.currentSaveData.respawnPosition;
        PlayerController.isMoveable = true;
        InGameSaveManager.Load();//dead then reload
        //SceneManager.LoadScene("Title");
    }

}
