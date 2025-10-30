using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorNota : ObjetoRecogible
{
    [SerializeField] NotasSO nota;
    [SerializeField] string mensajeNota;
    
    [SerializeField] bool tutorial;
    // Start is called before the first frame update
    public void leer()
    {
        InventarioManager player = FindFirstObjectByType<InventarioManager>();
        if (!tutorial)
        player.ActualizarNotas(nota.ID);
        Debug.Log("leyendo nota");
        if (tutorial)
        {
            HUDManager.instancia.LeerNotaTutorial(mensajeNota);
        }
        else
        {
            HUDManager.instancia.LeerNota(nota.notaImagen);
            this.DestruirObjeto();
        }
           
        
            

    }
    private void OnTriggerEnter2D(Collider2D collision)
    { SoundEvents.RecogerNota?.Invoke(); } //Sonido by Chelo :D
}
