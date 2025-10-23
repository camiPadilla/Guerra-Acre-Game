using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TarodevController;
using System.Collections;
using PantallaCarga;

public class MasterGameManager : MonoBehaviour
{
    public static MasterGameManager instance;
    //Referencias
    public SaludPersonaje playerSalud;
    public AtaquePersonaje playerAtaque;
    public PlayerController playerController;
    public LoaderScene loaderScene;

    public List<bool> checkpointsActivos = new List<bool>();
    public GameData gameData;
    public string escenaActual;
    public string escenaSiguiente;
    public bool InGame;
    public int lastCP;
    public int AcPoint;

    //para el guardado de diferentes partidas
    public int currentLevel=1;
    private int currentSlot = 1;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        ReferenciasPlayer();
        SceneManager.sceneLoaded += OnLoadScene;
    }

    private void ReferenciasPlayer()
    {
        playerSalud = FindObjectOfType<SaludPersonaje>();
        playerAtaque = FindObjectOfType<AtaquePersonaje>();
        playerController = FindObjectOfType<PlayerController>();
    }
    public void SetSlot(int slot)
    {
        currentSlot = slot;
    }
    private void OnLoadScene(Scene scene, LoadSceneMode mode)
    {
        ReferenciasPlayer();

        if (scene.name == "MainMenu")
        {
            Debug.Log("Estas en el Main Menu");
            return;
        }

        if (scene.name == "EscenaUno")
        {
            escenaActual = "EscenaUno";
            escenaSiguiente = "EscenaDos";
        }
        else if (scene.name == "EscenaDos")
        {
            escenaActual = "EscenaDos";
            escenaSiguiente = "FinalScene";
        }

        LoadGame();
    }

    // Se llama desde un checkpoint
    public void ActivarCheckPoint(int index)
    {
        if (gameData == null)
            gameData = new GameData(playerSalud, playerAtaque, playerController, currentLevel, SceneManager.GetActiveScene().name, lastCP);

        if (index >= gameData.checkpointsActivos.Count)
        {
            for (int i = gameData.checkpointsActivos.Count; i <= index; i++)
                gameData.checkpointsActivos.Add(false);
        }

        gameData.checkpointsActivos[index] = true;
        gameData.lastCheckPoint = index;

        SaveGame();
        Debug.Log("Checkpoint activado y guardado: " + index);
    }

    public void SaveGame()
    {
        GameData data = new GameData(playerSalud ,playerAtaque, playerController,currentLevel,SceneManager.GetActiveScene().name, lastCP);
        SaveLoadSystem.SaveGame(data, currentSlot);
    }

    public void LoadGame()
    {
        GameData data = SaveLoadSystem.LoadGame(currentSlot);
        if(data != null)
        {
            StartCoroutine(LoadRestore(data));
        }
    }
    private IEnumerator LoadRestore(GameData data)
    {
        if (SceneManager.GetActiveScene().name != data.lastScene)
            SceneManager.LoadScene(data.lastScene);

        yield return new WaitForSeconds(0.2f);
        ReferenciasPlayer();
        yield return RestoreAfterLoad(data);
    }
    private IEnumerator RestoreAfterLoad(GameData data)
    {
        yield return new WaitForSeconds(0.1f);
        int lastCheckPoint = (data != null) ? data.lastCheckPoint : -1;
        if(lastCheckPoint != -1)
        {
            CheckPoints[] checkPoints = FindObjectsOfType<CheckPoints>();
            foreach(var cp in checkPoints)
            {
                if(cp.indexCP == lastCheckPoint)
                {
                    playerController.transform.position = cp.transform.position;
                    break;
                }
            }
        }
        else
        {
            playerController.transform.position = new Vector3(data.position[1], data.position[2], data.position[3]);
        }
        playerSalud.vidasJugador = data.vidasJugador;
        playerSalud.vidasEXtras = data.vidasExtras;
        playerAtaque.cantidadBalas = data.balas;
        playerAtaque.seleccionArma = data.tipoArma;
        
    }
    public void NewGame()
    {
        SaveLoadSystem.DeleteSlot(currentSlot);
        loaderScene.LoadSceneString(ConstantsGame.SCENAUNO);
    }

    public void DeleteGame()
    {
        SaveLoadSystem.DeleteAllData();
        Debug.Log("Datos de guardado eliminados.");
    }

    public void DetenerTiempo()
    {
        Time.timeScale = 0;
    }
}
