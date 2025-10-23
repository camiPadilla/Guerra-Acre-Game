using PantallaCarga;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
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
            FindObjectOfType(typeof(LoaderScene));
        }
        if(vidas == null)
        {
            FindObjectOfType(typeof(SaludPersonaje));
        }
        if(escudos == null)
        {
            FindObjectOfType(typeof(SaludPersonaje));
        }
        if(balas == null)
        {
            FindObjectOfType(typeof(AtaquePersonaje));
        }
    }
    public void Uptade()
    {
        ProgresoFinal();
    }
    public void ProgresoFinal()
    {
        textVidas.text = vidas.ToString();
        textEsc.text = escudos.ToString();
        textBalas.text = balas.ToString();
        textEsc.text = escudos.ToString(); 
    }

    public void IrMenu()
    {
        loaderScene.LoadSceneString(ConstantsGame.MAINMENU);
    }
    public void SiguienteNivel()
    {
        if(sceneIndex == 1)
        {
            loaderScene.LoadSceneString(ConstantsGame.SCENAUNO);
        }
        //procede para mas escena si es el caso
    }
}
