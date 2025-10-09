using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> objetosRecogibles;
    public static GameManager instancia;
    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    public void InstanciarObjeto(Vector2 posicion)
    {
        int random = Random.Range(0, objetosRecogibles.Count);
        Instantiate(objetosRecogibles[random], posicion, Quaternion.identity);
    }
}
