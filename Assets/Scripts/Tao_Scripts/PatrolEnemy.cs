using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    Enermy enermy;
    public float setMoveDistance;
    public float detectDistance;

    void Start()
    {
        enermy = GetComponent<Enermy>();
        enermy.enemySpeed = 1f;
    }

    private void FixedUpdate()
    {
        detectDistance = Vector3.Distance(this.transform.position, Enermy.player.transform.position);
        //Patrol
        if (Mathf.Abs(enermy.moveDistance) >= setMoveDistance)
        {
            enermy.enemySpeed *= -1;
            enermy.moveDistance = 0;
        }
        //flip sprite
        if (enermy.velocity.x > 0 && !enermy.facingRight)
        {
            CharacterFlip();
        }
        else if (enermy.velocity.x < 0 && enermy.facingRight)
        {
            CharacterFlip();
        }
    }
    void CharacterFlip()
    {
        enermy.facingRight = !enermy.facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
