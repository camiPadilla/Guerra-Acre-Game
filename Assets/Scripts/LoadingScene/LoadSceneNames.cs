using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PantallaCarga 
{
    public class LoadSceneNames : MonoBehaviour
    {
        //Agregar las demas escenas como el menu , etc
        public void SceneEscenaUno()
        {
            LoaderScene.instance.LoadSceneString(ConstantsGame.ESCENAUNO);
        }
        public void SceneEscenaDos()
        {
            LoaderScene.instance.LoadSceneString(ConstantsGame.ESCENADOS);
        }
        public void QuitGame()
        {
            Application.Quit();
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }
}


