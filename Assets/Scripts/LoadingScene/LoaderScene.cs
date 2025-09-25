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

        // Llamas a este m�todo desde cualquier parte
        public void LoadSceneString(string sceneName)
        {
            sceneDestino = sceneName;
            SceneManager.LoadScene(ConstantsGame.SCENELOADINGSCREEN);
        }

        private IEnumerator CargarAsync()
        {
            yield return new WaitForSeconds(3f); 
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneDestino);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }
}
