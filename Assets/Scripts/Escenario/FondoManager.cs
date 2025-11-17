using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FondoManager : MonoBehaviour
{
    [SerializeField]
    private float retraso;
    [SerializeField]
    private Transform camaraTransform;
    private Vector3 anteriorPosicionCamara;

    // Start is called before the first frame update
    void Start()
    {
        //camaraTransform = Camera.main.transform;
        anteriorPosicionCamara = camaraTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = (camaraTransform.position.x - anteriorPosicionCamara.x) * retraso;
        transform.Translate(new Vector3(deltaX, 0, 0));
        anteriorPosicionCamara = camaraTransform.position;
    }
}
