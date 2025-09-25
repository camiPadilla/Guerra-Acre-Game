using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cajasDestruibles : ObjetoDestruible
{
    // Start is called before the first frame update
    
    public void ActivarLoot()
    {
        GameManager.instancia.InstanciarObjeto(transform.position);
    }
}
