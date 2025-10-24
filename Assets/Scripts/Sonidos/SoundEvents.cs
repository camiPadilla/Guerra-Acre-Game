
using System;
using UnityEngine;

public static class SoundEvents
{
    //Cargar fuerza de piedra
    public static Action CargarFuerzaPiedra;
    public static Action DetenerCarga;

    //Lanzar piedra
    public static Action LanzarPiedra;
    public static Action CambiarArmaPiedra;

    //Ataque Melee
    public static Action AtaqueMachete;
    public static Action CambiarArmaMachete;

    
    //SaltoCa�da
    public static Action<float> Salto;

    //Destruir Objetos
    public static Action<float, int> DestruirObjeto;

    //Caminar Pasos
    public static Action PasosPasto;
    public static Action DetenerPasosPasto;

    //Recoger notas
    public static Action RecogerNota;

    //Recoger Arma
    public static Action RecogerArma;

    //Recoger Balas
    public static Action RecogerBalas;
    //Ataque Fusil
    public static Action SinBalas;
    public static Action RecargarBalas;


    internal static object MorirPersonaje()
    {
        throw new NotImplementedException();
    }
    //Da�o Personaje
    public static Action DanoPersonaje;
    public static Action MorirPersonajee;

    //Bandera Checkpoint
    public static Action CheckpointActivado;

    //Hablar NPC Aliado
    public static Action HablarAliadoNPC;

    //Mosquito
    public static Action MorirMosquito;

    //Siringuero
    public static Action<float> DisparoEnemigo;
    public static Action<float> RecibirDano;
    public static Action<float> MorirSiringuero;

    //Arrastrar Objeto
    public static Action ArrastrarObjeto;
    public static Action DetenerArrastrarObjeto;

    //Caer al agua
    public static Action CaerAgua;

    //Ambientes
    public static Action Ambience;
}

