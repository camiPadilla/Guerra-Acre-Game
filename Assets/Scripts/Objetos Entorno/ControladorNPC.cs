using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorNPC : ObjetoRecogible
{
    [SerializeField] float tiempoEspera;
    bool dialogando = false;
    //[SerializeField] Sprite MensajeNPC;
    [SerializeField] DialogosSO dialogo;
    int cont = 0;
    // Start is called before the first frame update
    void Start()
    {
        //dialogo.SetActive(false);
    }

    // Update is called once per frame

    public void Interactuar()
    {
        Debug.Log("estas en una interaccion de un NPC");
        cont++;
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