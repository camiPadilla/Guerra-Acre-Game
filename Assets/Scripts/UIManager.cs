using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class UIManager : MonoBehaviour
{
    [SerializeField] List<GameObject> pantallasMenu;
    

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
        pantallasMenu[0].SetActive(true);
        pantallasMenu[1].SetActive(false);
        pantallasMenu[2].SetActive(false);
        pantallasMenu[3].SetActive(false);
        pantallasMenu[4].SetActive(false);
        pantallasMenu[5].SetActive(false);
       pantallasMenu[6].SetActive(false);
        pantallasMenu[7].SetActive(false);
    }
    public void IrColeccionables()
    {
        pantallasMenu[0].SetActive(false);
        pantallasMenu[1].SetActive(true);
        pantallasMenu[2].SetActive(false);
        pantallasMenu[3].SetActive(false);
        pantallasMenu[4].SetActive(false);
       pantallasMenu[5].SetActive(false);
        pantallasMenu[6].SetActive(false);
        pantallasMenu[7].SetActive(false);
    }
    public void IrMenuPartida()
    {
         pantallasMenu[0].SetActive(false);
        pantallasMenu[1].SetActive(false);
        pantallasMenu[2].SetActive(true);
        pantallasMenu[3].SetActive(false);
        pantallasMenu[4].SetActive(false);
        pantallasMenu[5].SetActive(false);
        pantallasMenu[6].SetActive(false);
        pantallasMenu[7].SetActive(false);
    }
    public void IrSalir()
    {
         pantallasMenu[0].SetActive(false);
        pantallasMenu[1].SetActive(false);
        pantallasMenu[2].SetActive(false);
        pantallasMenu[3].SetActive(true);
        pantallasMenu[4].SetActive(false);
        pantallasMenu[5].SetActive(false);
       pantallasMenu[6].SetActive(false);
        pantallasMenu[7].SetActive(false);
    }
    public void IrOpciones()
    {
        pantallasMenu[0].SetActive(false);
        pantallasMenu[1].SetActive(false);
        pantallasMenu[2].SetActive(false);
        pantallasMenu[3].SetActive(false);
        pantallasMenu[4].SetActive(true);
        pantallasMenu[5].SetActive(false);
        pantallasMenu[6].SetActive(false);
        pantallasMenu[7].SetActive(false);
    }
    public void IrAudio()
    {
        pantallasMenu[0].SetActive(false);
        pantallasMenu[1].SetActive(false);
        pantallasMenu[2].SetActive(false);
        pantallasMenu[3].SetActive(false);
        pantallasMenu[4].SetActive(false);
        pantallasMenu[5].SetActive(true);
        pantallasMenu[6].SetActive(false);
        pantallasMenu[7].SetActive(false);
    }
    public void IrControles()
    {
         pantallasMenu[0].SetActive(false);
        pantallasMenu[1].SetActive(false);
        pantallasMenu[2].SetActive(false);
        pantallasMenu[3].SetActive(false);
        pantallasMenu[4].SetActive(false);
        pantallasMenu[5].SetActive(false);
        pantallasMenu[6].SetActive(true);
        pantallasMenu[7].SetActive(false);
    }
    public void IrVideo()
    {
         pantallasMenu[0].SetActive(false);
        pantallasMenu[1].SetActive(false);
        pantallasMenu[2].SetActive(false);
        pantallasMenu[3].SetActive(false);
        pantallasMenu[4].SetActive(false);
        pantallasMenu[5].SetActive(false);
        pantallasMenu[6].SetActive(false);
        pantallasMenu[7].SetActive(true);
    }
    public void Desactivado()
    {
         pantallasMenu[0].SetActive(false);
         pantallasMenu[1].SetActive(false);
        pantallasMenu[2].SetActive(false);
        pantallasMenu[3].SetActive(false);
        pantallasMenu[4].SetActive(false);
       pantallasMenu[5].SetActive(false);
        pantallasMenu[6].SetActive(false);
        pantallasMenu[7].SetActive(false);
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
