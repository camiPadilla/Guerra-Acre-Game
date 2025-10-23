using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dado : MonoBehaviour
{
    [SerializeField] private List<Sprite> caras = new List<Sprite>();
    [SerializeField] private SpriteRenderer miSprite;
    [SerializeField] private int cara;
    public bool usable;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        //Girar();
        miSprite = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
    }

    public void Girar()
    {
        int random = Random.Range(1, 6);
        cara = random;
        miSprite.sprite = caras[cara - 1];
    }
    public void Oponer()
    {
        switch (cara)
        {
            case 1: cara = 6; break;
            case 2: cara = 5; break;
            case 3: cara = 4; break;
            case 4: cara = 3; break;
            case 5: cara = 2; break;
            case 6: cara = 1; break;
        }
        miSprite.sprite = caras[cara - 1];
        usable = false;
        JuegoDadoManager.Instance.bonusPrimera = 0;
        JuegoDadoManager.Instance.ContarPuntos();
        JuegoDadoManager.Instance.AumentarVuelta();
    }
    private void OnMouseDown()
    {
        if (usable && JuegoDadoManager.Instance.PuedeVoltear())
        {
            //Oponer();
            animator.SetTrigger("Cambiar");
            //Debug.Log(cara);

        }
    }
    private void OnMouseOver()
    {
        if (usable)
        {
            //animacion para interactuar
            animator.SetBool("Mouse", true);
        }
    }
    private void OnMouseExit()
    {
        animator.SetBool("Mouse", false);
    }
    public int GetCara()
    {
        return cara;
    }
}
