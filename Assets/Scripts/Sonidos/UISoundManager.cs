using UnityEngine;
using FMODUnity;

public class UISoundManager : MonoBehaviour
{
    [SerializeField] EventReference hoverEvent;
    [SerializeField] EventReference clickEvent;
    [SerializeField] EventReference click2Event;

    

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
}
