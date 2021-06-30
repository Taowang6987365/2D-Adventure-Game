using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class BossFightController : MonoBehaviour
{
    [SerializeField] private GameObject breakableObj;
    private int count;
    private int max_count;
    private float nextCreateTime;
    public Transform[] spwanPosition;
    [SerializeField] private int id;

    private void Start()
    {
        nextCreateTime = 0;
        max_count = 3;
    }

    private void Update()
    {
        if (count < max_count)
        {
            CreateBox();
        }
        
    }

    private List<int> rawIndex = new List<int> {0, 1, 2, 3, 4, 5};
    private List<int> newIndex = new List<int>();


   

    public void CreateBox()
    {
        id = Random.Range(0, 10);
        Debug.Log(id);
        for (int i = 0; i < max_count; i++)
        {
            if (id <= 5)
            {
                GameObject.Instantiate(breakableObj,spwanPosition[1]);
                GameObject.Instantiate(breakableObj,spwanPosition[3]);
                GameObject.Instantiate(breakableObj,spwanPosition[5]);
                count++;
            }
            else
            {
                GameObject.Instantiate(breakableObj,spwanPosition[0]);
                GameObject.Instantiate(breakableObj,spwanPosition[2]);
                GameObject.Instantiate(breakableObj,spwanPosition[4]);
                count++;
            }
            
            
            
        }
        
        
    }
    
}
