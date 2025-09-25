using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public void CambioScene()
    {
        string levelToLoad = LevelLoader.nextLevel;
        StartCoroutine(MakeTheLoad(levelToLoad));
    }
    IEnumerator MakeTheLoad(string sceneName)
    {
        yield return new WaitForSeconds(1f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone== false)
        {
            yield return null;
        }
    }
}
