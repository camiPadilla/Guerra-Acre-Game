
using System;
using UnityEngine;

public static class SoundEvents
{
    //Cargar fuerza de piedra
    public static Action CargarFuerzaPiedra;
    public static Action DetenerCarga;

    //Lanzar piedra
    public static Action LanzarPiedra;
    //Ataque Melee
    public static Action AtaqueMachete;

    //SaltoCaída
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

    //Bandera Checkpoint
    public static Action CheckpointActivado;

    //Hablar NPC Aliado
    public static Action HablarAliadoNPC;

    //Mosquito
    public static Action MorirMosquito;

    //Arrastrar Objeto
    public static Action ArrastrarObjeto;
    public static Action DetenerArrastrarObjeto;

    //Ambientes
    public static Action Ambience;
}

