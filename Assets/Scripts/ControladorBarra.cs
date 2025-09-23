using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorBarra : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float velocidad;
    [SerializeField] Transform posicionInicial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position= Vector2.MoveTowards(transform.position, target.position, velocidad * Time.deltaTime);
        if(Vector2.Distance(transform.position, target.position)<0.1f)
        {
            //gameObject.SetActive(false);
            transform.position = posicionInicial.position;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Iniciando RCP");
            this.GetComponent<SpriteRenderer>().color = Color.green;

        }
    }
}
