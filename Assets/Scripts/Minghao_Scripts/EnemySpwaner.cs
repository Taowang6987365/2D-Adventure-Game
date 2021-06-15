using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwaner : MonoBehaviour
{
    public GameObject Enemy;
    public float enemyCount;
    public Transform Position;
    private float PositonCount;
    Vector3 CurrentPOs;
    float PosZDistance;

    float StartZ;

    [SerializeField] private Transform Destination;
    void Start()
    {
        
        PositonCount = 1f;
        CurrentPOs = Position.position;
        StartZ = CurrentPOs.z;
        StartZ += 1;
        
        for (int i=0;i<enemyCount;i++)
        {
            
            CurrentPOs.z += 1.5f * PositonCount;
            PosZDistance = CurrentPOs.z - StartZ;
            if (PosZDistance >= 15)
            {
                CurrentPOs.z = StartZ;
                CurrentPOs.x += 1.5f;
                PositonCount = 1f;
            }
            
          GameObject enemy=   Instantiate(Enemy, CurrentPOs, Quaternion.identity);
            enemy.GetComponent<Enemey>().Destination1 = Destination;

            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
