using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dado : MonoBehaviour
{
    [SerializeField] List<Sprite> caras = new List<Sprite>();
    [SerializeField] SpriteRenderer miSprite;
    [SerializeField] int cara;
    // Start is called before the first frame update
    void Start()
    {
        Girar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void Girar()
    {
        int random = Random.Range(1, 6);
        miSprite.sprite = caras[random-1];
        print(random);
    }
}
