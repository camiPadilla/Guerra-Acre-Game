using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
public class HUDManager : MonoBehaviour
{
    [SerializeField] GameObject interactuable;
    public static HUDManager instancia;
    [SerializeField] TMP_Text text;
    GameObject mensajeE;
    [SerializeField] GameObject pantallaNota;
    [SerializeField] GameObject pantallaPausa;
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
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pantallaNota.SetActive(false);
            Reanudar();
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

    public void Actualizar()
    {
    }
    public void LeerNota(string mensajeNuevo)
    {
        Time.timeScale = 0;
        text.text = mensajeNuevo;
        pantallaNota.SetActive(true);  

    }
    public void Reanudar()
    {
        Time.timeScale = 1;
    }
}
