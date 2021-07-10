using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingPlat : MonoBehaviour
{
    public bool isOnPlat;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2D;
    [SerializeField] private float setTimer;

    private Animator anim;

    private void Start()
    {
        setTimer = 3f;
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Controller2D.isGounded && isOnPlat)
        {
            StartCoroutine(PlatBreaking(setTimer));
        }
    }

    IEnumerator PlatBreaking(float timer)
    {
        yield return new WaitForSeconds(timer);
        //spriteRenderer.enabled = false;
        boxCollider2D.enabled = false;
        anim.SetBool("OnStep",true);
        isOnPlat = false;

        yield return new WaitForSeconds(timer);
        //spriteRenderer.enabled = true;
        boxCollider2D.enabled = true;
        anim.SetBool("OnStep",false);
    }
}
