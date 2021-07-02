using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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

    public Transform[] originalIndex;
    public List<Transform> rawIndex;
    public List<Transform> newIndex;


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
            nextCreateTime -= Time.deltaTime;
            if (nextCreateTime <= 0)
            {
                CreateBox();
                nextCreateTime = 2f;
            }
        }
    }
    
    public void CreateBox()
    {
        foreach (var item in originalIndex)
        {
            rawIndex.Add(item);
        }
        newIndex.Clear();
        int tempCount = rawIndex.Count;

        for (int i = 0; i < tempCount; i++)
        {
            int tempIndex = UnityEngine.Random.Range(0, rawIndex.Count);
            newIndex.Add(rawIndex[tempIndex]);
            rawIndex.RemoveAt(tempIndex);
        }

        for (int i = 0; i < 3; i++)
        {
            Instantiate(breakableObj,newIndex[i].position,quaternion.identity);
            count++;
        }
    }

    public void AttackBoss()
    {
        Debug.Log("Shoot");
        GameObject.Instantiate(bulletPrefab, firePosition);
    }
}
