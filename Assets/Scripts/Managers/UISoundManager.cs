using UnityEngine;
using UnityEngine.UI;

public class IUSoundsManager : MonoBehaviour
{
    public Scrollbar masterScrollbar;
    public Scrollbar musicScrollbar;
    public Scrollbar sfxScrollbar;
    private void Start()
    {
        masterScrollbar.value = PlayerSettings.Instance.masterVolume;
        musicScrollbar.value  = PlayerSettings.Instance.musicVolume;
        sfxScrollbar.value    = PlayerSettings.Instance.sfxVolume;
        masterScrollbar.onValueChanged.AddListener(v =>
        {
            PlayerSettings.Instance.masterVolume = v;
            PlayerSettings.Instance.SaveToPrefs();
        });

        musicScrollbar.onValueChanged.AddListener(v =>
        {
            PlayerSettings.Instance.musicVolume = v;
            PlayerSettings.Instance.SaveToPrefs();
        });

        sfxScrollbar.onValueChanged.AddListener(v =>
        {
            PlayerSettings.Instance.sfxVolume = v;
            PlayerSettings.Instance.SaveToPrefs();
        });
    }
}