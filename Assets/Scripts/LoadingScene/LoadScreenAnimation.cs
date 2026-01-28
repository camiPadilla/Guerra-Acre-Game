using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class LoadScreenAnimation : MonoBehaviour
{

    [SerializeField] private Transform areaSpawn;
    [SerializeField] private Vector2 areaSize;
    [SerializeField] private List<GameObject> juego;
    [SerializeField] private List<Animator> animators;
    [SerializeField] private TMP_Text textLoad;
    [SerializeField] private TMP_Text textTip;
    public string[] tips;

        //1 tigrillo 2 coca 3 siringuero

    public int tipoInteraccion;
    private bool aveCazada = false;
    private void Start()
    {
        tipoInteraccion = UnityEngine.Random.Range(0, 3);
        MiniJuegos();
        StartCoroutine(Consejos());
    }

    public void MiniJuegos()
    {

        if (tipoInteraccion == 0)
        {
            // tigrillo y ave
            juego[0].SetActive(true);
            juego[1].SetActive(true);
        }
        else if (tipoInteraccion == 1)
        {
            // soldado comiendo coca
            juego[2].SetActive(true);
            juego[3].SetActive(true);
        }
        else
        {
            juego[4].SetActive(true);
        }
    }
    private void OnEnable()
    {
        StartCoroutine (Cargando());
    }

    private void Update()
    {
        if (tipoInteraccion == 0) Tigrillo();
        else if (tipoInteraccion == 1) SoldadoComiendo();
        else SoldadoBailando();
    }

    private void Tigrillo()
    {

        if (Input.GetKeyDown(KeyCode.Space) && !aveCazada)
        {
            Debug.Log("¡El tigrillo cazó al ave!");
            aveCazada = true;
            animators[0].SetTrigger("ataque");
            juego[0].transform.position = juego[1].transform.position;
            StartCoroutine(RespawnAve());
        }
    }

    private IEnumerator RespawnAve()
    {
        yield return new WaitForSeconds(0.5f);
        aveCazada = false;
        float w = areaSize.x / 2f;
        float h = areaSize.y / 2f;
        Vector3 localPos = new Vector3(UnityEngine.Random.Range(-w, w), UnityEngine.Random.Range(-h, h), 0);
        juego[1].transform.position = areaSpawn.TransformPoint(localPos);
    }
    private IEnumerator Cargando()
    {
        textLoad.text = "Cargando";
        yield return new WaitForSeconds(0.5f);
        textLoad.text = "Cargando .";
        yield return new WaitForSeconds(0.5f);
        textLoad.text = "Cargando ..";
        yield return new WaitForSeconds(0.5f);
        textLoad.text = "Cargando ...";
        yield return new WaitForSeconds(0.5f);
        StartCoroutine (Cargando());
    }

    private IEnumerator Consejos()
    {
        CambiarTip();
        yield return new WaitForSeconds(3.3f);
        CambiarTip();
        yield return new WaitForSeconds(3.3f);
        CambiarTip();
        yield return new WaitForSeconds(3.3f);
    }
    void CambiarTip()
    {
        int i;
        i = UnityEngine.Random.Range(0, tips.Length);
        textTip.text = tips[i];

    }
    private void SoldadoComiendo()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animators[1].speed += 0.5f;
            InstanciarCoca();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            animators[1].speed = 1f;
        }
    }

    void InstanciarCoca()
    {
        float w = areaSize.x / 2f;
        float h = areaSize.y / 2f;
        Vector3 localPos = new Vector3(UnityEngine.Random.Range(-w, w), UnityEngine.Random.Range(-h, h), 0);
        Vector3 pos = areaSpawn.TransformPoint(localPos);
        Instantiate(juego[3], pos, Quaternion.identity, areaSpawn);
    }

    private void SoldadoBailando()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            animators[2].speed = 3f;
        }
        else
        {
            animators[2].speed = 1f;
        }
    }

}