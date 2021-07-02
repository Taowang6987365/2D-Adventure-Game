using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCrystal : MonoBehaviour
{
    public bool isBossFight;
    private bool canDecrease;
    private pushablebox box;

    // Start is called before the first frame update
    void Start()
    {
        canDecrease = true;
        box = GetComponent<pushablebox>();
    }

    // Update is called once per frame
    void Update()
    {

            if ( box.life <= 0 && canDecrease)
            {
                BossFightController.GetInstance().count--;
                canDecrease = false;
            }
            if (BossFightController.GetInstance().count<=0)
            {
                if (!BossFightController.canshoot)
                {
                    BossFightController.GetInstance().AttackBoss();
                }
            }
    }
}
