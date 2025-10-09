using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class RCPManager : MonoBehaviour
{
    [SerializeField] GameObject barraPrefab;
    [SerializeField] List<RhythmSO> ritmos;
    [SerializeField] List<GameObject> barrasLista;
    [SerializeField] int cantidadBarras;
    static public RCPManager instancia;
    [SerializeField] Transform _target;
    [SerializeField] int puntosGanar;
    int puntosActual;
    int conteoDeBalas=0;
    // Start is called before the first frame update


    
    private void Awake()
    {
        if (instancia != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instancia = this;
        }
    }
    void Start()
    {
        InstanciarBarras();
        InvokeRepeating(nameof(ConfigurarBarra), 1f, 1.6f);
        
        //Invoke(nameof(ConfigurarBarra),1f);
    }
    public void AumentarPuntos(int incremento)
    {
        puntosActual += incremento;
        Debug.Log("se aumentaron puntos");
    }
    void InstanciarBarras()
    {
        for (int i = 0; i < cantidadBarras; i++)
        {
            barrasLista.Add(Instantiate(barraPrefab, transform.position, transform.rotation));
        }
    }
    public GameObject ObtenerBarra()
    {
        GameObject nuevaBarra = null;
        foreach (var barraRecorrer in barrasLista)
        {
            if (barraRecorrer.gameObject.activeInHierarchy == false)
            {
                nuevaBarra = barraRecorrer;
                break;
            }
        }


        return nuevaBarra;
    }
    public void DevolverBarra(GameObject barra)
    {
        barra.transform.position = transform.position;
        barra.SetActive(false);
        
    }
    
    public void ConfigurarBarra()
    {
        conteoDeBalas++;
        Debug.Log("pasaron " + conteoDeBalas);
        GameObject nuevaBarra = ObtenerBarra();
        nuevaBarra.SetActive(true);
        ControladorBarra _micontroladorBarra = nuevaBarra.GetComponent<ControladorBarra>();
        _micontroladorBarra.posicionInicial = transform;
        _micontroladorBarra.target = _target;
        int IndiceRandom = Random.Range(0, ritmos.Count);
        _micontroladorBarra.tiempoBarra = ritmos[IndiceRandom];
        ;
    }
    private void Update()
    {
        if (puntosActual == puntosGanar)
        {
            CancelInvoke();
            Debug.Log("Salvaste al boliviano");
            Time.timeScale = 0;
        }
        else if (conteoDeBalas == 15)
        {
            CancelInvoke();
            Debug.Log("murio el boliviano");
            Time.timeScale = 0;
        }

    }

}
