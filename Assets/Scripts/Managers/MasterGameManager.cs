using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using TarodevController;
using UnityEngine.SceneManagement;
using Unity.Mathematics;
public class MasterGameManager : MonoBehaviour
{
    public List<bool> checkpointsActivos = new List<bool>();
    public List<LevelCompletionData > nivelesCompletados = new List<LevelCompletionData>();
    public SaludPersonaje playerSalud;
    public AtaquePersonaje playerAtaque;
    public PlayerController playerController;
    public string escenaActual;
    public string escenaSiguiente;


    // Start is called before the first frame update
    public static MasterGameManager instance;
    public GameData gameData;
    private void Awake()
    {
        ReferenciasPlayer();
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void ReferenciasPlayer()
    {
        playerSalud = FindObjectOfType<SaludPersonaje>();
        playerAtaque = FindObjectOfType<AtaquePersonaje>();
        playerController = FindObjectOfType<PlayerController>();
    }
    private void OnLoadSecene(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            print("Hola estas en el Main Menu");
        }
        if (scene.name == "EscenaUno")
        {
            print("Hola estas en el Nivel 1 o tutorial en otras palabras");
            playerSalud = FindObjectOfType<SaludPersonaje>();
            playerAtaque = FindObjectOfType<AtaquePersonaje>();
            playerController = FindObjectOfType<PlayerController>();
            if (playerSalud != null && playerAtaque != null && playerController != null)
            {
                SaveLoadSystem.LoadPlayerData();
            }
            escenaActual = "EscenaUno";
            escenaSiguiente = "EscenaDos";
        }
        if (scene.name == "EscenaDos")
        {
            print("Hola estas en el Nivel 2");
            playerSalud = FindObjectOfType<SaludPersonaje>();
            playerAtaque = FindObjectOfType<AtaquePersonaje>();
            playerController = FindObjectOfType<PlayerController>();
            if (playerSalud != null && playerAtaque != null && playerController != null)
            {
                SaveLoadSystem.LoadPlayerData();
            }
            escenaActual = "EscenaDos";
            escenaSiguiente = "FinalScene";
        }

    }
    public void SaveGame()
    {
        print("||Guardando partida...||");
        SaveLoadSystem.SavePlayerData(playerSalud, playerAtaque, playerController);
        SaveLoadSystem.SaveLevelData(gameData);
    }
    public void LoadGame()
    {
        PlayerData playerData = SaveLoadSystem.LoadPlayerData();

        if (playerData != null && playerController != null)
        {
            int lastCheckpoint = PlayerPrefs.GetInt("LastCheckpoint", -1);
            if (lastCheckpoint != -1)
            {
                CheckPoints[] checkpoints = FindObjectsOfType<CheckPoints>();
                foreach (var cp in checkpoints)
                {
                    if (cp.indexCP == lastCheckpoint)
                    {
                        playerController.transform.position = cp.transform.position;
                        break;
                    }
                }
            }
            else
            {
                // Si no hay checkpoint guardado, cargar posición normal
                playerController.transform.position = new Vector3(
                    playerData.position[0],
                    playerData.position[1],
                    playerData.position[2]
                );
            }
            playerSalud.vidasJugador = playerData.vidasJugador;
            playerSalud.vidasEXtras = playerData.vidasExtras;
            playerAtaque.cantidadBalas = playerData.balas;
            playerAtaque.seleccionArma = playerData.tipoArma;
        }
        else
        {
            Debug.Log(" No se encontró partida previa o jugador no asignado.");
        }
    }
    public void DeleteGame()
    {
        SaveLoadSystem.DeleteAllData();
        PlayerPrefs.DeleteAll();
        Debug.Log("Datos de guardado eliminados.");
    }
}
