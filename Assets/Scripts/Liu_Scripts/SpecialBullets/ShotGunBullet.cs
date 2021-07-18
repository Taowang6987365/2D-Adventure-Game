using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;


public class ShotGunBullet : MonoBehaviour
{
    public BulletCharacter bulletTemplate;
    public Transform firPoint;
    private List<BulletCharacter> tempBullets;
    public float startTime;

    public float CountTime;
    public float StopTime;

    void Start()
    {
        startTime = 4f;
        tempBullets = new List<BulletCharacter>();
        CountTime *= Time.deltaTime;
        StopTime += Time.deltaTime;
        StartCoroutine(FirShotgun());
    }

    IEnumerator FirShotgun()
    {
        // while (CountTime < StopTime)
        // {
        //     float angle = 0;
        //     yield return new WaitForSeconds(2f);
        //     for (int i = 0; i < 10; i++)
        //     {
        //         CreatBullet(angle - 30, firPoint.transform.position);
        //         CreatBullet(angle, firPoint.transform.position);
        //         CreatBullet(angle + 30, firPoint.transform.position);
        //         //angle += 90;
        //         yield return new WaitForSeconds(0.5f);
        //     }
        // }
        Vector3 bulletDir = Vector3.down; 
        Quaternion leftRota = Quaternion.AngleAxis(-30, Vector3.forward);
        Quaternion RightRota = Quaternion.AngleAxis(30, Vector3.forward); 
        while(CountTime<StopTime)
        {
            yield return new WaitForSeconds(startTime);
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    switch (j)
                    {
                        case 0:
                            CreatBullet(bulletDir, firPoint.transform.position);  
                            break;
                        case 1:
                            bulletDir = RightRota * bulletDir;
                            CreatBullet(bulletDir, firPoint.transform.position);
                            break;
                        case 2:
                            bulletDir = leftRota * (leftRota * bulletDir); 
                            CreatBullet(bulletDir, firPoint.transform.position);
                            bulletDir = RightRota * bulletDir;
                            break;
                    }
                }
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    public BulletCharacter CreatBullet(Vector3 dir, Vector3 creatPoint)
    {
        BulletCharacter bulletCharacter = Instantiate(bulletTemplate, creatPoint, Quaternion.identity);
        bulletCharacter.gameObject.SetActive(true);
        bulletCharacter.dir = dir;
        tempBullets.Add(bulletCharacter);
        return bulletCharacter;
    }
}
