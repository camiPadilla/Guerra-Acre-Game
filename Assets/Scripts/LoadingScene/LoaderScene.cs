using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PantallaCarga
{
    public class LoaderScene : MonoBehaviour
    {
        public static LoaderScene instance;
        // Start is called before the first frame update
        void Awake()
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("LoaderScene");
            if (objs.Length > 1)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;

            }
            DontDestroyOnLoad(gameObject);
        }
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(ConstantsGame.SCENELOADINGSCREEN);
            StartCoroutine(LoadSceneAsync(sceneName));
        }
        IEnumerator LoadSceneAsync(string sceneName)
        {
            yield return new WaitForSeconds(6f);
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            yield return new WaitUntil(() => asyncLoad.progress >= 0.9f);
        }
    }

}

