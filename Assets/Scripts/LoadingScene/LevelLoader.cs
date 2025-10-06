using System.Collections;
using System.Collections.Generic;
using PantallaCarga;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelLoader 
{
    public static string nextLevel;
    public static void LoadLevel(string sceneName)
    {
        nextLevel = sceneName;
        SceneManager.LoadScene(ConstantsGame.SCENELOADINGSCREEN);
    }
}
