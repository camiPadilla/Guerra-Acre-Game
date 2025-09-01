using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorNPC : ObjetoRecogible
{
    [SerializeField] string mensaje;
    [SerializeField] float tiempoEspera;
    bool dialogando= false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    public void Interactuar()
    {
        
        if(dialogando == false)
        {
            dialogando = true;
            Debug.Log(mensaje);
            StartCoroutine(nameof(tiempoMensaje));
        }
        
        
    }
    IEnumerator tiempoMensaje()
    {
       
        yield return new WaitForSeconds(tiempoEspera);
        dialogando = false;

    }
}
