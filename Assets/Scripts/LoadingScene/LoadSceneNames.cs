using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PantallaCarga 
{
   
    public class LoadSceneNames : MonoBehaviour
    { 
        static LoadSceneNames instance;
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        public void SceneMainMenu()
        {
            LoaderScene.instance.LoadSceneString(ConstantsGame.MAINMENU);
        }
        //Agregar las demas escenas como el menu , etc
        public void SceneEscenaUno()
        {
            LoaderScene.instance.LoadSceneString(ConstantsGame.SCENAUNO);
        }
        public void SceneEscenaDos()
        {
            LoaderScene.instance.LoadSceneString(ConstantsGame.SCENADOS);
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


