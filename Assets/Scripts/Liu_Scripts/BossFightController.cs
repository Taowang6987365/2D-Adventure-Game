using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class BossFightController : MonoBehaviour
{
    private static BossFightController instance;
    
    [SerializeField] private GameObject breakableObj;
    public int count;
    private int max_count;
    private float nextCreateTime;
    public Transform[] spwanPosition; 
    private int id;
    public bool canshoot;
    public int bossHP;
    public Slider booHPSlider;
    public GameObject boss;
    private Animator anim;
    private Rigidbody2D rigid;
    [SerializeField] private GameObject fillArea;

    public Transform[] originalIndex;
    public List<Transform> rawIndex;
    public List<Transform> newIndex;
    


    public Transform firePosition;
    public GameObject bulletPrefab;

    private  BossFightController(){}
    private void Start()
    {
        instance = this;
        bossHP = 10;
        nextCreateTime = 0;
        max_count = 3;
        canshoot = false;
        booHPSlider.value = 1;
        anim = boss.GetComponent<Animator>();
        rigid = boss.GetComponent<Rigidbody2D>();
    }
    public static BossFightController GetInstance()
    {
        if (instance == null)
        {
            instance = new BossFightController();
        }
        return instance;
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

        if (bossHP <= 0)
        {
            anim.SetBool("IsDeath",true);
            rigid.gravityScale = 10;
            fillArea.SetActive(false);
            Destroy(boss,2f);
        }
    }
    
    public void CreateBox()
    {
        canshoot = false;
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
        canshoot = true;
        Debug.Log("Shoot");
        GameObject.Instantiate(bulletPrefab, firePosition);
    }
}
