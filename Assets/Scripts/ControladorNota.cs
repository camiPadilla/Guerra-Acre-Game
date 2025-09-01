using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorNota : ObjetoRecogible
{

    // Start is called before the first frame update
    
    private void OnDisable()
    {
        leer();
    }
    void leer()
    {
        Debug.Log("leyendo nota");
        //Time.timeScale = 0;
    }
}
