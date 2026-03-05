using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjetoRecogible : MonoBehaviour
{
    [SerializeField] string nombreObjeto;
    float imagenSize;
    bool desactivado;
    public  Animator animator;
    [SerializeField] GameObject[] variante;
    public int objId;
    public TutorialObjeto tuto;
    public bool tutOb;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void ObtenerTamaño()
    {
        //imagenSize = this.GetComponent<SpriteRenderer>().bounds.extents.y;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (nombreObjeto == "armadura")
            {
                int armadura = collision.gameObject.GetComponent<SaludPersonaje>().GetArmaduraJugador();
                if (armadura <= 0) 
                { 
                    variante[0].SetActive(true); 
                    variante[1].SetActive(false); 
                }
                else if (armadura >= 1) 
                { 
                    variante[0].SetActive(false);
                    variante[1].SetActive(true);
                }
            }
            HUDManager.instancia.MostrarInteraccion(transform.position, 0.8f, "recogible");
            if (animator != null)
            {
                animator.SetBool("interactuable", true);
            }else
            {
                Debug.Log("No tiene animator");
            }
            if (collision.gameObject.GetComponent<InputPlayer>().getInteractuable())
            {
                if(nombreObjeto == "botiquin" || nombreObjeto == "arma")
                {
                    if(tutOb == true)
                    {
                        Debug.Log("Soy un tutorial");
                        tuto.TutorialObjUI();
                    }
                }
                if (nombreObjeto != "NPC" && nombreObjeto != "nota" )
                {
                    Debug.Log("hola");
                    
                    collision.gameObject.SendMessage("RecibirInfo", nombreObjeto);
                    DestruirObjeto();
                    desactivado = true;
                }
                
                else if (nombreObjeto == "nota")
                {

                    SendMessage("Leer");

                }
                else
                {
                    //Debug.Log("hola quisiste interactuar con migo el npc");
                    SendMessage("Interactuar");
                    return;
                }
            }
        }
    }
    public void DestruirObjeto()
    {
        if(nombreObjeto != "NPC")
        {
            MasterGameManager.instance.ObjetosRecogidos(objId);
            gameObject.SetActive(false);
        }    
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player") && gameObject !=null && (HUDManager.instancia != null))
        {
            HUDManager.instancia.Ocultar();
            if (animator != null)
            {
                animator.SetBool("interactuable", false);
            }
            if (desactivado == true && nombreObjeto == "balas")
            {
                HUDManager.instancia.AumentarBalas(transform.position);
            }
            if (nombreObjeto == "armadura")
            {
                variante[0].SetActive(true);
                variante[1].SetActive(true);
            }
        }
    }

}