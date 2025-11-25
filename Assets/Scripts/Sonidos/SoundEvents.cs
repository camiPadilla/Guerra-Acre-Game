
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
    public static Action LeerSimple;
    public static Action LeerColeccionable;
    public static Action DetenerColeccionable;

    //Recoger Arma
    public static Action RecogerArma;

    //Recoger Balas
    public static Action RecogerBalas;
    //Ataque Fusil
    public static Action SinBalas;
    public static Action RecargarBalas;

    //Objetos curables
    public static Action RecogerVida;
    public static Action EquiparArmadura;

    //Dano Personaje
    public static Action DanoPersonaje;
    public static Action MorirPersonaje;

    //Bandera Checkpoint
    public static Action CheckpointActivado;

    //Hablar NPC Aliado
    public static Action HablarAliadoNPC;

    //Mosquito
    public static Action<float, string> VueloMosquito;
    public static Action<float> MorirMosquito;

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

    //Detener Musica
    public static Action DetenerMusica;

    //Pause Sound
    public static Action PauseSound;

    //Cacho Sounds
    public static Action hoverCacho;
    public static Action lanzarDado;
    public static Action anotar;
}

