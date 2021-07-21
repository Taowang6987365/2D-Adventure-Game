using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonText : MonoBehaviour
{
    [SerializeField] private Text txt;
    [SerializeField] private string logID;
    private LanguageHandler lang;

    // Start is called before the first frame update
    void Start()
    {
        lang = LanguageHandler.instance;
        txt = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = lang.GetText(logID);
    }
}
