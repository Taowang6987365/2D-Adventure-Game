using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Centi_Behaviour : MonoBehaviour
{
    public Animator animator;
    public float detectDistance;
    public Transform playerPos;
    public Enermy enermy;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerPos = Enermy.player.transform;
        enermy = GetComponent<Enermy>();
    }

    private void Update()
    {
        if (enermy.isDead)
        {
            StartCoroutine(EnemyDeath());
        }
    }

    void FixedUpdate()
    {
        animator.SetFloat("Speed", Mathf.Abs(Enermy.velocity.x));
    }

    IEnumerator EnemyDeath()
    {
        animator.SetBool("isDead", true);
        yield return new WaitForSeconds(0.6f);
        Destroy(this.gameObject);
    }

}
