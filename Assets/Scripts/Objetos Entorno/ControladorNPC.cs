using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorNPC : ObjetoRecogible
{
    [SerializeField] DialogosSO dialogo;
    
    // Start is called before the first frame update
    
    // Update is called once per frame
    
    public void Interactuar()
    {
        Debug.Log("estas en una interaccion de un NPC");
        
        if (GameManager.instancia.Onplaying())
        {
            MostrarMensaje();
            
        }


    }
    

    public void MostrarMensaje()
    {
        SoundEvents.HablarAliadoNPC?.Invoke(); //Sonido by Chelo :D
        HUDManager.instancia.IniciarDialogo(dialogo);
    }
}
