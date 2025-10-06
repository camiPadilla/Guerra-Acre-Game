using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorNPC : ObjetoRecogible
{
    [SerializeField] float tiempoEspera;
    bool dialogando = false;
    [SerializeField] Sprite MensajeNPC;
    [SerializeField] GameObject dialogo;
    // Start is called before the first frame update
    void Start()
    {
        dialogo.SetActive(false);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(dialogando == true)
        {
            //HUDManager.instancia.Ocultar();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { SoundEvents.HablarAliadoNPC?.Invoke(); } //Sound by Chelo :D

    public void Interactuar()
    {
        //Debug.Log("el NPC anda interactuando");
        if (dialogando == false)
        {
            dialogando = true;
            MostrarMensaje();
            StartCoroutine(nameof(tiempoMensaje));
        }


    }
    IEnumerator tiempoMensaje()
    {
        
        yield return new WaitForSeconds(tiempoEspera);
        dialogo.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        dialogando = false;
        

    }

    public void MostrarMensaje()
    {
        dialogo.GetComponent<SpriteRenderer>().sprite = MensajeNPC;
        dialogo.SetActive(true);
    }
}
