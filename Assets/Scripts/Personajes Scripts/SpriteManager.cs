using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Jugador1;
    [SerializeField] private GameObject[] Jugador2;
    [SerializeField] private GameObject[] c_Rmano, a_Rmano,m_Machete, m_Carrada, m_Fusil, c_Machete, s_Machete,  caraHerido;
    [SerializeField] private GameObject armadura1, armadura2;
    [SerializeField] private int jugador;

    void Start()
    {
        CambiarJugador();
    }
    public void CambiarArma(int arma)
    {
        switch (arma)
        {
            case 0:
                c_Rmano[jugador].SetActive(true);
                a_Rmano[jugador].SetActive(false);
                m_Machete[jugador].SetActive(true);
                m_Carrada[jugador].SetActive(false);
                m_Fusil[jugador].SetActive(false);
                c_Machete[jugador].SetActive(false);
                s_Machete[jugador].SetActive(true);
                break;
            case 1:
                c_Rmano[jugador].SetActive(true);
                a_Rmano[jugador].SetActive(false);
                m_Machete[jugador].SetActive(false);
                m_Carrada[jugador].SetActive(true);
                m_Fusil[jugador].SetActive(false);
                c_Machete[jugador].SetActive(true);
                s_Machete[jugador].SetActive(false); 
                break;
            case 2:
                c_Rmano[jugador].SetActive(false);
                a_Rmano[jugador].SetActive(true);
                m_Machete[jugador].SetActive(false);
                m_Carrada[jugador].SetActive(false);
                m_Fusil[jugador].SetActive(true);
                c_Machete[jugador].SetActive(true);
                s_Machete[jugador].SetActive(false);
                break;
        }
    }
    
    public void CambiarJugador()
    {
        if (jugador == 0)
        {
            for (int i = 0; i < Jugador1.Length; i++)
            {
                Jugador1[i].SetActive(true);
                Jugador2[i].SetActive(false);
            }
        }else if (jugador == 1)
        {
            for (int i = 0; i < Jugador2.Length; i++)
            {
                Jugador1[i].SetActive(false);
                Jugador2[i].SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
