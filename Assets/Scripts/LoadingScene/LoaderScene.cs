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

        // Llamas a este método desde cualquier parte
        public void LoadScene(string sceneName)
        {
            sceneDestino = sceneName;
            SceneManager.LoadScene(ConstantsGame.SCENELOADINGSCREEN);
        }

        // Este lo ejecutas desde la pantalla de carga
        public void CargarDestino()
        {
            StartCoroutine(CargarAsync());
        }

        private IEnumerator CargarAsync()
        {
            yield return new WaitForSeconds(3f); // tiempo fijo de carga
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneDestino);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }
}


