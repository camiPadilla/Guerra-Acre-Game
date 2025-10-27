using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TarodevController;
using PantallaCarga;

public class MasterGameManager : MonoBehaviour
{
    public static MasterGameManager instance;


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
    public int currentLevel = 1;


    [SerializeField] public int currentSlot = 1; // se define desde el menú

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
        Debug.Log("Slot seleccionado: " + currentSlot);
    }

    private void OnLoadScene(Scene scene, LoadSceneMode mode)
    {
        ReferenciasPlayer();

        if (scene.name == "MainMenu")
        {
            Debug.Log("Estas en el Main Menu");
            return;
        }

        // Configuración según la escena
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

    public void ActivarCheckPoint(int index)
    {
        if (gameData == null)
            gameData = new GameData(playerSalud, playerAtaque, playerController, currentLevel, SceneManager.GetActiveScene().name, lastCP, currentSlot);

        // Asegurar que la lista sea suficientemente larga
        if (index >= gameData.checkpointsActivos.Count)
        {
            for (int i = gameData.checkpointsActivos.Count; i <= index; i++)
                gameData.checkpointsActivos.Add(false);
        }

        gameData.checkpointsActivos[index] = true;
        gameData.lastCheckPoint = index;

        SaveGame();
        Debug.Log("✅ Checkpoint activado y guardado: " + index);
    }

    public void SaveGame()
    {
        GameData data = new GameData(playerSalud, playerAtaque, playerController, currentLevel, SceneManager.GetActiveScene().name, lastCP, currentSlot);
        SaveLoadSystem.SaveGame(data, currentSlot);
        Debug.Log($"💾 Juego guardado en slot {currentSlot}");
    }

    public void LoadGame()
    {
        GameData data = SaveLoadSystem.LoadGame(currentSlot);
        if (data != null)
        {
            StartCoroutine(LoadRestore(data));
            Debug.Log($"📂 Partida cargada desde slot {currentSlot}");
        }
        else
        {
            Debug.LogWarning("⚠️ No hay partida guardada en este slot.");
        }
    }

    private IEnumerator LoadRestore(GameData data)
    {
        // Si la escena guardada no es la actual, se cambia
        if (SceneManager.GetActiveScene().name != data.lastScene)
            SceneManager.LoadScene(data.lastScene);

        yield return new WaitForSeconds(0.2f);

        ReferenciasPlayer();
        yield return RestoreAfterLoad(data);
    }

    private IEnumerator RestoreAfterLoad(GameData data)
    {
        yield return new WaitForSeconds(0.1f);

        // Restaurar posición desde el último checkpoint o posición guardada
        int lastCheckPoint = data.lastCheckPoint;
        if (lastCheckPoint != -1)
        {
            CheckPoints[] checkPoints = FindObjectsOfType<CheckPoints>();
            foreach (var cp in checkPoints)
            {
                if (cp.indexCP == lastCheckPoint)
                {
                    playerController.transform.position = cp.transform.position;
                    break;
                }
            }
        }
        else
        {
            playerController.transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
        }

        playerSalud.vidasJugador = data.vidasJugador;
        playerSalud.vidasEXtras = data.vidasExtras;
        playerAtaque.cantidadBalas = data.balas;
        playerAtaque.seleccionArma = data.tipoArma;
    }


    public void NewGame()
    {
        SaveLoadSystem.DeleteSlot(currentSlot);
        loaderScene.LoadSceneString("EscenaUno");
        Debug.Log($"🌱 Nuevo juego iniciado en slot {currentSlot}");
    }


    public void DeleteGame()
    {
        SaveLoadSystem.DeleteAllData();
        Debug.Log("🗑️ Datos de guardado eliminados completamente.");
    }

    public void DetenerTiempo()
    {
        Time.timeScale = 0;
    }
}
