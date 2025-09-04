using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoRecogible : MonoBehaviour
{
    [SerializeField] string nombreObjeto;
    float imagenSize;
    // Start is called before the first frame update
    public void ObtenerTama�o(){
                imagenSize = this.GetComponent<SpriteRenderer>().bounds.extents.y;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            ObtenerTama�o();
            HUDManager.instancia.MostrarInteraccion(transform.position, 1f);
            if (collision.gameObject.GetComponent<InputPlayer>().getInteractuable())
            {
                collision.gameObject.SendMessage("RecibirInfo", nombreObjeto);
                gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            HUDManager.instancia.Ocultar();
        }
    }
   
    
    public void setNombre(string nombreNuevo)
    {
        nombreObjeto = nombreNuevo;
    }
    

}