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
    Vector2 corazon = new Vector2(2.5f, 2.5f);
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
        if (transform.position.x > corazon.x + 0.5f)
        {
            interactuable = false;
            GetComponent<SpriteRenderer>().color = Color.gray;
        }
            
        if (Input.GetKeyDown(KeyCode.E) && interactuable)
            comprobarPuntos();
        //Debug.Log(Vector2.Distance(transform.position, corazon));
    }
    
    IEnumerator barraCorrecta(Color colorPuntaje)
    {
        GetComponent<SpriteRenderer>().color = colorPuntaje;
        transform.localScale = new Vector2(1.5f, 1.5f);
        yield return new WaitForSeconds(0.3f);
        GetComponent<SpriteRenderer>().color = Color.gray;
        interactuable = false;
        transform.localScale = Vector2.one;
    }
    private void comprobarPuntos()
    {
        float distancia = Vector2.Distance(transform.position, corazon);
        if (distancia <= 0.3f)
        {
            StartCoroutine("barraCorrecta", Color.green);
            RCPManager.instancia.AumentarPuntos(10);
        }else if(distancia > 0.3f && distancia<2f)
        {
            RCPManager.instancia.AumentarPuntos(10);
            StartCoroutine("barraCorrecta", Color.yellow);
        }
    }
}
