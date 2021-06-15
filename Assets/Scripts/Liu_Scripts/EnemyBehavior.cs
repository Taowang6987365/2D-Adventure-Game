using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(Enermy.canAttack == true)
        {
            Attack();
        }
        else if(Enermy.canAttack == false)
        {
            StopAttack();
        }
    }

    public void Attack()
    {
        anim.SetBool("CanAttack", true);
    }

    public void StopAttack()
    {
        anim.SetBool("CanAttack", false);
    }
}
