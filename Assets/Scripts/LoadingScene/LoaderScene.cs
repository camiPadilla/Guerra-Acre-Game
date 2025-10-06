using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PantallaCarga
{
    public class LoaderScene : MonoBehaviour
    {
        public static LoaderScene instance;
        [SerializeField] private GameObject pantallaCarga;
        [SerializeField] private LoadScreenAnimation loadScreenAnimation;
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(pantallaCarga);

            if (pantallaCarga != null)
                pantallaCarga.SetActive(false);


        }
        public void LoadSceneString(string sceneName)
        {
            loadScreenAnimation.GetComponent<LoadScreenAnimation>().tipoInteraccion = Random.Range(0, 3);
            StartCoroutine(PantallaCarga(sceneName));
        }
        private IEnumerator PantallaCarga(string sceneName)
        {
            pantallaCarga.SetActive(true);
            yield return new WaitForSeconds(10f);
            loadScreenAnimation.DestroyAllClones();
            SceneManager.LoadSceneAsync(sceneName);
            pantallaCarga.SetActive(false);
        }
    }
}
