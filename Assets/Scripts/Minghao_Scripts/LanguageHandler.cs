using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LanguageHandler : MonoBehaviour
{

    public class Dialogs
    {
        public string[] eng;
        public string[] chn;
    }


    private string path;
    public Dialogs logs;
    private string jsonstr;
    public int currentLang =1;
    public static LanguageHandler instance { get; private set; }

    private void Awake()
    {
        if(instance !=null)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        path = "./Assets/Language/Language.json";
        jsonstr = System.IO.File.ReadAllText(path);
        logs = JsonUtility.FromJson<Dialogs>(jsonstr);
        currentLang = 1;
        //Debug.Log(logs.eng[10]);
    }
    private void Update()
    {
        
    }
    public void ChangeLang(int lang)
    {
        currentLang = lang;
    }
    public string GetText(int logNum)
    {
        if(currentLang == 1)
        {
            return logs.eng[logNum];
        }
        else if(currentLang==2)
        {
            return logs.chn[logNum];
        }
        else
        {
            return "";
        }
    }

    
}
