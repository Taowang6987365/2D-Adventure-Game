using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingStoneManager : MonoBehaviour
{
    public FallingStone[] stones;
    public bool isFinished;
    void Start()
    {
        stones = transform.GetComponentsInChildren<FallingStone>();
        foreach (var stone in stones)
        {
            stone.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(StoneFalling());
    }

    IEnumerator StoneFalling()
    {
        for (int i = 0; i < stones.Length; i++)
        {
            yield return new WaitForSeconds(0.1f);
            stones[i].enabled = true;
        }
    }
}
