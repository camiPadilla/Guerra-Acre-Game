using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fade : MonoBehaviour
{
    public Animator animatorFade;
    public float timeFade;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("FadeOut", timeFade);
    }

    // Update is called once per frame
    public void FadeOut()
    {
        animatorFade.Play("FadeOut");
    }
}
