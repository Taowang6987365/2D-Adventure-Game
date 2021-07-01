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
                BossFightController.instance.count--;
                canDecrease = false;
            }
            if (BossFightController.instance.count<=0)
            {
                BossFightController.instance.AttackBoss();
                BossFightController.instance.canshoot = false;
            }
    }
}
