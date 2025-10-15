using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    public int indexCP;                // Número del checkpoint
    public bool checkPointActivo;      // Si ya fue activado o no

    private void Start()
    {
        // Seguridad: verificar que exista el GameManager
        if (MasterGameManager.instance == null)
        {
            Debug.LogError("No se encontró el MasterGameManager en la escena.");
            return;
        }

        // Si el checkpoint ya fue activado antes, marcarlo activo
        if (MasterGameManager.instance.checkpointsActivos.Count > indexCP &&
            MasterGameManager.instance.checkpointsActivos[indexCP])
        {
            checkPointActivo = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !checkPointActivo)
        {
            checkPointActivo = true;
            Debug.Log($"Checkpoint {indexCP} activado por el jugador.");

            ActivarCheckpoint();
        }
    }

    private void ActivarCheckpoint()
    {
        var master = MasterGameManager.instance;

        // Asegura que la lista tenga espacio suficiente
        while (master.checkpointsActivos.Count <= indexCP)
        {
            master.checkpointsActivos.Add(false);
        }

        // Marca el checkpoint como activo
        master.checkpointsActivos[indexCP] = true;

        // Guarda la posición actual del jugador en el sistema de guardado
        if (master.playerController != null)
        {
            Vector3 pos = master.playerController.transform.position;

            // Guarda usando tu sistema de guardado personalizado
            SaveLoadSystem.SavePlayerData(master.playerSalud, master.playerAtaque, master.playerController);
            SaveLoadSystem.SaveLevelData(master.gameData);

            Debug.Log($"Checkpoint {indexCP} guardado con posición ({pos.x}, {pos.y}, {pos.z})");
        }
        else
        {
            Debug.LogWarning("No se encontró el PlayerController al intentar guardar el checkpoint.");
        }
    }
}
