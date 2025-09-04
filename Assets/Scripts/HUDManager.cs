using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] GameObject interactuable;
    public static HUDManager instancia;
    GameObject mensajeE;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Start()
    {
        mensajeE = Instantiate(interactuable, transform);
        mensajeE.SetActive(false);
    }
    public void MostrarInteraccion(Vector2 posicion, float imagen)
    {
        Debug.Log("mostrado");
        Vector2 posicionE= new Vector2(posicion.x, posicion.y + imagen*2f);
        mensajeE.transform.position = posicionE;
        mensajeE.SetActive(true);

    }
    public void Ocultar()
    {
        Debug.Log("ocultado");
        mensajeE.SetActive(false);
    }
}
