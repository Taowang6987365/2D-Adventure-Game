namespace Liu_Scripts.SpecialBullets
{
   using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundGroupBullet : MonoBehaviour
{
    public BulletCharacter bulletTemplate;
    public Transform firPoint;
    public List<BulletCharacter> tempBullets;
    public float startTime;

    public float CountTime = 1;
    public float StopTime = 2;

    // Start is called before the first frame update
    void Start()
    {
        startTime = 10f;
        tempBullets = new List<BulletCharacter>();
        CountTime *= Time.deltaTime;
        StopTime += Time.deltaTime;
        StartCoroutine(FirRoundGroup());
        
    }

    IEnumerator FirRound(int number, Vector3 creatPoint)
    {
        Vector3 bulletDir = Vector3.down;//更改弹幕发射方式
        Quaternion rotateQuate = Quaternion.AngleAxis(30, Vector3.forward);//使用四元数制造绕Z轴旋转10度的旋转
            //yield return new WaitForSeconds(5f);
            for (int i = 0; i < number; i++)    //发射波数
            {
                for (int j = 0; j < 12; j++)
                {
                    CreatBullet(bulletDir, creatPoint);
                    bulletDir = rotateQuate * bulletDir; //让发射方向旋转10度，到达下一个发射方向
                }
                yield return new WaitForSeconds(0.5f); //协程延时，0.5秒进行下一波发射
            }
            yield return null;

        

    }


    IEnumerator FirRoundGroup()
        {
        Vector3 bulletDir = firPoint.transform.up;
        Quaternion rotateQuate = Quaternion.AngleAxis(90, Vector3.forward);
        while (CountTime < StopTime)
        {
            yield return new WaitForSeconds(startTime);
            List<BulletCharacter> bullets = new List<BulletCharacter>();      
            for (int i = 0; i < 4; i++)
            {
                var tempBullet = CreatBullet(bulletDir, firPoint.transform.position);
                bulletDir = rotateQuate * bulletDir; 
                bullets.Add(tempBullet);
            }
            yield return new WaitForSeconds(1.0f);   
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].bulletSpeed = 0;
                StartCoroutine(FirRound(4, bullets[i].transform.position));
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

}