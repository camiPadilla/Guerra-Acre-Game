using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    [SerializeField] BoxCollider2D miAtaque;
    bool interactaundo = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
            miAtaque.enabled = true;
            StartCoroutine(nameof(Espera));
        }
        if (Input.GetKey(KeyCode.E))
        {
            Debug.Log("Jugador listo para interactuar");
            interactaundo = true;
        }
        else
        {
            interactaundo = false;
        }
    }
    IEnumerator Espera()
    {
        yield return new WaitForSeconds(0.5f);
        miAtaque.enabled = false;
    }

    public bool getInteractuable()
    {
        return interactaundo;
    }
    
}
