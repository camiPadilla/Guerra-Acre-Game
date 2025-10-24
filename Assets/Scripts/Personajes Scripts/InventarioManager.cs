using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventarioManager : MonoBehaviour
{
    int balas = 0;

    int dinamitas;
    [SerializeField] int limiteBalas;
    [SerializeField] int aumento;
    [SerializeField] List<string> listaNotas;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public int GetBalas()
    {
        return balas;
    }
    public void SetBalas(int nuevaCantidad)
    {
        balas = nuevaCantidad;
        if(balas > limiteBalas)
        {
            balas = limiteBalas;
        }
        HUDManager.instancia.ActualizarTotalBalas(balas);


    }
    public void RecibirInfo(string nombre)
    {
        switch (nombre)
        {
            case "balas":
                SetBalas(balas += aumento);
                Debug.Log("ahora el jugador tiene en balas " + balas);
                break;
            case "botiquin":
                SendMessage("Curarse");
                break;
            case "armadura":
                SendMessage("ObtenerArmadura");
                break;
            case "dinamita":
                
                dinamitas++;
                if (dinamitas > 5) dinamitas = 5;
                Debug.Log("el jugador tiene en dinamita " + dinamitas);
                break;
            case "arma":
                SendMessage("ObtenerArma");

                break;
        }


    }
    public void ActualizarNotas(string ID)
    {
        listaNotas.Add(ID);
    }
}
