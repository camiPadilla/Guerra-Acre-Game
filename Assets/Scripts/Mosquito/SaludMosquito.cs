using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaludMosquito : MonoBehaviour
{
    [SerializeField] int vidas;
    Animator animator;
    BoxCollider2D micolision;
    void Start()
    {
        animator = GetComponent<Animator>();
        micolision = GetComponent<BoxCollider2D>();
    }
    public void perderVida()
    {
        vidas--;
        Debug.Log("Perdi una vida mosquito");
        if(vidas == 0)
        {
            SoundEvents.MorirMosquito?.Invoke(transform.position.x);
            animator.SetTrigger("muerto");
            micolision.enabled = false;

        }
    }
    public void Suicidio()
    {
        Destroy(this.gameObject);
    }
}
