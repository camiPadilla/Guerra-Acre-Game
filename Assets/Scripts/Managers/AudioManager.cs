using UnityEngine;
using FMODUnity;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] UISoundManager uiSoundManager;
    [SerializeField] public Scrollbar masterVolume;
    [SerializeField] public Scrollbar musicFader;
    [SerializeField] public Scrollbar SFXFader;
    private void Awake()
    {
        CargarDesdePrefs();
        masterVolume.onValueChanged.AddListener(OnValorCambiado);
        musicFader.onValueChanged.AddListener(OnValorCambiado);
        SFXFader.onValueChanged.AddListener(OnValorCambiado);
    }
    void Update()
    {
        if (masterVolume && musicFader && SFXFader)
        {
            if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                uiSoundManager = FindAnyObjectByType<UISoundManager>();
                masterVolume.value = uiSoundManager.masterVolumeREF.value;
                musicFader.value = uiSoundManager.musicFaderREF.value;
                SFXFader.value = uiSoundManager.SFXFaderREF.value;
            }
            ActualizarMasterVolume();
            ActualizarMusicVolume();
            ActualizarSFXVolume();
        }
    }
    void OnValorCambiado(float valor)
    {
        GuardarEnPrefs();
        //Debug.Log("Valor de volumen cambiado y guardado en PlayerPrefs: " + valor);
    }
    public void ActualizarMasterVolume()
    {
        RuntimeManager.StudioSystem.setParameterByName("MasterFader", masterVolume.value);
    }
    public void ActualizarMusicVolume()
    {
        RuntimeManager.StudioSystem.setParameterByName("MusicFader", musicFader.value);
    }
    public void ActualizarSFXVolume()
    {
        RuntimeManager.StudioSystem.setParameterByName("SFXFader", SFXFader.value);
    }

    public void CargarDesdePrefs()
    {
        if (masterVolume)
            masterVolume.value = PlayerPrefs.GetFloat("VolMaster", masterVolume.value);

        if (musicFader)
            musicFader.value = PlayerPrefs.GetFloat("VolMusic", musicFader.value);

        if (SFXFader)
            SFXFader.value = PlayerPrefs.GetFloat("VolSFX", SFXFader.value);
    }

    public void GuardarEnPrefs()
    {
        if (masterVolume)
            PlayerPrefs.SetFloat("VolMaster", masterVolume.value);

        if (musicFader)
            PlayerPrefs.SetFloat("VolMusic", musicFader.value);

        if (SFXFader)
            PlayerPrefs.SetFloat("VolSFX", SFXFader.value);

        PlayerPrefs.Save();
    }
}
