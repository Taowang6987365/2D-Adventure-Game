using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCrystal : MonoBehaviour
{
    public bool isBossFight;
    private bool canDecrease;
    private pushablebox box;
    public static BossCrystal instance;

    public int life;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        canDecrease = true;
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log("Enemyaccount"+BossFightController.instance.count);
            if (life <= 0 && canDecrease)
            {
                //BossFightController.instance.count--;
                canDecrease = false;
            }
            if (BossFightController.instance.count<=0)
            {

                    BossFightController.instance.AttackBoss();
                    BossFightController.instance.canshoot = false;

                

            }
            


    }
}
