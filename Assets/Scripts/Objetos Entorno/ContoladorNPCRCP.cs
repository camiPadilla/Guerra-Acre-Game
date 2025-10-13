using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class ContoladorNPCRCP : ObjetoRecogible
{
    public UnityEvent acciones;
    // Start is called before the first frame update
    void Start()
    {
       
    }
    public void Interactuar()
    {
        Debug.Log("el NPC anda interactuando");
        acciones.Invoke();
        this.DestruirObjeto();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
