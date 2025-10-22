using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using PantallaCarga;
using UnityEditorInternal;
using System.Threading;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instancia;
    [Header("Elementos HUD")]
    [SerializeField] GameObject interactuable;
    [SerializeField] DialogosManager dialogosManager;
    
    [SerializeField] TMP_Text textoNota;
    [SerializeField]GameObject MensajeInteraccion;
    [Header("Pantallas")]
    [SerializeField] List<GameObject> menues;
    [SerializeField] GameObject menuPausa;
    [SerializeField] GameObject pantallaBienvenida;
    [SerializeField] GameObject HUDGame;
    [Header("Imagenes y barras")]
    [SerializeField] Sprite imagenClick;
    [SerializeField] Sprite imagenE;
    [SerializeField] Sprite imagenAumentoBala;
    [SerializeField] RectTransform barraVida;
    [SerializeField] RectTransform barraArmadura;
    [SerializeField] TextMeshProUGUI textoBalas;
    [SerializeField] TextMeshProUGUI textoTotalBalas;
    [SerializeField] List<GameObject> armas;
    [SerializeField] List<Sprite> imagenArmas;
    [SerializeField] GameObject hudBalas;
    [SerializeField] GameObject padreInteraccion;
    [SerializeField] MasterGameManager masterGameManager;
    private ControladorNPC npc;
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
        if (masterGameManager == null)
        {
            masterGameManager = FindObjectOfType<MasterGameManager>();
        }
        if (menuPausa == null)
        {
            menuPausa = GameObject.FindWithTag("canvas");
            foreach(Transform child in menuPausa.transform)
            {
                if (child.gameObject.name == "PantallaPausa")
                {
                    menuPausa = child.gameObject;
                }
            }

        }
        menuPausa.SetActive(false);
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.instancia.CerrarEstado();
        }

    }
    public void ReanudarPartida(int indice)
    {
        Reanudar();
        Debug.Log("vuelves al juego"); 
        HUDGame.SetActive(true);
        if (indice == 3)
        {
            menuPausa.SetActive(false);
            return;
        }
        menues[indice].SetActive(false);
        
    }
    
    public void ActivarRifle()
    {
        SoundEvents.RecogerArma.Invoke(); //Sonido by Chelo :D
        Debug.Log("hola activando arma");
        armas[2].GetComponent<UnityEngine.UI.Image>().sprite = imagenArmas[1];

    }
    public void AumentarBalas(Vector2 posicion)
    {
        SoundEvents.RecogerBalas.Invoke(); //Sonido by Chelo :D
        MensajeInteraccion.GetComponent<SpriteRenderer>().sprite = imagenAumentoBala;
        Vector2 posicionMensaje = new Vector2(posicion.x, posicion.y + 2f);
        MensajeInteraccion.transform.position = posicionMensaje;
        MensajeInteraccion.SetActive(true);
        //Debug.Log("ganaste 5 ");
        StartCoroutine("FadeOut", 0.25f);


    }
    IEnumerator FadeOut(float tiempoTotal)
    {
        SpriteRenderer miSpriteRenderer = MensajeInteraccion.GetComponent<SpriteRenderer>();
        Vector2 movement = Vector2.up * 0.5f * Time.deltaTime;
        float startTime = Time.time;

        while (Time.time - startTime < tiempoTotal)
        {
            MensajeInteraccion.transform.Translate(movement);
            float porcentaje = (Time.time - startTime) / tiempoTotal;
            Color color = miSpriteRenderer.color;
            color.a = Mathf.Lerp(1f, 0f, porcentaje);
            miSpriteRenderer.color = color;
            yield return null;
        }

        Color final = miSpriteRenderer.color;
        final.a = 0f;
        miSpriteRenderer.color = final;
        yield return new WaitForSeconds(0.5f);
        final.a = 1f;
        miSpriteRenderer.color = final;
        MensajeInteraccion.SetActive(false);
    }
    public void ActualizarArma(int armaActiva)
    {

        foreach (GameObject arma in armas)
        {
            UnityEngine.UI.Image imagenArma = arma.GetComponent<UnityEngine.UI.Image>();
            RectTransform tranformArma = arma.GetComponent<RectTransform>();
            if (armas.IndexOf(arma) == armaActiva)
            {
                tranformArma.sizeDelta = new Vector2(65, 50);
                tranformArma.anchoredPosition = new Vector2(-10, tranformArma.anchoredPosition.y);
                imagenArma.color = Color.white;
                if (armaActiva == 2)
                {
                    hudBalas.SetActive(true);
                }
                else
                {
                    hudBalas.SetActive(false);
                }
            }
            else
            {
                tranformArma.sizeDelta = new Vector2(55, 45);
                tranformArma.anchoredPosition = new Vector2(0, tranformArma.anchoredPosition.y);
                imagenArma.color = Color.gray;
            }
        }
    }

    public void ActualizarVida(int cantidadVidas)
    {
        Debug.Log("se acutalizar� la vida");
        barraVida.sizeDelta = new Vector2(26.5f * cantidadVidas, barraVida.sizeDelta.y);
    }
    public void ActualizarArmadura(int cantidadArmadura)
    {
        Debug.Log("se acutalizar� la armadura");
        barraArmadura.sizeDelta = new Vector2(cantidadArmadura * 53, barraArmadura.sizeDelta.y);
    }
    public void ActualizarTotalBalas(int cantidadTotal)
    {
        textoTotalBalas.text = "" + cantidadTotal;

    }
    public void ActualizarBalasActual(int cantidadBalas)
    {

        textoBalas.text = "" + cantidadBalas;
    }
    // Update is called once per frame
    void Start()
    {
        StartCoroutine(DarBienvenida());
        MensajeInteraccion = Instantiate(interactuable, padreInteraccion.transform);
        MensajeInteraccion.SetActive(false);
        padreInteraccion.transform.parent = MensajeInteraccion.transform;
        imagenE = MensajeInteraccion.GetComponent<SpriteRenderer>().sprite;
        MensajeInteraccion.GetComponent<SpriteRenderer>().sortingLayerName = "IU";
        ActualizarBalasActual(0);
        ActualizarTotalBalas(0);

    }
    public void MostrarInteraccion(Vector2 posicion, float imagen, string nombre)
    {
        if (nombre == "movible")
        {
            MensajeInteraccion.GetComponent<SpriteRenderer>().sprite = imagenClick;
        }
        else if (nombre == "recogible")
        {
            MensajeInteraccion.GetComponent<SpriteRenderer>().sprite = imagenE;
        }
        Vector2 posicionE = new Vector2(posicion.x, posicion.y + imagen * 2f + 0.5f);
        MensajeInteraccion.transform.position = posicionE;
        MensajeInteraccion.SetActive(true);

    }
    public void Ocultar()
    {
        MensajeInteraccion.SetActive(false);
    }

    
    public void LeerNota(string mensajeNuevo)
    {
        masterGameManager.DetenerTiempo();
        textoNota.text = mensajeNuevo;
        GameManager.instancia.CambiarDeEstado(3);
        menues[0].SetActive(true);
        HUDGame.SetActive(false);

    }
    public void IniciarDialogo(DialogosSO dialogo)
    {

        //DetenerTiempo();
        Debug.Log("hola deberias estar dialogando");
        GameManager.instancia.CambiarDeEstado(4);
        dialogosManager.IniciarDialogo(dialogo);
        menues[1].SetActive(true);
        HUDGame.SetActive(false);


    }
    public void Pausar()
    {
        masterGameManager.DetenerTiempo();
        menuPausa.SetActive(true);
        HUDGame.SetActive(false);
    //llamar al hud
    }
    public void Reanudar()
    {
        Time.timeScale = 1;
    }
    IEnumerator DarBienvenida()
    {
        pantallaBienvenida.SetActive(true);
        yield return new WaitForSeconds(3f);
        pantallaBienvenida.SetActive(false);
    }
}
