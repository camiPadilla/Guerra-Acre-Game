using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReanudarJogo : MonoBehaviour
{
    GameManager gameManager;

    // Update is called once per frame
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "EscenaUno")
        {
            gameManager = FindAnyObjectByType<GameManager>(); ;
        }
    }
    public void ClickDetector()
    {
        Debug.Log("Clic sobre este objeto");
        gameManager.CerrarEstado();
    }
}
