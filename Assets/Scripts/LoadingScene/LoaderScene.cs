using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PantallaCarga
{
    public class LoaderScene : MonoBehaviour
    {
        public static LoaderScene instance;
        private static string sceneDestino;

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
        public void LoadSceneString(string sceneName)
        {
            sceneDestino = sceneName;
        }

        // Llamado desde la pantalla de carga
        public void CargarDestino()
        {
            StartCoroutine(Cargar(sceneDestino));
        }

        private IEnumerator Cargar(string sceneDestino)
        {
            yield return new WaitForSeconds(2f); // espera fija antes de cargar

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneDestino);
            asyncLoad.allowSceneActivation = false;

            // Espera hasta que esté lista la carga
            while (!asyncLoad.isDone)
            {
                if (asyncLoad.progress >= 0.9f)
                {
                    asyncLoad.allowSceneActivation = true;
                }
                yield return null;
            }
        }
    }
}
