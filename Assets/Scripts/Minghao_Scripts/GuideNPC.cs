using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideNPC : MonoBehaviour
{
    [SerializeField] private Text dialog;
    private LanguageHandler lang;


    private IEnumerator ShowDialog()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
    public void ShowUp(int logID)
    {
        
        dialog.text = lang.GetText(logID);
        StartCoroutine("ShowDialog");
    }


    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        lang = LanguageHandler.instance;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Horizontal")>0)
        {
            transform.rotation = Quaternion.Euler(0,0,0);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
