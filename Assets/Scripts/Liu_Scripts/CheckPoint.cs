using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject checkPoint;
    Controller2D controller2D;
    float playerYPos;
    [SerializeField] private Material checkPointAfterMaterial;

    // Start is called before the first frame update
    void Start()
    {
        controller2D = GetComponent<Controller2D>();
    }

    // Update is called once per frame
    void Update()
    {

            playerYPos = PlayerStatus.instance.playerYPos;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            checkPoint = this.gameObject;
            PlayerStatus.instance.checkPoint = checkPoint;
            InGameSaveManager.instance.activeSave.respawnPosition = new Vector2(PlayerStatus.instance.checkPoint.transform.position.x, playerYPos);
            checkPoint.GetComponent<Renderer>().material = checkPointAfterMaterial;
            Debug.Log(PlayerStatus.instance.checkPoint);
        }
    }
}
