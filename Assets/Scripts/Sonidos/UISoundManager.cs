using UnityEngine;
using FMODUnity;
using UnityEngine.UI;
using FMOD.Studio;
using static UnityEngine.Rendering.DebugUI;

public class UISoundManager : MonoBehaviour
{
    [SerializeField] EventReference hoverEvent;
    [SerializeField] EventReference clickEvent;
    [SerializeField] EventReference click2Event;
    [SerializeField] EventReference hoverJochi;
    [SerializeField] EventReference music;

    [SerializeField] private Scrollbar masterVolume;
    private void Start()
    {
    
    }
    void Update()
    {        
        ActualizarMasterVolume();   
    }

    public void PlayHover()
    {
        if (!hoverEvent.IsNull)
            RuntimeManager.PlayOneShot(hoverEvent);
    }

    public void PlayClick()
    {
        if (!clickEvent.IsNull)
            RuntimeManager.PlayOneShot(click2Event);
    }

    public void PlayClickDos()
    {
        if (!clickEvent.IsNull)
            RuntimeManager.PlayOneShot(clickEvent);
    }

    public void PlayHoverJochi()
    {
        if (!hoverJochi.IsNull)
            RuntimeManager.PlayOneShot(hoverJochi);
    }

    public void ActualizarMasterVolume() 
    {
        
        float volume = masterVolume.value;
        //Debug.Log("Valor actual del Scrollbar: " + volume);

        RuntimeManager.StudioSystem.setParameterByName("MasterFader", volume);
        RuntimeManager.StudioSystem.getParameterByName("MasterFader", out float value);
        //Debug.Log("Valor actual del MasterFader: " + value);
    }
}
