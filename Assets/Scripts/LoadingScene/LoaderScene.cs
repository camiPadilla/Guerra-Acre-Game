using System.Collections;
using Unity.VisualScripting;
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
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            
        }
        public void LoadSceneString(string sceneName)
        {
            SceneManager.LoadScene(ConstantsGame.SCENELOADINGSCREEN);
            StartCoroutine(PantallaCarga(sceneName));
        }
        private IEnumerator PantallaCarga(string sceneName)
        {
            yield return new WaitForSeconds(10f);
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            yield return new WaitUntil(()=>operation.progress <=0.9f);
        }
    }
}