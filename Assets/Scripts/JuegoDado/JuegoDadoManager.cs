using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class JuegoDadoManager : MonoBehaviour
{
    [SerializeField] private List<Dado> dados;
    [SerializeField] private VasoManager vaso;
    [SerializeField] private GameObject[] pantallas;
    private int escalera,full, poker, grande;
    private int[] puntos = new int[6];
    public int bonusPrimera;
    [SerializeField]
    private GameObject anote,anoteEnemigo;
    [SerializeField]
    private TextMeshProUGUI[] puntosUI;
    [SerializeField]
    private TextMeshProUGUI puntajeTotal;
    [SerializeField]
    private TextMeshProUGUI puntajeTotalEnemigo;
    [SerializeField] 
    private int cantidadTirosMax;
    private int puntaje, puntajeEnemigo;
    private int vueltas;
    private int cantidadTiros;
    private bool botones;
    
    //Camara
    [SerializeField] Transform targetEnemigo;
    [SerializeField] GameObject camara;
    Transform targetCamara;
    [SerializeField] float velCamara;
    
    //CosasEnemigo
    private int[] puntajesEnemigo;
    private bool[] anotablesEnemigo;
    [SerializeField] List<Dado> dadosEnemigo;



    public static JuegoDadoManager Instance { get; private set; }
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        cantidadTiros = 0;
    }
    private void Start()
    {
        CambiarPantalla(1);
        DesactivarDados();
        DesactivarVaso();
        targetCamara = transform;
    }
    private void Update()
    {
        if (Vector3.Distance(camara.transform.position, targetCamara.position) > 0.1f)
        {
            camara.transform.position = Vector3.MoveTowards(camara.transform.position, targetCamara.position, velCamara * Time.deltaTime);
        }
    }

    public void DesactivarDados()
    {
        for (int i = 0; i < dados.Count; i++)
        {
            dados[i].usable = false;
        }
    }
    public void ActivarDados()
    {
        for (int i = 0; i < dados.Count; i++)
        {
            dados[i].usable = true;
        }
    }
    public void DesactivarVaso()
    {
        vaso.usable = false;
    }
    public void ActivarVaso()
    {
        vaso.usable = true;
    }
    public void Tirar()
    {
        if (cantidadTiros <= cantidadTirosMax)
        {
            vueltas = 0;
            for (int i = 0; i < dados.Count; i++)
            {
                dados[i].Girar();
            }
            bonusPrimera = 5;
            ContarPuntos();
            ActivarDados();
            cantidadTiros++;
            vaso.usable = false;
            botones = true;
        }
    }
    public void ContarPuntos()
    {
        puntos = new int[6];
        escalera = full = poker = grande = 0;
        int[] frecuencia = new int[7];
        bool par = false;
        bool trio = false;
        bool cuatro = false; 
        bool cinco = false;

        for (int i = 0; i < dados.Count; i++)
        {
            int cara = dados[i].GetCara();
            frecuencia[cara]++;

        }
        for (int i = 1; i <= 6 ; i++)
        {
            puntos[i - 1] = i * frecuencia[i];
            if (frecuencia[i] == 2)
            {
                par = true;
            }else 
            if (frecuencia[i] == 3)
            {
                trio = true;
            }else
            if (frecuencia[i] == 4)
            {
                cuatro = true;
            }else
            if (frecuencia[i] == 5)
            {
                cinco = true;
            }
        }

        if ((frecuencia[2] == 1 && frecuencia[3] == 1 && frecuencia[4] == 1 && frecuencia[5] == 1 && frecuencia[6] == 1) ||
        (frecuencia[1] == 1 && frecuencia[2] == 1 && frecuencia[3] == 1 && frecuencia[4] == 1 && frecuencia[5] == 1))
        {
            escalera = 20 + bonusPrimera;
        }else if (par && trio)
        {
            full = 30 + bonusPrimera;
        }else if (cuatro)
        {
            poker = 40 + bonusPrimera;
        }else if (cinco)
        {
            if (bonusPrimera > 0)
            {
                Debug.Log("Ganaste por dormida");
            }
            else
            {
                grande = 50;
            }
        }
        MostrarPuntos();
    }
    private void MostrarPuntos()
    {
        for (int i = 0; i < 10; i++)
        {
            if (i < 6)
            {
                puntosUI[i].text = "" + puntos[i];
            }
            else
            {
                switch (i)
                {
                    case 6:
                        puntosUI[i].text = "" + escalera;
                        break;
                    case 7:
                        puntosUI[i].text = "" + full;
                        break;
                    case 8:
                        puntosUI[i].text = "" + poker;
                        break;
                    case 9:
                        puntosUI[i].text = "" + grande;
                        break;
                }
            }
        }
    }
    public bool PuedeVoltear()
    {
        if (vueltas < 2)
        {
            return true;
        }
        else
        {
            DesactivarDados();
            return false;
        }
    }
    public void AumentarVuelta()
    {
        vueltas++;
    }
    public void Anotar(int i)
    {
        if ((cantidadTiros < cantidadTirosMax) && botones)
        {
            if (i < 6)
            {
                puntaje = puntaje + puntos[i];
            }
            else
            {
                switch (i)
                {
                    case 6:
                        puntaje = puntaje + escalera;
                        break;
                    case 7:
                        puntaje = puntaje + full;
                        break;
                    case 8:
                        puntaje = puntaje + poker;
                        break;
                    case 9:
                        puntaje = puntaje + grande;
                        break;
                }
            }
            botones = false;
        }
        puntajeTotal.text = puntaje.ToString();

        //turno Oponente
        CamaraEnemigo();
        StartCoroutine(vaso.TiradaEnemigo());

        vaso.usable = true;
        if(cantidadTiros == cantidadTirosMax)
        {
            cantidadTiros++;
        }
        if (cantidadTiros > cantidadTirosMax)
        {
            EvaluarVictoria();
            vaso.usable = false;
        }
    }
    public void Prueba(Button boton)
    {
        if (!botones || !(cantidadTiros <= cantidadTirosMax))
        {
            boton.interactable = true;
        }
    }
    public void EvaluarVictoria()
    {
        if (puntaje > puntajeEnemigo)
        {
            Ganar();
        }
        else
        {
            Perder();
        }
    }
    public void Ganar()
    {
        Debug.Log("Ganaste");
    }
    public void Perder()
    {
        Debug.Log("Perdiste");

    }
    public void CambiarPantalla(int numero)
    {
        for (int i = 0; i < pantallas.Length; i++)
        {
            pantallas[i].SetActive(false);
            if(i == numero) pantallas[i].SetActive(true);
            if (i == 0) ActivarVaso();
        }
    }
    public void CamaraPropia()
    {
        targetCamara = transform;
    }
    public void CamaraEnemigo()
    {
        targetCamara = targetEnemigo;
    }
    // Enemigo
    private void ContarPuntosEnemigo()
    {

    }
}
