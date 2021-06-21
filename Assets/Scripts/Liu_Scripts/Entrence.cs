using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrence : MonoBehaviour
{
    public string entrancePassword;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerStatus.instance.scenenPassword == entrancePassword)
        {
            PlayerStatus.instance.transform.position = transform.position;//entrance position
        }
    }


}
