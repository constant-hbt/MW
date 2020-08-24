using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PainelSugestão : MonoBehaviour
{
    public TextMeshProUGUI tmpSubTitulo;
    
    public GameObject[] paineis;

    public GameObject[] botoesPaineis;

    public GameObject btnAnterior;
    public GameObject btnProximo;

    private int idPainelAtivo;

    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public void botaoProximo()
    {
        if(idPainelAtivo == 1)
        {
            idPainelAtivo = 1;
        }else if(idPainelAtivo > 1)
        {
            idPainelAtivo--;
        }

        alterarPainelAtivo(idPainelAtivo);
    }

    public void botaoAnterior()
    {
        if(idPainelAtivo == 3)
        {
            idPainelAtivo = 3;
        }else if(idPainelAtivo < 3)
        {
            idPainelAtivo++;
        }
        alterarPainelAtivo(idPainelAtivo);
    }

    public void alterarPainelAtivo(int numPainelAtivo)
    {
        switch (numPainelAtivo)
        {
            case 1:
                btnAnterior.SetActive(false);

                for(int i=0; i < paineis.Length; i++)
                {
                    if((i + 1) == numPainelAtivo)
                    {
                        paineis[i].SetActive(true);
                        botoesPaineis[i].SetActive(true);
                    }
                    else
                    {
                        paineis[i].SetActive(false);
                        botoesPaineis[i].SetActive(false);
                    }
                }
                break;
            case 2:

                break;
            case 3:

                break;


        }
    }
}
