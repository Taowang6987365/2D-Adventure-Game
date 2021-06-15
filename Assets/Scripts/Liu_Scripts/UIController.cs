using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UltimateClean;

public class UIController : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(Switch.instance.canControlVolume == true)
        //{
        //    volumeSlider.interactable = true;
        //}
        //else if(Switch.instance.canControlVolume == false)
        //{
        //    volumeSlider.interactable = false;
        //    volumeSlider.value = 0;
        //}
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }


}
