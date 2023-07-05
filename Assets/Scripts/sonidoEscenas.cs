using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sonidoEscenas : MonoBehaviour
{
    private sonidoEscenas instace;
    public sonidoEscenas Instance
    {
        get
        {
            return instace;
        }
    }

    private void Awake()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }

        if(instace!= null && instace != this) 
        {
            Destroy(gameObject);
            return;
        } 
        else
        {
            instace = this;
        }

        DontDestroyOnLoad(gameObject);
    }
    
}
