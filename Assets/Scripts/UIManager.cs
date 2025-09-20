using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject coleccionables;
    [SerializeField] GameObject partida;
    [SerializeField] GameObject salir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void IrMenu()
    {
        menu.SetActive(true);
        coleccionables.SetActive(false);
        partida.SetActive(false);
        salir.SetActive(false);
    }
    public void IrColeccionables()
    {
        menu.SetActive(false);
        coleccionables.SetActive(true);
        partida.SetActive(false);
        salir.SetActive(false);
    }
    public void IrMenuPartida()
    {
        menu.SetActive(false);
        coleccionables.SetActive(false);
        partida.SetActive(true);
        salir.SetActive(false);
    }
    public void IrSalir()
    {
        menu.SetActive(false);
        coleccionables.SetActive(false);
        partida.SetActive(false);
        salir.SetActive(true);
    }
    public void Salir()
    {
        Application.Quit();
    }
    public void Comezar()
    {
         
    }
}
