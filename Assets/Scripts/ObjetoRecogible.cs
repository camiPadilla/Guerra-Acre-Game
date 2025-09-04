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
<<<<<<< Updated upstream
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
=======
    public void ObtenerTamaño(){
                imagenSize = this.GetComponent<SpriteRenderer>().bounds.extents.y;
>>>>>>> Stashed changes
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
<<<<<<< Updated upstream
            interactuable = true;
            Debug.Log("presione E para interactuar");
           
            jugadorInteractuando = collision.gameObject.GetComponent<InputPlayer>().getInteractuable();
=======
            ObtenerTamaño();
            HUDManager.instancia.MostrarInteraccion(transform.position, 1f);
            if (collision.gameObject.GetComponent<InputPlayer>().getInteractuable())
            {
                collision.gameObject.SendMessage("RecibirInfo", nombreObjeto);
                gameObject.SetActive(false);
            }
>>>>>>> Stashed changes
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
            Debug.Log("conseguiste " + nombreObjeto);
            gameObject.SetActive(false);
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
