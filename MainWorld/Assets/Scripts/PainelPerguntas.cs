using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class PainelPerguntas : MonoBehaviour
{
    private Pergunta_Controller _pergunta_Controller;
    private GameController _gameController;

    //PERGUNTA 1
    [Header("PERGUNTA 1")]

    public TextMeshProUGUI tmpPergunta1;

    public Toggle opcaoA1;
    public Toggle opcaoB1;
    public Toggle opcaoC1;

    public TextMeshProUGUI tmpOpcaoA1;
    public TextMeshProUGUI tmpOpcaoB1;
    public TextMeshProUGUI tmpOpcaoC1;

    //PERGUNTA 2
    
    [Header("PERGUNTA 2")]
    public TextMeshProUGUI tmpPergunta2;

    public TMP_InputField respDescritiva;

    //PERGUNTA 3

    [Header("PERGUNTA 3")]

    public TextMeshProUGUI tmpPergunta3;

    public Toggle opcaoA3;
    public Toggle opcaoB3;
    public Toggle opcaoC3;

    public TextMeshProUGUI tmpOpcaoA3;
    public TextMeshProUGUI tmpOpcaoB3;
    public TextMeshProUGUI tmpOpcaoC3;

    //Verifica se as respostas foram preenchidas
    public bool validarResp1;
    public bool validarResp2;
    public bool validarResp3;

    public Button btnSalvar;

    //Captura as respostas

    public string respostaPerg1;
    public string respostaPerg2;
    public string respostaPerg3;
    public int id_pergunta;

    void Awake()
    {
        _pergunta_Controller = FindObjectOfType(typeof(Pergunta_Controller)) as Pergunta_Controller;
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;

        btnSalvar.enabled = false;

        CaptarPerguntas(_gameController.idFaseEmExecucao);
    }

    void Start()
    {
        validarResp1 = false;
        validarResp2 = false;
        validarResp3 = false;
    }

    
    void Update()
    {
        //CAPTA E VALIDA AS RESPOSTAS DA PERGUNTA 1
        if(opcaoA1.isOn)
        {
            validarResp1 = true;
            respostaPerg1 = tmpOpcaoA1.text;

        }else if (opcaoB1.isOn)
        {
            validarResp1 = true;
            respostaPerg1 = tmpOpcaoB1.text;

        }else if (opcaoC1.isOn)
        {
            validarResp1 = true;
            respostaPerg1 = tmpOpcaoC1.text;

        }
        else
        {
            validarResp1 = false;
            respostaPerg1 = "";
        }

            //CAPTA E VALIDA AS RESPOSTAS DA PERGUNTA 2
            if(respDescritiva.text != "")
            {
                validarResp2 = true;
                respostaPerg2 = respDescritiva.text;
            }
            else
            {
                validarResp2 = false;
                respostaPerg2 = "";
            }

                 //CAPTA E VALIDA AS RESPOSTAS DA PERGUNTA 3
                 if (opcaoA3.isOn)
                {
                    validarResp3 = true;
                    respostaPerg3 = tmpOpcaoA3.text;

                }
                else if (opcaoB3.isOn)
                {
                    validarResp3 = true;
                    respostaPerg3 = tmpOpcaoB3.text;

                }
                else if (opcaoC3.isOn)
                {
                    validarResp3 = true;
                    respostaPerg3 = tmpOpcaoC3.text;

                }
                else
                {
                    validarResp3 = false;
                    respostaPerg3 = "";
                }


        if (validarResp1 && validarResp2 && validarResp3)
        {
            btnSalvar.enabled = true;
        }
        else
        {
            btnSalvar.enabled = false;
        }
    }

    //preenche os campos referentes a pergunta no painel
    void GetPerguntas(Perguntas p_pergunta)
    {
       /* tmpPergunta1.text = p_pergunta.pergunta1;
        tmpPergunta2.text = p_pergunta.pergunta2;
        tmpPergunta3.text = p_pergunta.pergunta3;
        id_pergunta = p_pergunta.id;*/

     /*   if(p_pergunta.Pergunta1 != "" && p_pergunta.Pergunta2 != "" && p_pergunta.Pergunta3 != "")
         {
             tmpPergunta1.text = p_pergunta.Pergunta1;
             tmpPergunta2.text = p_pergunta.Pergunta2;
             tmpPergunta3.text = p_pergunta.Pergunta3;
             id_pergunta = p_pergunta.Id;

         } */
    }

    //Chama a função para pegar as perguntas no banco
    public void CaptarPerguntas(int id_fase)
    {
        _pergunta_Controller.ChamarPegarPergunta(id_fase, GetPerguntas);
    }

    public void botaoSalvar()
    {
       

        Resposta resposta = new Resposta();
        resposta.Resposta_pergunta1 = respostaPerg1;
        resposta.Resposta_pergunta2 = respostaPerg2;
        resposta.Resposta_pergunta3 = respostaPerg3;
        resposta.Id_pergunta = id_pergunta;

        _pergunta_Controller.ChamarRegistrarResposta(resposta);
        
        SceneManager.LoadScene("SelecaoFase");
    }
}
