using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorNota : ObjetoRecogible
{
    
    [SerializeField] string mensajeNota;
    // Start is called before the first frame update
    public void leer()
    {
        
        Debug.Log("leyendo nota");
        HUDManager.instancia.LeerNota(mensajeNota);
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    { SoundEvents.RecogerNota?.Invoke(); }
}
