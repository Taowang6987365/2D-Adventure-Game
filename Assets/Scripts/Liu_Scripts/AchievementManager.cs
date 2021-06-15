using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    [SerializeField] private Toggle[] achievementObj;

    private int maxAchieves;
    public int goalsAchieved;

    public static AchievementManager instance;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        maxAchieves = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(goalsAchieved>=maxAchieves)
        {
            goalsAchieved = maxAchieves;
        }
        UpdateAchieves();
    }

    public void UpdateAchieves()
    {
            if(goalsAchieved == 1)
            {
                Debug.Log("AAA");
                ToggleOneOn(true);
            }
            else if (goalsAchieved == 2)
            {
                Debug.Log("BBB");
                achievementObj[goalsAchieved-1].isOn = true;
            }
            else if (goalsAchieved == 3)
            {
                Debug.Log("CCC");
                achievementObj[goalsAchieved-1].isOn = true;
            }        
    }

    public void ToggleOneOn(bool isOn)
    {
        achievementObj[goalsAchieved - 1].isOn = true;
    }

   
}
