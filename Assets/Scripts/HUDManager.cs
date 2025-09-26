using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] GameObject interactuable;
    public static HUDManager instancia;
    [SerializeField] TMP_Text text;
    GameObject mensajeE;
    [SerializeField] GameObject pantallaNota;
    [SerializeField] GameObject pantallaPausa;
    [SerializeField] GameObject HUDGameplay;
    [SerializeField] GameObject PantallaFin;
    [SerializeField] GameObject pantallaMuerte;
    [SerializeField] GameObject pantallaCarga;
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
    //--------------------------------------------
    [Header("Sprites de las interacciones")]
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private Image cosa1;
    [SerializeField] private Image cosa2;
    [SerializeField] private RectTransform areaCanvas;
    [SerializeField] private GameObject avesCazadasOb;
    [SerializeField] private TMPro.TextMeshProUGUI avesCazadasText;
    private int avesCazadas = 0;
    private int tipoInteraccion;
    private bool aveCazada = false;
    //--------------------------------------------

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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pausar();
        }

    }
    public void ActivarRifle()
    {
        Debug.Log("hola activando arma");
        armas[2].GetComponent<UnityEngine.UI.Image>().sprite = imagenArmas[1];

    }
    public void AumentarBalas(Vector2 posicion)
    {
        mensajeE.GetComponent<SpriteRenderer>().sprite = imagenAumentoBala;
        Vector2 posicionMensaje = new Vector2(posicion.x, posicion.y + 2f);
        mensajeE.transform.position = posicionMensaje;
        mensajeE.SetActive(true);
        Debug.Log("ganaste 5 ");
        StartCoroutine("FadeOut", 0.25f);


    }
    IEnumerator FadeOut(float tiempoTotal)
    {
        SpriteRenderer miSpriteRenderer = mensajeE.GetComponent<SpriteRenderer>();
        Vector2 movement = Vector2.up * 0.5f * Time.deltaTime;
        float startTime = Time.time;

        while (Time.time - startTime < tiempoTotal)
        {
            mensajeE.transform.Translate(movement);
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
        mensajeE.SetActive(false);
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

        mensajeE = Instantiate(interactuable, transform);
        mensajeE.SetActive(false);
        imagenE = mensajeE.GetComponent<SpriteRenderer>().sprite;
        mensajeE.GetComponent<SpriteRenderer>().sortingOrder = 4;
        ActualizarBalasActual(0);
        ActualizarTotalBalas(0);

    }
    public void MostrarInteraccion(Vector2 posicion, float imagen, string nombre)
    {
        if (nombre == "movible")
        {
            mensajeE.GetComponent<SpriteRenderer>().sprite = imagenClick;
        }
        else if (nombre == "recogible")
        {
            mensajeE.GetComponent<SpriteRenderer>().sprite = imagenE;
        }
        //Debug.Log("mostrado");
        Vector2 posicionE = new Vector2(posicion.x, posicion.y + imagen * 2f);
        mensajeE.transform.position = posicionE;
        mensajeE.SetActive(true);

    }
    public void Ocultar()
    {
        //Debug.Log("ocultado");
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
        HUDGameplay.SetActive(false);
    }
    public void MostrarPantallaCarga()
    {
        Randomizar();
        DetenerTiempo();
        pantallaCarga.SetActive(true);
        HUDGameplay.SetActive(false);
        StartCoroutine(Cargando());
    }
    //METODOS DE LA PANTALLA DE CARGA
    IEnumerator Cargando()
    {
        yield return new WaitForSeconds(4f);
        MiniJuegos();
    }
    private void MiniJuegos()
    {
        // Elegir interacción al azar
        tipoInteraccion = Random.Range(0, 3);

        if (tipoInteraccion == 0)
        {
            avesCazadasOb.SetActive(true);
            // tigrillo y ave
            cosa1.sprite = sprites[0];
            cosa2.sprite = sprites[1];
        }
        else if (tipoInteraccion == 1)
        {
            avesCazadasOb.SetActive(false);
            // soldado comiendo coca
            cosa1.sprite = sprites[2];
            cosa2.sprite = sprites[3];
        }
        else
        {
            avesCazadasOb.SetActive(false);
            // soldado bailando
            cosa1.sprite = sprites[4];
            cosa2.sprite = sprites[5];
        }
    }
    private void Randomizar()
    {
        if (tipoInteraccion == 0) Tigrillo();
        else if (tipoInteraccion == 1) SoldadoComiendo();
        else SoldadoBailando();
    }
    private void Tigrillo()
    {

        if (Input.GetKeyDown(KeyCode.Space) && !aveCazada)
        {
            Debug.Log("¡El tigrillo cazó al ave!");
            aveCazada = true;

            cosa1.rectTransform.position = new UnityEngine.Vector3(cosa2.rectTransform.position.x, cosa2.rectTransform.position.y, 0);
            if (aveCazada)
            {
                avesCazadas++;
                avesCazadasText.text = avesCazadas.ToString();
                cosa1.sprite = sprites[6];
                StartCoroutine(RespawnAve());
            }
            else
            {
                cosa1.sprite = sprites[0];
            }
        }
    }

    private IEnumerator RespawnAve()
    {
        aveCazada = false;
        yield return new WaitForSeconds(0.3f);
        float ancho = areaCanvas.rect.width / 2f;
        float alto = areaCanvas.rect.height / 2f;
        float posX = Random.Range(-ancho, ancho);
        float posY = Random.Range(-alto, alto);
        cosa2.rectTransform.localPosition = new Vector3(posX, posY, 0);
        
    }

    private void SoldadoComiendo()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //animacion de comer coca
            Debug.Log("El soldado mastica coca");
            InstanciarComida();
            // cambiar animaciona aqui
        }
    }
    private void InstanciarComida()
    {
        Image comida = Instantiate(cosa2, areaCanvas);
        RectTransform rt = comida.GetComponent<RectTransform>();
        float x = Random.Range(-200f, 200f);
        float y = Random.Range(-100f, 100f);
        rt.anchoredPosition = new Vector2(x, y);
    }

    private void SoldadoBailando()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //solo aumento la velodidad de la animación ya que debe bailar rapido jsjsjss
            Debug.Log("El soldado se pone a bailar");

        }
    }
}
