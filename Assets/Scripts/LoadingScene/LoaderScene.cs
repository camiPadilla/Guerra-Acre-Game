using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PantallaCarga
{
    public class LoaderScene : MonoBehaviour
    {
        public static LoaderScene instance;

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
            StartCoroutine(CargarConPantalla(sceneName));
        }

        private IEnumerator CargarConPantalla(string sceneName)
        {
            AsyncOperation loadLoading = SceneManager.LoadSceneAsync(ConstantsGame.SCENELOADINGSCREEN);
            while (!loadLoading.isDone)
                yield return null;

            // Peque�a espera visual
            yield return new WaitForSeconds(10f);

            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            operation.allowSceneActivation = false;

            while (operation.progress < 0.9f)
            {
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);
            operation.allowSceneActivation = true;
        }
    }
}
