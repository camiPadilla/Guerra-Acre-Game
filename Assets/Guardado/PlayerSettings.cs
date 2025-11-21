using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    public static PlayerSettings Instance;

    public float masterVolume;
    public float musicVolume;
    public float sfxVolume;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadFromPrefs();  // Cargar al iniciar el juego
    }

    public void LoadFromPrefs()
    {
        masterVolume = PlayerPrefs.GetFloat("VolMaster", 1f);
        musicVolume  = PlayerPrefs.GetFloat("VolMusic", 1f);
        sfxVolume    = PlayerPrefs.GetFloat("VolSFX", 1f);
    }

    public void SaveToPrefs()
    {
        PlayerPrefs.SetFloat("VolMaster", masterVolume);
        PlayerPrefs.SetFloat("VolMusic", musicVolume);
        PlayerPrefs.SetFloat("VolSFX", sfxVolume);
        PlayerPrefs.Save();
    }
}