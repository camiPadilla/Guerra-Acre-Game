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
    private void OnEnable()
    {
        SoundEvents.PauseSound += PlayClick;
    }
    void Update()
    { 

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
}
