using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuLang : MonoBehaviour
{
    public Text startText;
    public Text settingText;
    public Text quitText;
    private LanguageHandler lang;
    // Start is called before the first frame update
    void Start()
    {
        lang = LanguageHandler.instance;
    }

    // Update is called once per frame
    void Update()
    {
        //if(startText.text != lang.GetText(0))
        //{
        //    startText.text = lang.GetText(0);
        //}
        //if (settingText.text != lang.GetText(1))
        //{
        //    settingText.text = lang.GetText(1);
        //}
        //if (quitText.text != lang.GetText(2))
        //{
        //    quitText.text = lang.GetText(2);
        //}
    }
}
