using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ControladorBarra : MonoBehaviour
{
    [SerializeField] public Transform target;
    float velocidad=9.5f * 0.5825f;
    [SerializeField] public Transform posicionInicial;
    [SerializeField] public RhythmSO tiempoBarra;
    Vector2 corazon = new Vector2(-0.5f, 13f);
    
     bool interactuable = true;

    private void OnEnable()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        interactuable=true;
    }
    // Update is called once per frame
    void Update()
    {
        
        transform.position= Vector2.MoveTowards(transform.position, target.position, velocidad * Time.deltaTime);
        if(Vector2.Distance(transform.position, target.position)<0.1f)
            RCPManager.instancia.DevolverBarra(this.gameObject);
        if (transform.localPosition.x > corazon.x + 0.5f && interactuable)
        {
            //Debug.Log("el objeto se alejo del target");
            interactuable = false;
            GetComponent<SpriteRenderer>().color = Color.gray;
        }

        if (Input.GetKey(KeyCode.E) && interactuable)
        {
            //Debug.Log("hola estoy detectando inputs");
            ComprobarPuntos();
        }
        //Debug.Log(Vector2.Distance(transform.position, corazon));
    }
    
    IEnumerator FeedBackGrafico(Color colorPuntaje)
    {
        GetComponent<SpriteRenderer>().color = colorPuntaje;
        transform.localScale = new Vector2(1.5f, 1.5f);
        yield return new WaitForSeconds(0.3f);
        GetComponent<SpriteRenderer>().color = Color.gray;
        interactuable = false;
        transform.localScale = Vector2.one;
    }
    private void ComprobarPuntos()
    {
        float distancia = Vector2.Distance(transform.localPosition, corazon);
        //Debug.Log("distancia: " + distancia);
        if (distancia <= 0.32f && distancia > -0.3f)
        {
            StartCoroutine("FeedBackGrafico", Color.green);
            RCPManager.instancia.AumentarPuntos(10);
            interactuable = false;
        }else if(distancia > 0.32f && distancia<1.82f)
        {
            RCPManager.instancia.AumentarPuntos(5);
            StartCoroutine("FeedBackGrafico", Color.yellow);
            interactuable=false;
        }
    }
}
