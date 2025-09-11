
using System;
using UnityEngine;

public static class SoundEvents
{
    //Cargar fuerza de piedra
    public static Action CargarFuerzaPiedra;
    public static Action DetenerCarga;

    //Lanzar piedra
    public static Action LanzarPiedra;

    //DestruirCaja
    public static Action<float> DestruirCaja;

    //Caminar Pasos
    public static Action PasosPasto;
    public static Action DetenerPasosPasto;
    public static Action RecogerNota;
}

