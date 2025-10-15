using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjetoRecogible : MonoBehaviour
{
    [SerializeField] string nombreObjeto;
    float imagenSize;
    bool desactivado;
    // Start is called before the first frame update
    public void ObtenerTamaño()
    {
        //imagenSize = this.GetComponent<SpriteRenderer>().bounds.extents.y;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            
            HUDManager.instancia.MostrarInteraccion(transform.position, 0.8f, "recogible");
            if (collision.gameObject.GetComponent<InputPlayer>().getInteractuable())
            {
                if (nombreObjeto != "NPC" && nombreObjeto != "nota")
                {
                    collision.gameObject.SendMessage("RecibirInfo", nombreObjeto);
                    DestruirObjeto();
                    desactivado = true;
                }
                else if (nombreObjeto == "nota")
                {

                    SendMessage("leer");
                    
                }
                else
                {
                    SendMessage("Interactuar");
                    return;
                }
                
            }
        }
    }
    public void DestruirObjeto()
        {
        gameObject.SetActive(false);
        }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player") && gameObject !=null)
        {
            HUDManager.instancia.Ocultar();
            if (desactivado == true && nombreObjeto == "balas")
            {
                HUDManager.instancia.AumentarBalas(transform.position);
            }


        }
    }



    

}