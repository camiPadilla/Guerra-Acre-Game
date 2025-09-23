using System.Collections;
using System.Collections.Generic;
using PantallaCarga;
using UnityEngine;

public class FinalNivel : MonoBehaviour
{
    [SerializeField] private GameObject jugador;
    [SerializeField] private LoaderScene loader;
    public void Start()
    {
        loader = FindObjectOfType<LoaderScene>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Colision");
        if (other.CompareTag("Player"))
        {
            StartCoroutine(CargarEscena(ConstantsGame.SCENADOS));
        }
    }
    IEnumerator CargarEscena(string sceneName)
    {
        //No se si aparecera datos o cosas que el jugador haya recolectado o logros, etc....
        yield return new WaitForSeconds(2f);
        loader.LoadScene(sceneName);
    }
}
