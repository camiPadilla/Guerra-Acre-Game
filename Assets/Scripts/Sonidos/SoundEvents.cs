
using System;
using UnityEngine;

public static class SoundEvents
{
    //Cargar fuerza de piedra
    public static Action CargarFuerzaPiedra;
    public static Action DetenerCarga;

    //Lanzar piedra
    public static Action LanzarPiedra;

    //SaltoCaída
    public static Action<float> Salto;

    //Destruir Caja
    public static Action<float> DestruirObjeto;

    //Caminar Pasos
    public static Action PasosPasto;
    public static Action DetenerPasosPasto;

    //Recoger notas
    public static Action RecogerNota;

    //Hablar NPC Aliado
    public static Action HablarAliadoNPC;

    //Ambientes
    public static Action Ambience;
}

