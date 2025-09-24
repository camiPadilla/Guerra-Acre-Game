using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PantallaCarga {
    public class LoadSceneNames : MonoBehaviour
    {
        //Agregar las demas escenas como el menu , etc
        public void SceneEscenaUno()
        {
            LoaderScene.instance.LoadScene(ConstantsGame.ESCENAUNO);
        }
        public void SceneEscenaDos()
        {
            print("Hola soy la escena 2");
            LoaderScene.instance.LoadScene(ConstantsGame.ESCENADOS);
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


