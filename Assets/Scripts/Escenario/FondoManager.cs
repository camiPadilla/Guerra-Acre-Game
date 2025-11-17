using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FondoManager : MonoBehaviour
{
    [SerializeField]
    private GameObject filtro;
    [SerializeField]
    private float tamano;
    private float tamanoInicial;
    [SerializeField] private float tiempoAnim;
    [SerializeField] private float pasos;
    WaitForSeconds wait;



    // Start is called before the first frame update
    void Start()
    {
        wait = new WaitForSeconds(tiempoAnim / pasos); 
        tamanoInicial = 1.31f;
    }
    private IEnumerator CambiarTamano(float tI, float iF)
    {
        for (int i = 0; i < pasos; i++)
        {
            float nuevoTamano = Mathf.Lerp(tI, iF, (i + 1) / pasos);
            filtro.transform.localScale = new Vector3(nuevoTamano, nuevoTamano, 1);
            yield return wait;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            StartCoroutine(CambiarTamano(filtro.transform.localScale.x, tamano));
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            StartCoroutine(CambiarTamano(filtro.transform.localScale.x, tamanoInicial));
        }
    }

}
