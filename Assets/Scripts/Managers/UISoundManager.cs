using UnityEngine;
using FMODUnity;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class UISoundManager : MonoBehaviour
{
    [SerializeField] public Scrollbar masterVolumeREF;
    [SerializeField] public Scrollbar musicFaderREF;
    [SerializeField] public Scrollbar SFXFaderREF;

    private void Awake()
    {
        CargarDesdePrefs();
    }

    public void CargarDesdePrefs()
    {
        if (masterVolumeREF)
            masterVolumeREF.value = PlayerPrefs.GetFloat("VolMaster");

        if (musicFaderREF)
            musicFaderREF.value = PlayerPrefs.GetFloat("VolMusic");

        if (SFXFaderREF)
            SFXFaderREF.value = PlayerPrefs.GetFloat("VolSFX");
    }
}
