using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public GameObject selectWheel;
    public GameObject[] wheels;
    public float rotateValue;
    public int count;
    void Start()
    {
        selectWheel = wheels[0];
        foreach (var wheel in wheels)
        {
            RotateValue();
            wheel.transform.Rotate(0, 0, rotateValue);
        }
    }

    public void ConfirmAnswer()
    {
        count = 0;
        for (int i = 0; i < 3; i++)
        {
            Debug.Log(wheels[i].transform.rotation.z);
            if (wheels[i].transform.rotation.z <= 0.001f)
            {
                Debug.Log("wheel " + i + " activate");
                count++;
            }
        }
        if (count == 3)
        {
            Debug.Log("Win");
        }
    }

    public void FirstWheel()
    {
        selectWheel = wheels[0];
        //change color something
    }

    public void SecondWheel()
    {
        selectWheel = wheels[1];
        //change color something
    }

    public void ThirdWheel()
    {
        selectWheel = wheels[2];
        //change color something
    }

    public void RightRotate()
    {
        if (selectWheel != null)
        {
            StartCoroutine(RotateWheel(-1));
        }
    }
    public void LeftRotate()
    {
        if (selectWheel != null)
        {
            StartCoroutine(RotateWheel(1));
        }
    }

    void RotateValue()
    {
        rotateValue = Random.Range(0, 360);
        if (rotateValue % 30 != 0)
        {
            RotateValue();
        }
    }

    IEnumerator RotateWheel(float direction)
    {
        for (int i = 0; i < 30; i++)
        {
            yield return new WaitForSeconds(0.01f);
            selectWheel.transform.Rotate(direction * Vector3.forward);
        }
    }
}
