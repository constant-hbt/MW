﻿using System.Collections;
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
    private string subTitulo;
    void Start()
    {
        idPainelAtivo = 1;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDisable()
    {
        idPainelAtivo = 1;
        alterarPainelAtivo(idPainelAtivo);
    }
    public void botaoAnterior()
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

    public void botaoProximo()
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
        Debug.Log("Valor de idPainelAtivo = " + numPainelAtivo);
        switch (numPainelAtivo)
        {
            case 1:
                btnAnterior.SetActive(false);
                btnProximo.SetActive(true);
                tmpSubTitulo.text = "Sugestão1";
                break;
            case 2:
                btnAnterior.SetActive(true);
                btnProximo.SetActive(true);
                tmpSubTitulo.text = "Sugestao2";
                break;
            case 3:
                btnAnterior.SetActive(true);
                btnProximo.SetActive(false);
                tmpSubTitulo.text = "Sugestao3";
                break;


        }

        for (int i = 0; i < paineis.Length; i++)
        {
            if ((i + 1) == numPainelAtivo)
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

        
    }
    public void btnClose()
    {
        this.gameObject.SetActive(false);
    }
}
