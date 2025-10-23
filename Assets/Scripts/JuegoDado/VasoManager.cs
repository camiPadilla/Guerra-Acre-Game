
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VasoManager : MonoBehaviour
{
    public bool usable;
    [SerializeField]
    private JuegoDadoManager juegoManager;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        usable = true;
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        Debug.Log("tirado");
        if (usable)
        {
            //animacion tirar
            //juegoManager.Tirar();
            animator.SetTrigger("Tirar");
        }
    }
    private void OnMouseOver()
    {
        if (usable)
        {
            //animacion para interactuar
            animator.SetBool("Mouse",true);
        }
    }
    private void OnMouseExit()
    {
            animator.SetBool("Mouse",false);
    }
    public void Tirada()
    {
        juegoManager.Tirar();
    }
}
