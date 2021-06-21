using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject checkPoint;
    float playerYPos;
   //[SerializeField] private Material checkPointAfterMaterial;

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
            if(checkPoint != null)
            {
                InGameSaveManager.instance.activeSave.respawnPosition = new Vector3(PlayerStatus.instance.checkPoint.transform.position.x, playerYPos, 0);
            }
        }
    }
}
