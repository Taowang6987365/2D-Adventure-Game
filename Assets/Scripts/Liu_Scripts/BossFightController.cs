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
    private int id;
    public bool canshoot;
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
    

    public void CreateBox()
    {
        id = Random.Range(0, 10);
            //Debug.Log(id);
        for (int i = 0; i <1; i++)
        {
            if (id <= 5)
            {
                GameObject.Instantiate(breakableObj,spwanPosition[1].transform);
                GameObject.Instantiate(breakableObj,spwanPosition[3].transform);
                GameObject.Instantiate(breakableObj,spwanPosition[5].transform);
                count+=3;
            }
            else
            {
                GameObject.Instantiate(breakableObj,spwanPosition[0].transform);
                GameObject.Instantiate(breakableObj,spwanPosition[2].transform);
                GameObject.Instantiate(breakableObj,spwanPosition[4].transform);
                count+=3;
            }
            
            
            
        }
        
        
    }

    public void AttackBoss()
    {
        Debug.Log("Shoot");
        GameObject.Instantiate(bulletPrefab, firePosition);
    }
    
    
}
