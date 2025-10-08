using UnityEngine;
using FMODUnity;

public class SAmbienceSounds : MonoBehaviour
{
    [SerializeField] StudioEventEmitter Ambiente;
    float tiempoMin = 15f; // tiempo mínimo entre sonidos
    float tiempoMax = 50f; // tiempo máximo entre sonidos

    private float siguienteTiempo;
    private float temporizador;

    void Start()
    {
        SetNuevoTiempo();
    }

    void Update()
    {
        temporizador += Time.deltaTime;
        if (temporizador >= siguienteTiempo)
        {
            if (Ambiente != null)
            {
                Debug.Log("Reproduciendo sonido de aves: " + temporizador);
                Ambiente.Play();
                SetNuevoTiempo();
            }
        }
    }

    void SetNuevoTiempo()
    {
        temporizador = 0f;
        siguienteTiempo = Random.Range(tiempoMin, tiempoMax);
    }

}
