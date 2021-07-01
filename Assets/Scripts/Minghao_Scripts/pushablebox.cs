using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class pushablebox : MonoBehaviour
{
    public static pushablebox instance;
    private Vector3 initPos ;
    public int life = 3;
    

    [SerializeField] private bool canReset = false;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        initPos = transform.position;
    }

    public void GetHit()
    {
        if (life > 0)
        {
            life -= 1;
            BossCrystal.instance.life -= 1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(life<=0)
        {
            if(canReset)
            {
                transform.position = initPos;
                life = 3;
                
            }
            else
            {
                Controller2D.isPlayerHit = false;
                BossFightController.instance.count--;
                Destroy(gameObject,0.1f);
            }
        }
    }
}
