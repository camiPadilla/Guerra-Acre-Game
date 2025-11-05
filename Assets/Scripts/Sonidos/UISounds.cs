using UnityEngine;
using FMODUnity;
using UnityEngine.UI;
using FMOD.Studio;
using static UnityEngine.Rendering.DebugUI;

public class UISounds : MonoBehaviour
{
    [SerializeField] EventReference hoverEvent;
    [SerializeField] EventReference clickEvent;
    [SerializeField] EventReference click2Event;
    [SerializeField] EventReference hoverJochi;


    [SerializeField] private Scrollbar masterVolumeREF;
    [SerializeField] private Scrollbar musicFaderREF;
    [SerializeField] private Scrollbar SFXFaderREF;

    [SerializeField] private Scrollbar masterVolume;
    [SerializeField] private Scrollbar musicFader;
    [SerializeField] private Scrollbar SFXFader;


    /*private void Awake()
    {
        int managers = FindObjectsOfType<UISounds>().Length;
        if (managers > 1)
        {
            Destroy(gameObject);
            return;
        }

        //DontDestroyOnLoad(this.gameObject);
    }*/

    void Update()
    {        
        if (masterVolume && musicFader && SFXFader)
        {
            if (masterVolumeREF && musicFaderREF && SFXFaderREF)
            {
                masterVolume.value = masterVolumeREF.value;
                musicFader.value = musicFaderREF.value;
                SFXFader.value = SFXFaderREF.value;
            }
            ActualizarMasterVolume();
            ActualizarMusicVolume();
            ActualizarSFXVolume();
        }
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
        
        //float volume = masterVolume.value;
        //Debug.Log("Valor actual del Scrollbar: " + volume);

        RuntimeManager.StudioSystem.setParameterByName("MasterFader", masterVolume.value);
        //RuntimeManager.StudioSystem.getParameterByName("MasterFader", out float value);
        //Debug.Log("Valor actual del MasterFader: " + value);
    }
    public void ActualizarMusicVolume()
    {

        //float volume = musicFader.value;
        //Debug.Log("Valor actual del Scrollbar: " + volume);

        RuntimeManager.StudioSystem.setParameterByName("MusicFader", musicFader.value);
        //RuntimeManager.StudioSystem.getParameterByName("MusicFader", out float value);
        //Debug.Log("Valor actual del MusicFader: " + value);
    }
    public void ActualizarSFXVolume()
    {

        //float volume = SFXFader.value;
        //Debug.Log("Valor actual del Scrollbar: " + volume);

        RuntimeManager.StudioSystem.setParameterByName("SFXFader", SFXFader.value);
        //RuntimeManager.StudioSystem.getParameterByName("SFXFader", out float value);
        //Debug.Log("Valor actual del SFXFader: " + value);
    }
}
