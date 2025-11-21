using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorNota : ObjetoRecogible
{
    [SerializeField] NotasSO nota;
    [SerializeField] string mensajeNota;
    
    [SerializeField] bool tutorial;
    // Start is called before the first frame update
    private void Start()
    {
        if (nota.obtenida)
        {
            this.gameObject.SetActive(false);
        }
    }
    public void Leer()
    {        
        InventarioManager player = FindFirstObjectByType<InventarioManager>();
        if (!tutorial)
        {
            player.ActualizarNotas(nota.ID);
            ControladorEscena escena = FindFirstObjectByType<ControladorEscena>();
            escena.ObtenerNota(nota);
            HUDManager.instancia.LeerNota(nota.notaImagen);
            this.DestruirObjeto();
        }
        Debug.Log("leyendo nota");
        if (tutorial)
        {
            HUDManager.instancia.LeerNotaTutorial(mensajeNota);
        }
        
        SoundEvents.DetenerPasosPasto.Invoke(); //Sonido by Chelo :D
        if (nota.name.ToLower().Contains("null")) SoundEvents.LeerSimple.Invoke();
        else SoundEvents.LeerColeccionable.Invoke(); //Sonido by Chelo :D
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        SoundEvents.RecogerNota.Invoke(); //Sonido by Chelo :D
    }
}
