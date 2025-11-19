using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    [SerializeField] private GameObject[] jugador1;
    [SerializeField] private GameObject[] jugador2;
    [SerializeField] private GameObject[] c_Rmano, fusil_Rmano,m_Machete, m_Carrada, m_Abierta, c_Machete, s_Machete,  caraHerido;
    [SerializeField] private GameObject armadura1, armadura2;
    [SerializeField] private int jugador;

    private void Awake()
    {
        OcultarTodo();

    }
    void Start()
    {
        CambiarJugador();
    }
    public void EquiparArmadura(int armadura)
    {
        switch (armadura)
        {
            case 0:
                armadura1.SetActive(false);
                armadura2.SetActive(false);
                break;
            case 1:
                armadura1.SetActive(true);
                armadura2.SetActive(false);
                break;
            case 2:
                armadura1.SetActive(true);
                armadura2.SetActive(true);
                break;
        }
    }

    public void CambiarArma(int arma)
    {
        switch (arma)
        {
            case 0:
                c_Rmano[jugador].SetActive(true);
                fusil_Rmano[jugador].SetActive(false);
                m_Machete[jugador].SetActive(true);
                m_Carrada[jugador].SetActive(false);
                m_Abierta[jugador].SetActive(false);
                c_Machete[jugador].SetActive(false);
                s_Machete[jugador].SetActive(true);
                break;
            case 1:
                c_Rmano[jugador].SetActive(true);
                fusil_Rmano[jugador].SetActive(false);
                m_Machete[jugador].SetActive(false);
                m_Carrada[jugador].SetActive(true);
                m_Abierta[jugador].SetActive(false);
                c_Machete[jugador].SetActive(true);
                s_Machete[jugador].SetActive(false); 
                break;
            case 2:
                c_Rmano[jugador].SetActive(false);
                fusil_Rmano[jugador].SetActive(true);
                m_Machete[jugador].SetActive(false);
                m_Carrada[jugador].SetActive(false);
                m_Abierta[jugador].SetActive(true);
                c_Machete[jugador].SetActive(true);
                s_Machete[jugador].SetActive(false);
                break;
        }
    }
    
    public void CaraHerido(bool entrada)
    {
        caraHerido[jugador].SetActive(entrada);
    }

    public void CambiarJugador()
    {
        if (jugador == 0)
        {
            for (int i = 0; i < jugador1.Length; i++)
            {
                jugador1[i].SetActive(true);
                jugador2[i].SetActive(false);
            }
        }else if (jugador == 1)
        {
            for (int i = 0; i < jugador2.Length; i++)
            {
                jugador1[i].SetActive(false);
                jugador2[i].SetActive(true);
            }
        }
    }
    private void OcultarTodo()
    {
        for (int i = 0; i < jugador1.Length; i++)
        {
            jugador1[i].SetActive(false);
            jugador2[i].SetActive(false);
        }for (int i = 0; i < c_Rmano.Length; i++)
        {
            c_Rmano[i].SetActive(false); 
            fusil_Rmano[i].SetActive(false); 
            m_Machete[i].SetActive(false); 
            m_Carrada[i].SetActive(false); 
            m_Abierta[i].SetActive(false); 
            c_Machete[i].SetActive(false); 
            s_Machete[i].SetActive(false); 
            caraHerido[i].SetActive(false);
        }
        armadura1.SetActive(false); 
        armadura2.SetActive(false);

    }

}
