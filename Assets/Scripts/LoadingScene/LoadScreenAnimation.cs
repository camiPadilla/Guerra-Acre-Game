using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    public class LoadScreenAnimation : MonoBehaviour
{
    public static LoadScreenAnimation instance;

    [Header("Sprites de las interacciones")]
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private Image cosa1;
    [SerializeField] private Image cosa2;
    [SerializeField] private Image fade;
    [SerializeField] private RectTransform areaCanvas;
    [SerializeField] private GameObject avesCazadasOb;
    [SerializeField] private TMPro.TextMeshProUGUI avesCazadasText;
    private int avesCazadas = 0;

    public int tipoInteraccion;
    private bool aveCazada = false;

    //para los clones de los bolos de coca
    private void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    public void MiniJuegos()
    {

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

    private void Update()
    {
        MiniJuegos();
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
        yield return new WaitForSeconds(0.5f);
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
            InstanciarCoca();
            // cambiar animaciona aqui
        }
    }
    private void InstanciarCoca()
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

