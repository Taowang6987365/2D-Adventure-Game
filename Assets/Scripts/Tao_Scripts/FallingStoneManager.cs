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

    private IEnumerator StoneFalling()
    {
        foreach (var stone in stones)
        {
            yield return new WaitForSeconds(0.1f);
            stone.enabled = true;
        }
    }
}
