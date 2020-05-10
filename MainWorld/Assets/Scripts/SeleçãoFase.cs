using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SeleçãoFase : MonoBehaviour
{

    public GameObject barraDeProgresso;
    public TextMeshProUGUI textoProgresso;
    public TextMeshProUGUI tmpFaseSelecionada;
    public float maxProgresso;//no caso como tem 9 fases esse é o maior progresso
    public float progressoAtual;

    
    void Start()
    {
       
    }

    
    void Update()
    {
        barraDeProgresso.transform.localScale = new Vector3(pegarTamanhoBarra(progressoAtual, maxProgresso), barraDeProgresso.transform.localScale.y, barraDeProgresso.transform.localScale.z);
        textoProgresso.text = progressoAtual+"/"+maxProgresso;
        /*
        if(progressoAtual < maxProgresso)
        {
            progressoAtual += Time.deltaTime * 5;
        }
        else                                                            PARA FAZER A BARRA DE PROGRESSO NA TRANSIÇÃO ENTRE FASES
        {
            textoProgresso.text = "100% completo";
        }*/
    }

    public void nomeFase(int id)
    {//responsavel por chamar as fases
        switch (id)
        {
            case 1:
                print("Kington");
                break;
            case 2:
                print("Devon");
                break;
            case 3:
                print("York");
                break;
            case 4:
                print("Wareham");
                break;
            case 5:
                print("Edington");
                break;
            case 6:
                print("Chippenham");
                break;
            case 7:
                print("Wantage");
                break;
            case 8:
                print(" Exeter");
                break;
            case 9:
                print("Wessex");
                break;
        }
    }



    public float pegarTamanhoBarra(float minValor , float maxValor)
    {
        return minValor / maxValor;
    }

    public int pegarPorcentagemBarra(float minValor , float maxValor , int fator)
    {
        return Mathf.RoundToInt( pegarTamanhoBarra(minValor, maxValor) * fator);
    }

    public void nomeFaseSelecionada(string nomeFase)
    {
        tmpFaseSelecionada.text = nomeFase;
    }



}
