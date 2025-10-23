using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorNota : ObjetoRecogible
{
    
    [SerializeField] string mensajeNota;
    [SerializeField] string ID;
    [SerializeField] bool tutorial;
    // Start is called before the first frame update
    public void leer()
    {
        InventarioManager player = FindFirstObjectByType<InventarioManager>();
        player.ActualizarNotas(ID);
        Debug.Log("leyendo nota");
        HUDManager.instancia.LeerNota(mensajeNota);
        if (!tutorial) this.DestruirObjeto();
            

    }
    private void OnTriggerEnter2D(Collider2D collision)
    { SoundEvents.RecogerNota?.Invoke(); } //Sonido by Chelo :D
}
