using System;
using System.Collections;
using UnityEngine;

public class BulletCharacter : MonoBehaviour
{
    public Vector3 dir;
    [Range(6f, 10f)] public float bulletSpeed = 7f;
    public bool isMove;
    public bool runOnce;
    bool isBallModePlay;
    public Controller2D controller2D;
    private BoxCollider2D playerCollider;
    private PlayerController playerController;
    private float boundsX;
    private float boundsY;
    
    void Start()
    {
        isMove = true;
        Destroy(gameObject, 5f);
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerCollider = playerController.gameObject.GetComponent<BoxCollider2D>();
            boundsX = playerCollider.bounds.max.x - playerCollider.bounds.min.x;
            boundsY = playerCollider.bounds.max.y - playerCollider.bounds.min.y;
        }
    }

    void Update()
    {
        if (isMove)
        {
            BulletMove();
            BulletHitPlayer();
        }
        
    }
    public void BulletMove()
    {
        transform.position += dir * bulletSpeed * Time.deltaTime;
    }

    public void BulletHitPlayer()
    {
        float distanceX = playerController.transform.position.x - this.transform.position.x;
        float distanceY = playerController.transform.position.y - this.transform.position.y;
        
        if (Mathf.Abs(distanceX) <= boundsX / 2 && Mathf.Abs(distanceY) <= boundsY / 2 && !runOnce)
        {
            playerController.HitPlayer();
            runOnce = true;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Environment"))
        {
            Destroy(gameObject);
        }
    }


    public IEnumerator DirChangeMoveMode(float endTime, float dirChangeTime, float angle)
    {
        float time = 0;
        bool isRotate = true;
        isBallModePlay = true;
        while (isBallModePlay)
        {
            time += Time.deltaTime;
            transform.position += bulletSpeed * dir * Time.deltaTime;
            if (time >= dirChangeTime && isRotate)
            {
                isRotate = false;
                StartCoroutine(BulletRotate(angle));
            }

            yield return null;
        }
    }
    
    IEnumerator BulletRotate(float angle)
    {
        while (isBallModePlay)
        {
            Quaternion tempQuat = Quaternion.AngleAxis(angle, Vector3.forward);
            dir = tempQuat * dir;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
