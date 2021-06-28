using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShotGunBullet : MonoBehaviour
{
    public BulletCharacter bulletTemplate;
    public Transform firPoint;
    private List<BulletCharacter> tempBullets;

    public float CountTime;
    public float StopTime;

    void Start()
    {
        tempBullets = new List<BulletCharacter>();
        CountTime *= Time.deltaTime;
        StopTime += Time.deltaTime;
        StartCoroutine(FirShotgun());
    }

    IEnumerator FirShotgun()
    {
        while (CountTime < StopTime)
        {
            float angle = 0;
            yield return new WaitForSeconds(2f);
            for (int i = 0; i < 10; i++)
            {
                CreatBullet(angle - 30, firPoint.transform.position);
                CreatBullet(angle, firPoint.transform.position);
                CreatBullet(angle + 30, firPoint.transform.position);
                //angle += 90;
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    public BulletCharacter CreatBullet(float angle, Vector3 creatPoint)
    {
        BulletCharacter bulletCharacter = Instantiate(bulletTemplate, creatPoint, Quaternion.identity);
        bulletCharacter.transform.Rotate(new Vector3(0, 0, angle));
        bulletCharacter.gameObject.SetActive(true);
        tempBullets.Add(bulletCharacter);
        return bulletCharacter;
    }
}
