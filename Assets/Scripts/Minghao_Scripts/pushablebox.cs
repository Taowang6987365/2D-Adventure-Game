using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class pushablebox : MonoBehaviour
{
    public static pushablebox instance;
    private Vector3 initPos ;
    public int life = 3;
    private bool isFinished;
    private AudioSource audioSource;
    public GameObject particleSystem;

    [SerializeField] private bool canReset = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        instance = this;
        initPos = transform.position;
    }

    public void GetHit()
    {
        if (life > 0)
        {
            life -= 1;
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
                if (!isFinished)
                {
                    GameObject go = Instantiate(particleSystem,gameObject.transform.position,gameObject.transform.rotation);
                    audioSource.PlayOneShot(audioSource.clip);
                    audioSource.volume = 2;
                    isFinished = true;
                    Destroy(go,0.6f);
                }
                Controller2D.isPlayerHit = false;
                Destroy(gameObject,0.7f);
            }
        }
    }
}
