using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    [SerializeField] GameObject menu;
    [SerializeField] GameObject coleccionables;
    [SerializeField] GameObject partida;
    [SerializeField] GameObject salir;
    [SerializeField] GameObject opciones;
    [SerializeField] GameObject MenuAudio;
    [SerializeField] GameObject controles;
    [SerializeField] GameObject video;

    [SerializeField] string escenaPartida;

    // Start is called before the first frame update
    void Start()
    {
        IrMenu();
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
        opciones.SetActive(false);
        MenuAudio.SetActive(false);
        controles.SetActive(false);
        video.SetActive(false);
    }
    public void IrColeccionables()
    {
        menu.SetActive(false);
        coleccionables.SetActive(true);
        partida.SetActive(false);
        salir.SetActive(false);
        opciones.SetActive(false);
        MenuAudio.SetActive(false);
        controles.SetActive(false);
        video.SetActive(false);
    }
    public void IrMenuPartida()
    {
        menu.SetActive(false);
        coleccionables.SetActive(false);
        partida.SetActive(true);
        salir.SetActive(false);
        opciones.SetActive(false);
        MenuAudio.SetActive(false);
        controles.SetActive(false);
        video.SetActive(false);
    }
    public void IrSalir()
    {
        menu.SetActive(false);
        coleccionables.SetActive(false);
        partida.SetActive(false);
        salir.SetActive(true);
        opciones.SetActive(false);
        MenuAudio.SetActive(false);
        controles.SetActive(false);
        video.SetActive(false);
    }
    public void IrOpciones()
    {
        menu.SetActive(false);
        coleccionables.SetActive(false);
        partida.SetActive(false);
        salir.SetActive(false);
        opciones.SetActive(true);
        MenuAudio.SetActive(false);
        controles.SetActive(false);
        video.SetActive(false);
    }
    public void IrAudio()
    {
        menu.SetActive(false);
        coleccionables.SetActive(false);
        partida.SetActive(false);
        salir.SetActive(false);
        opciones.SetActive(false);
        MenuAudio.SetActive(true);
        controles.SetActive(false);
        video.SetActive(false);
    }
    public void IrControles()
    {
        menu.SetActive(false);
        coleccionables.SetActive(false);
        partida.SetActive(false);
        salir.SetActive(false);
        opciones.SetActive(false);
        MenuAudio.SetActive(false);
        controles.SetActive(true);
        video.SetActive(false);
    }
    public void IrVideo()
    {
        menu.SetActive(false);
        coleccionables.SetActive(false);
        partida.SetActive(false);
        salir.SetActive(false);
        opciones.SetActive(false);
        MenuAudio.SetActive(false);
        controles.SetActive(false);
        video.SetActive(true);
    }
    public void Desactivado()
    {
        menu.SetActive(false);
        coleccionables.SetActive(false);
        partida.SetActive(false);
        salir.SetActive(false);
        opciones.SetActive(false);
        MenuAudio.SetActive(false);
        controles.SetActive(false);
        video.SetActive(false);
    }
    public void Salir()
    {
        Application.Quit();
    }
    public void Comezar()
    {
        SceneManager.LoadScene(escenaPartida);
    }
}
