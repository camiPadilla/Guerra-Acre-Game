using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusilEnemigo : ArmaEnemigo
{
    public override void DisparoIA()
    {
        //Disparar bala
        rbArma.AddForce(transform.right * fuerza, ForceMode2D.Impulse);
    }
}
