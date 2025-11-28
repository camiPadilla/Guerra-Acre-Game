using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cajasDestruibles : ObjetoDestruible
{

    [SerializeField] bool conLoot;
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ActivarLoot()
    {
        animator.SetTrigger("destruir");
        if (conLoot)   ObjetosManager.instancia.InstanciarObjeto(transform.position);
    }
}
