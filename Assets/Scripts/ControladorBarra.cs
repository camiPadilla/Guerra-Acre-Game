using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ControladorBarra : MonoBehaviour
{
    [SerializeField] public Transform target;
    float velocidad=9.5f * 0.5825f;
    
    [SerializeField] public RhythmSO tiempoBarra;
    Vector2 corazon;
    
     bool interactuable = true;

    public void setCorazon(Vector2 nuevo)
    {
        corazon = nuevo;
    }
    private void OnEnable()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        interactuable=true;
    }
    public void setTarget(Transform nuevo)
    {
        target = nuevo;
    }
    // Update is called once per frame
    void Update()
    {
        
        transform.position= Vector2.MoveTowards(transform.position, target.position, velocidad * Time.deltaTime);
        if(Vector2.Distance(transform.position, target.position)<0.1f)
            RCPManager.instancia.DevolverBarra(this.gameObject);
        if (transform.position.x >= corazon.x && interactuable)
        {
            interactuable = false;
            GetComponent<SpriteRenderer>().color = Color.gray;
        }

        else if (Input.GetKey(KeyCode.E) && interactuable)
        {
            Debug.Log("hola estoy detectando inputs");
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
        float distancia = Vector2.Distance(transform.position, corazon);
        //Debug.Log(distancia + " distancia del corazón a la barra");
        if (distancia <= 1.7f && distancia >= 1.1f)
        {
            StartCoroutine("FeedBackGrafico", Color.green);
            RCPManager.instancia.AumentarPuntos(10);
            interactuable = false;
        }
        else if (distancia < 2.75f)
        {
            RCPManager.instancia.AumentarPuntos(5);
            StartCoroutine("FeedBackGrafico", Color.yellow);
            interactuable = false;
        }
    }
}
