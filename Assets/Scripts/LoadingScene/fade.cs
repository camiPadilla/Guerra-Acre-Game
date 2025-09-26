using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fade : MonoBehaviour
{
    public Animator animatorFade;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("FadeOut", 20f);
    }

    // Update is called once per frame
    public void FadeOut()
    {
        animatorFade.Play("FadeOut");
    }
}
