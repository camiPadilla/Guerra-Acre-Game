using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cajasDestruibles : ObjetoDestruible
{

    [SerializeField] bool conLoot;
    // Start is called before the first frame update
    
    public void ActivarLoot()
    {
        if(conLoot)   GameManager.instancia.InstanciarObjeto(transform.position);
    }
}
