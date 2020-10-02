﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class TelaCarregamento : MonoBehaviour
{
    public string cenaACarregar;//cena que irei carregar
    public float tempoFixoSeg = 7;
   /* public enum TipoCarregamento { Carregamento, TempoFixo};
    public TipoCarregamento tipoDeCarregamento;
    public Image barraDeCarregamento;
    public TextMeshProUGUI textoProgresso;
    private int progresso = 0;
    private string textoOriginal;*/

    void Start()
    {
       
        /* switch (tipoDeCarregamento)
         {
             case TipoCarregamento.Carregamento:
                 StartCoroutine(cenaCarregamento(cenaACarregar));
                 break;
             case TipoCarregamento.TempoFixo:
                 StartCoroutine(tempoFixo(cenaACarregar));
                 break;
         }

         if(textoProgresso != null)
         {
             textoOriginal = textoProgresso.text;
         }
         if (barraDeCarregamento != null)
         {
             barraDeCarregamento.type = Image.Type.Filled;
             barraDeCarregamento.fillMethod = Image.FillMethod.Horizontal;
             barraDeCarregamento.fillOrigin = (int)Image.OriginHorizontal.Left;
         }*/
    }

    void Update()
    {
        /*switch (tipoDeCarregamento)
        {
            case TipoCarregamento.Carregamento:
                StartCoroutine(cenaCarregamento(cenaACarregar));
                break;
            case TipoCarregamento.TempoFixo:
                progresso = (int)( Mathf.Clamp((Time.time / tempoFixoSeg),0.0f,1.0f)* 100.0f);
                break;
        }
        if (textoProgresso != null)
        {
            textoProgresso.text = textoOriginal + " " + progresso + "%";
        }
        if (barraDeCarregamento != null)
        {
            barraDeCarregamento.fillAmount = (progresso / 100.0f);
        }*/
    }

    /*IEnumerator cenaCarregamento(string cena)
    {
        //AsyncOperation responsavel por descobrir a porcentagem de carregamento exato dos componentes de determinada cena
        AsyncOperation carregamento = SceneManager.LoadSceneAsync(cena);
        while (!carregamento.isDone)
        {
            progresso = (int)(carregamento.progress * 100.0f);
            yield return null;
        }
    }*/
    public void chamarTelaCarregamento(string cena)
    {
        StartCoroutine(tempoFixo(cena));
    }
    IEnumerator tempoFixo(string cena)
    {
        yield return new WaitForSeconds(tempoFixoSeg);
        SceneManager.LoadScene(cena);
    }
}
