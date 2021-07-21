using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class LanguageHandler : MonoBehaviour
{

    public class Dialogs
    {
        // public string[] eng;
        //public string[] chn;
        public Dictionary<string, string> eng;
        public Dictionary<string, string> chn;
    }


    private string path;
    public Dialogs logs;
    private string jsonstr;
    public int currentLang =1;
    private LangManager manager;

    public static LanguageHandler instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
            instance = this;


        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        path = Application.streamingAssetsPath +"/Language.json";
        jsonstr = System.IO.File.ReadAllText(path);
        // logs = JsonUtility.FromJson<Dialogs>(jsonstr);
        logs = JsonConvert.DeserializeObject<Dialogs>(jsonstr);
        manager = LangManager.instance;
        currentLang = manager.curLang;

        //Debug.Log(logs.eng[10]);
        Debug.Log(Application.dataPath);
    }
    private void Update()
    {
        
    }
    public void ChangeLang(int lang)
    {
        currentLang = lang;
        manager.curLang = lang;

    }
    public string GetText(string logNum)
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
