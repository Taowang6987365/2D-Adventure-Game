using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class BossFightController : MonoBehaviour
{
    public static BossFightController instance;
    
    [SerializeField] private GameObject breakableObj;
    public int count;
    private int max_count;
    private float nextCreateTime;
    public Transform[] spwanPosition;
    [SerializeField] private int id;
    [SerializeField] private bool canshoot;
    public int bossHP;


    public Transform firePosition;
    public GameObject bulletPrefab;

    private void Start()
    {
        instance = this;
        bossHP = 10;
        nextCreateTime = 0;
        max_count = 3;
        canshoot = false;
    }

    private void Update()
    {
        if (count == 0)
        {
            CreateBox();
        }
        //Debug.Log(count);
        
    }

    private List<int> rawIndex = new List<int> {0, 1, 2, 3, 4, 5};
    private List<int> newIndex = new List<int>();


   

    public void CreateBox()
    {
        id = Random.Range(0, 10);
            //Debug.Log(id);
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

    public void AttackBoss()
    {
        GameObject.Instantiate(bulletPrefab, firePosition);
    }
    
    
}
