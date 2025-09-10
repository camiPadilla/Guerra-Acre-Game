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
    [SerializeField] GameObject HUDGameplay;
    [SerializeField] GameObject pantallaMuerte;
    [SerializeField] Sprite imagenClick;
    [SerializeField] Sprite imagenE;
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
            Pausar();
        }

    }
    // Update is called once per frame
    void Start()
    {

        mensajeE = Instantiate(interactuable, transform); 
        mensajeE.SetActive(false);
        imagenE = mensajeE.GetComponent<SpriteRenderer>().sprite;

    }
    public void MostrarInteraccion(Vector2 posicion, float imagen, string nombre)
    {
        if (nombre == "movible")
        {
            mensajeE.GetComponent<SpriteRenderer>().sprite = imagenClick;
        } else if (nombre == "recogible")
        {
            mensajeE.GetComponent<SpriteRenderer>().sprite = imagenE;
        }
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
        DetenerTiempo();
        text.text = mensajeNuevo;
        HUDGameplay.SetActive(false);
        pantallaNota.SetActive(true);  
        
    }
    public void Pausar()
    {
        DetenerTiempo();
        pantallaPausa.SetActive(true);
        HUDGameplay.SetActive(false);
    }
    void DetenerTiempo()
    {
        Time.timeScale = 0;
    }
    public void Reanudar()
    {
        Time.timeScale = 1;
        HUDGameplay.SetActive(true);
    }
    public void MostrarPantallaMuerte()
    {
        pantallaMuerte.SetActive(true);
        DetenerTiempo();
        HUDGameplay.SetActive(false) ;
    }
}
