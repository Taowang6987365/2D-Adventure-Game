using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemyBehaviour : MonoBehaviour
{
    public Enermy enermy;
    public BoxCollider2D block;
    public Transform resetPos;
    void Start()
    {
        enermy = GetComponent<Enermy>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerStatus.instance.isDead)
        {
            block.enabled = true;
            transform.position = resetPos.position;
        }
    }
}
