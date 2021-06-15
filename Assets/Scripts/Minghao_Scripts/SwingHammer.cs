using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingHammer : MonoBehaviour
{
    private Animator anim;
    public float SwingSpeed = 1;
    [SerializeField] private float delay = 0f;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(delay>0)
        {
            delay -= Time.deltaTime;
        }

    
        if(delay>0)
        {
            anim.SetFloat("SwingSpeed", 0);
        }
        else
        {
            anim.SetFloat("SwingSpeed", SwingSpeed);
        }
    }
}
