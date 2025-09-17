using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PantallaCarga {
    public class LoadSceneNames : MonoBehaviour
    {
        //Agregar las demas escenas como el menu , etc
        public void SceneEscenaUno()
        {
            LoaderScene.instance.LoadScene(ConstantsGame.SCENAUNO);
        }
        public void SceneEscenaDos()
        {
            LoaderScene.instance.LoadScene(ConstantsGame.SCENADOS);
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


