using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    //[SerializeField] BoxCollider2D machete;
    bool interactaundo = false;
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
           // Debug.Log("Jugador listo para interactuar");
            interactaundo = true;
        }
        else
        {
            interactaundo = false;
        }
        if (Input.GetButton("Fire2"))
        {
            //Debug.Log("jugador listo para empujar");
            moviendo = true;
        }
        else
        {
            moviendo= false;
        }
    }
    //IEnumerator Espera()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    machete.enabled = false;
    //}

    public bool getInteractuable()
    {
        return interactaundo;
    }
    public bool GetMoviendo()
    {
        return moviendo;
    }

}
