using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    //[SerializeField] BoxCollider2D machete;
    bool interactuando = false;
    bool moviendo = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //if (Input.GetButtonDown("Fire1"))
        //{
        //    machete.enabled = true;
        //    StartCoroutine(nameof(Espera));
        //}
        if (Input.GetKey(KeyCode.E))
        {

            interactuando = true;
        }
        else
        {
            interactuando = false;
        }


        if (Input.GetButton("Fire2"))
        {
            Debug.Log("jugador listo para empujar");
            moviendo = true;
        }
        else
        {
            moviendo = false;
        }
        //interactaundo = false;
    }
    //IEnumerator Espera()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    machete.enabled = false;
    //}

    public bool getInteractuable()
    {
        bool aux = interactuando;
        interactuando = false;
        return aux;
    }
    public bool GetMoviendo()
    {
        return moviendo;
    }

}