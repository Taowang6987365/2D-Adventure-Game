using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemyAnim : MonoBehaviour
{
    public Enermy enermy;
    private void Start()
    {
        enermy = GetComponent<Enermy>();
        enermy.enemySpeed = 8f;
    }
    void Update()
    {
        enermy.isBoss = true;
    }
}
