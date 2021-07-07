using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockAllLevels : MonoBehaviour
{


    public void PressUnlockAll()
    {
        for (int i = 0; i <= 2; i++)
        {
            GameObject.FindObjectsOfType<LevelSelection>()[i].unlocked = true;
        }
    }

}
