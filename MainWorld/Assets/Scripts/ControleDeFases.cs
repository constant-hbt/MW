using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleDeFases : MonoBehaviour
{
    
    public int fasesConcluidas = 0;
    [Header("Fases")]
    public GameObject[] btnFases;
    public GameObject[] estradasFases;

    [Header("Territorios")]
    public GameObject[] territorios;
    public GameObject[] cadeados;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if(fasesConcluidas > 9)
        {
            fasesConcluidas = 9;
        }
        controleParaLiberarTerritorios();
        controleLiberarFases(fasesConcluidas);
    }

    void controleParaLiberarTerritorios()
    {
        if(fasesConcluidas > 0 && fasesConcluidas < 3)
        {
            for (int i = 0; i < territorios.Length; i++)
            {
                if (i == 0)
                {
                    territorios[i].SetActive(true);
                }
                else
                {
                    territorios[i].SetActive(false);
                }
            }
        }else if(fasesConcluidas >= 3 && fasesConcluidas < 5)
        {
            for (int i = 0; i < territorios.Length; i++)
            {
                if (i >=0 && i <= 1)
                {
                    
                    territorios[i].SetActive(true);
                    if(i == 1)
                    {
                        cadeados[0].SetActive(false);
                    }
                }
                else
                {
                    territorios[i].SetActive(false);
                }
            }
        }else if (fasesConcluidas >= 5 && fasesConcluidas < 7)
        {
            for (int i = 0; i < territorios.Length; i++)
            {
                if (i>=0 && i <=2 )
                {
                    territorios[i].SetActive(true);
                    if (i == 2)
                    {
                        cadeados[1].SetActive(false);
                    }
                }
                else
                {
                    territorios[i].SetActive(false);
                }
            }
        }else if(fasesConcluidas >= 7 && fasesConcluidas < 8)
        {
            for (int i = 0; i < territorios.Length; i++)
            {
                if (i >=0 && i <= 3)
                {
                    territorios[i].SetActive(true);
                    if (i == 3)
                    {
                        cadeados[2].SetActive(false);
                    }
                }
                else
                {
                    territorios[i].SetActive(false);
                }
            }
        }else if(fasesConcluidas >= 8 && fasesConcluidas <= 9)
        {
            for (int i = 0; i < territorios.Length; i++)
            {
                
                    territorios[i].SetActive(true);
                if (i == 1)
                {
                    cadeados[3].SetActive(false);
                }
            }
        }
    }
    void controleLiberarFases( int fases)
    {
        switch (fases)
        {
            case 0:
                btnFases[0].SetActive(true);// deixa somente a fase 0 ativada
                for (int i = 0; i < estradasFases.Length; i++)
                {
                        estradasFases[i].SetActive(false);
                        btnFases[i + 1].SetActive(false);
                    
                }
                btnFases[0].SetActive(true); //somente a primeira fase será ativada
                break;


            case 1:
               

                for(int i=0; i < estradasFases.Length; i++)
                {

                    if( i== 0)
                    {
                        estradasFases[i].SetActive(true);
                        btnFases[i+1].SetActive(true);// ativa a fase2 e a estrada que liga a ela
                    }
                    else
                    {
                        estradasFases[i].SetActive(false);
                        btnFases[i + 1].SetActive(false);
                    }
                }
                break;


            case 2:
                for (int i = 0; i < estradasFases.Length; i++)
                {

                    if (i >= 0 && i <= 1)
                    {
                        estradasFases[i].SetActive(true);
                        btnFases[i + 1].SetActive(true);// ativa a fase3 e a estrada que liga a ela
                    }
                    else
                    {
                        estradasFases[i].SetActive(false);
                        btnFases[i + 1].SetActive(false);
                    }
                }

                break;

            case 3:
                for (int i = 0; i < estradasFases.Length; i++)
                {

                    if (i >= 0 && i <=2)
                    {
                        estradasFases[i].SetActive(true);
                        btnFases[i + 1].SetActive(true);// ativa a fase4 e a estrada que liga a ela
                    }
                    else
                    {
                        estradasFases[i].SetActive(false);
                        btnFases[i + 1].SetActive(false);
                    }
                }
                break;

            case 4:
                for (int i = 0; i < estradasFases.Length; i++)
                {

                    if (i >= 0 && i <=3)
                    {
                        estradasFases[i].SetActive(true);
                        btnFases[i + 1].SetActive(true);// ativa a fase5 e a estrada que liga a ela
                    }
                    else
                    {
                        estradasFases[i].SetActive(false);
                        btnFases[i + 1].SetActive(false);
                    }
                }
                break;

            case 5:
                for (int i = 0; i < estradasFases.Length; i++)
                {

                    if (i >= 0 && i <= 4)
                    {
                        estradasFases[i].SetActive(true);
                        btnFases[i + 1].SetActive(true);// ativa a fase6 e a estrada que liga a ela
                    }
                    else
                    {
                        estradasFases[i].SetActive(false);
                        btnFases[i + 1].SetActive(false);
                    }
                }
                break;

            case 6:
                for (int i = 0; i < estradasFases.Length; i++)
                {

                    if (i >= 0 && i <= 5)
                    {
                        estradasFases[i].SetActive(true);
                        btnFases[i + 1].SetActive(true);// ativa a fase7 e a estrada que liga a ela
                    }
                    else
                    {
                        estradasFases[i].SetActive(false);
                        btnFases[i + 1].SetActive(false);
                    }
                }
                break;

            case 7:
                for (int i = 0; i < estradasFases.Length; i++)
                {

                    if (i >= 0 && i <= 6)
                    {
                        estradasFases[i].SetActive(true);
                        btnFases[i + 1].SetActive(true);// ativa a fase8 e a estrada que liga a ela
                    }
                    else
                    {
                        estradasFases[i].SetActive(false);
                        btnFases[i + 1].SetActive(false);
                    }
                }
                break;
            case 8:
                for (int i = 0; i < estradasFases.Length; i++)
                {

                    if (i >= 0 && i <= 7)
                    {
                        estradasFases[i].SetActive(true);
                        btnFases[i + 1].SetActive(true);// ativa a fase9 e a estrada que liga a ela
                    }
                    else
                    {
                        estradasFases[i].SetActive(false);
                        btnFases[i + 1].SetActive(false);
                    }
                }
                break;
            case 9:
                //for usado somente para não dar bug e apagar todas as fases ao concluir a fase 9
                
                for(int i=0; i < estradasFases.Length; i++)
                {
                    estradasFases[i].SetActive(true);
                }
                for(int i=0; i < btnFases.Length; i++)
                {
                    btnFases[i].SetActive(true);
                }
                break;
        }
    }
    
}
