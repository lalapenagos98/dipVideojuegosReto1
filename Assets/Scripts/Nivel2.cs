using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Nivel2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void Openscene()
    {
        Destroy(GameObject.Find("sonidoEscenas"));
        SceneManager.LoadScene("Nivel 2");
    }
}