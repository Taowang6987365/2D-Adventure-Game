using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LangManager : MonoBehaviour
{
    public static LangManager instance;
    public int curLang = 1;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
