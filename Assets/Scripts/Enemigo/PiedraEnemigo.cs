using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiedraEnemigo : ArmaEnemigo
{
    public override void DisparoIA()
    {
        Vector2 direccion = new Vector2(transform.right.x, transform.right.y);
        //Lanzar piedra
        rbArma.AddForce(direccion * fuerza, ForceMode2D.Impulse);
    }
}
