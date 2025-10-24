using PantallaCarga;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TarodevController;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ControladorEscena : MonoBehaviour
{
    public int sceneIndex;
    public LoaderScene loaderScene;
    public SaludPersonaje vidas;
    public SaludPersonaje escudos;
    public AtaquePersonaje balas;
    [SerializeField] PlayerController player;
    [SerializeField]public int ChPoint;
    [SerializeField] string nameScene;
    //contarEnemigos
    //-------textos para ui------
    public TMP_Text textVidas;
    public TMP_Text textEsc;
    public TMP_Text textBalas;
    public TMP_Text textAtaque;

    private void Start()
    {
        
        if(loaderScene == null)
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
        textEsc.text = escudos.vidasEXtras.ToString();
        textBalas.text = balas.GetBalasActuales().ToString();
    }

    public void IrMenu()
    {
        loaderScene.LoadSceneString(ConstantsGame.SCENEMAINMENU);
    }
    public void SiguienteNivel()
    {
        if (sceneIndex == 1)
        {
            loaderScene.LoadSceneString(ConstantsGame.SCENADOS);
            
        }
        //procede para mas escena si es el caso
    }
    public void guardarPartida()
    {
        GameData data = new GameData(vidas, balas, player, sceneIndex, nameScene, ChPoint);
        SaveLoadSystem.SaveGame(data, sceneIndex);
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
}