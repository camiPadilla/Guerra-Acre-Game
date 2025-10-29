using PantallaCarga;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;
using TarodevController;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class ControladorEscena : MonoBehaviour
{
    public int ScenType;
    /*
    con esto me refiero a que si el tipo de escena es 0 es el menu, y si el de 1 o 2  sera 
    */
    public int sceneIndex;
    public LoaderScene loaderScene;
    public SaludPersonaje vidas;
    public InventarioManager cantidadNotas;
    public AtaquePersonaje balas;
    public GameManager gameManager;
    [SerializeField] private GameObject MenuInGame;
    [SerializeField] MasterGameManager masterGameManager;
    [SerializeField] PlayerController player;
    [SerializeField]public int ChPoint;
    [SerializeField] string nameScene;
    //contarEnemigos
    //-------textos para ui------
    public TMP_Text textVidas;
    public TMP_Text textNot;
    public TMP_Text textBalas;
    public TMP_Text textEnemigos;
    //Para los ajustes pipipi

    private void Start()
    {

        if (loaderScene == null)
        {
            loaderScene = FindObjectOfType<LoaderScene>();
        }
        nameScene = ConstantsGame.SCENAUNO;
    }
    public void Update()
    {
        ProgresoFinal();
    }
    public void ProgresoFinal()
    {
        textVidas.text = vidas.vidasJugador.ToString();
        textNot.text = cantidadNotas.cantNotas.ToString();
        textBalas.text = balas.GettotalBalas().ToString();
        textEnemigos.text =gameManager.enemigosMuertos.ToString();
    }

    public void SiguienteNivel()
    {
        if (sceneIndex == 1)
        {
            loaderScene.LoadSceneString(ConstantsGame.SCENADOS);
            
        }
        else if(sceneIndex == 2) 
        {
                loaderScene.LoadSceneString(ConstantsGame.SCENECREDITS);
        }
        //procede con un swich con mas niveles pero como solo son 2 , po aqui nomas jsahdsafsa
    }
    public void GuardarPartida()
    {
        GameData data = new GameData(vidas, balas, player, sceneIndex, nameScene, ChPoint,masterGameManager.currentSlot);
        SaveLoadSystem.SaveGame(data,masterGameManager.currentSlot);
    }

    public void ReiniciarNivel()
    {
        loaderScene.LoadSceneString(ConstantsGame.SCENAUNO);
    }
    public void Salir()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    public void IrMenu()
    {
        loaderScene.LoadSceneString(ConstantsGame.SCENEMAINMENU);
        Destroy(MenuPausa.instance?.gameObject);
        Destroy(HUDManager.instancia?.gameObject);
        Destroy(MenuInGame);
    }
    public void CrearPart()
    {
        masterGameManager.NewGame();
    }

}