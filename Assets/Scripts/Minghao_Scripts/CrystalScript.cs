using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalScript : MonoBehaviour
{
    [SerializeField] private GameObject controlledDoor;
    private pushablebox box;
    private BossFightController bc;
    private int life;
    public bool isBossFight;
    private bool canDecrease;
    
    // Start is called before the first frame update
    void Start()
    {
        canDecrease = true;
        box = gameObject.GetComponent<pushablebox>();
        
    }

    // Update is called once per frame
    void Update()
    {
        life = box.life;
        if (!isBossFight)
        {
            if(life<=0)
            {
                controlledDoor.GetComponent<Door>().OpenDoor();
            }
        }

        if (isBossFight)
        {
            Debug.Log("Enemyaccount"+BossFightController.instance.count);
            if (life <= 0 && canDecrease)
            {
                Debug.Log("1");
                canDecrease = false;
                BossFightController.instance.count--;
                Destroy((this));
            }
            if (BossFightController.instance.count<=0)
            {
                Debug.Log("2");
                BossFightController.instance.AttackBoss();
            }
            
        }
        
        

        
    }
}
