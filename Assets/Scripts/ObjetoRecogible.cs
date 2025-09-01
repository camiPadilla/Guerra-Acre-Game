using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoRecogible : MonoBehaviour
{
    [SerializeField] string nombreObjeto;
    [SerializeField] int cantidad;
    public bool interactuable= false;
    public bool jugadorInteractuando=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            interactuable = true;
            Debug.Log("presione E para interactuar");
           
            jugadorInteractuando = collision.gameObject.GetComponent<InputPlayer>().getInteractuable();
        }
        
        PasarInformacion();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            interactuable = false;
        }
    }
    void PasarInformacion()
    {
        if (interactuable && jugadorInteractuando)
        {

            if (!transform.CompareTag("NPC"))
            {
                Debug.Log("conseguiste " + nombreObjeto);
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.GetComponent<ControladorNPC>().Interactuar();
            }
        }
        
    }
    public void setNombre(string nombreNuevo)
    {
        nombreObjeto = nombreNuevo;
    }
    public void setCantidad(int cantidadNuevo)
    {
        cantidad = cantidadNuevo;
    }

}
