using System.Collections;
using UnityEngine;

public class BulletCharacter : MonoBehaviour
{
    public Vector3 dir;
    [Range(6f, 10f)] public float bulletSpeed = 7f;
    public bool isMove;
    bool isBallModePlay;

    void Start()
    {
        isMove = true;
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        if (isMove)
        {
            BulletMove();
        }
        
    }
    public void BulletMove()
    {
        transform.position += dir * bulletSpeed * Time.deltaTime;
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
