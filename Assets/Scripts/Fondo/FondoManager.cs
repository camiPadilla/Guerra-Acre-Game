using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FondoManager : MonoBehaviour
{
    [SerializeField]
    private float retraso;

    private Transform camaraTransform;
    private Vector3 anteriorPosicionCamara;
    [SerializeField] private float anchoAprite, posicionInicial;

    // Start is called before the first frame update
    void Start()
    {
        camaraTransform = Camera.main.transform;
        anteriorPosicionCamara = camaraTransform.position;
        //anchoAprite = GetComponent<SpriteRenderer>().bounds.size.x;
        //anchoAprite = 24.68571f;

        Debug.Log(anchoAprite);
        //posicionInicial = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = (camaraTransform.position.x - anteriorPosicionCamara.x) * retraso;
        //float cantidadMovimiento = camaraTransform.position.x * (1 - retraso);
        transform.Translate(new Vector3(deltaX, 0, 0));
        anteriorPosicionCamara = camaraTransform.position;
        
        //if(cantidadMovimiento > posicionInicial + anchoAprite)
        //{
        //    transform.Translate(new Vector3(anchoAprite,0, 0));
        //    posicionInicial += anchoAprite;
        //}
        //else if(cantidadMovimiento < posicionInicial - anchoAprite)
        //{
        //    transform.Translate(new Vector3(-anchoAprite, 0, 0));
        //    posicionInicial -= anchoAprite;
        //}
    }
}
