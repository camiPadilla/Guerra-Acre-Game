using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorNota : ObjetoRecogible
{
    [SerializeField] string mensajeNota;
    // Start is called before the first frame update
    
    private void OnDisable()
    {
        leer();
    }
    void leer()
    {
        Debug.Log("leyendo nota");
        HUDManager.instancia.LeerNota(mensajeNota);
        //Time.timeScale = 0;
    }
}
