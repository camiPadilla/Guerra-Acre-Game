using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Tutorial : MonoBehaviour
{
    public string tutoriatxt;
    [SerializeField] TMP_Text textoTu;
    //[SerializeField] GameObject tutorialA;
    [SerializeField] GameObject textGame;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            textGame.SetActive(true);
            textoTu.text = tutoriatxt;
        }
        
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textGame.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
