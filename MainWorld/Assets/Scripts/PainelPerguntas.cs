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

    [Header("Perguntas")]

    public GameObject[] pergunta;

    public TextMeshProUGUI[] tmpDescricao;

    public GameObject[] alternativaPergunta1;
    public GameObject[] alternativaPergunta2;
    public GameObject[] alternativaPergunta3;

    public TextMeshProUGUI[] tmpAlternativaPergunta1;
    public TextMeshProUGUI[] tmpAlternativaPergunta2;
    public TextMeshProUGUI[] tmpAlternativaPergunta3;

    public Toggle[] opcao1;
    public Toggle[] opcao2;
    public Toggle[] opcao3;
    public Toggle[] opcao4;
    public Toggle[] opcao5;
    public Toggle[] opcao6;

    public GameObject[] inpRespDescritiva;
    
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
      /*  if(opcaoA1.isOn)
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
                */

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
    void GetPerguntas(Perguntas objPerguntas)
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

        int tamanhoPerguntas = objPerguntas.perguntas.Length;
        ativarComponentes(tamanhoPerguntas, objPerguntas);
        

        
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

    void ativarComponentes(int qtdPerguntas,Perguntas objPerguntas)
    {
        
        //habilitando os gameObjects das perguntas
        for(int i=0; i<qtdPerguntas; i++)
        {
            pergunta[i].SetActive(true);
        }
            //Preenchendo as descricoes das perguntas e suas respectivas alternativas
            if(qtdPerguntas == 1)
            {
                string[] alternativas1 = objPerguntas.perguntas[0].alternativas.Split('-');//preenche o array a partir do recorte das strings contidas dentro de perguntas[0].alternativas
                
                
                tmpDescricao[0].text = objPerguntas.perguntas[0].descricao;//preenche o tmpDescricao com a descricao da pergunta vinda do banco

                    for(int i = 0; i < alternativas1.Length; i++)//percorre o vetor de contendo a descricao das alternativas , habilitando os tmp de cada alternativa e as preenchendo com o conteudo
                    {
                        alternativaPergunta1[i].SetActive(true);
                        
                        tmpAlternativaPergunta1[i].text = alternativas1[i].ToString();
                    }
            }else if(qtdPerguntas == 2)
            {
                string[] alternativas1 = objPerguntas.perguntas[0].alternativas.Split('-');
                string[] alternativas2 = objPerguntas.perguntas[1].alternativas.Split('-');
                
                
                tmpDescricao[0].text = objPerguntas.perguntas[0].descricao;
                tmpDescricao[1].text = objPerguntas.perguntas[1].descricao;
                
                
                for (int i = 0; i < alternativas1.Length; i++)
                    {
                        alternativaPergunta1[i].SetActive(true);
                        tmpAlternativaPergunta1[i].text = alternativas1[i].ToString();
                    }
                    for (int x = 0; x < alternativas2.Length; x++)
                    {
                        alternativaPergunta2[x].SetActive(true);
                        tmpAlternativaPergunta2[x].text = alternativas2[x].ToString();
                    }
            }
            else if(qtdPerguntas == 3)
            {
                 string[] alternativas1 = objPerguntas.perguntas[0].alternativas.Split('-');
                 string[] alternativas2 = objPerguntas.perguntas[1].alternativas.Split('-');
                 string[] alternativas3 = objPerguntas.perguntas[2].alternativas.Split('-');

                tmpDescricao[0].text = objPerguntas.perguntas[0].descricao;
                tmpDescricao[1].text = objPerguntas.perguntas[1].descricao;
                tmpDescricao[2].text = objPerguntas.perguntas[2].descricao;

                if(alternativas1.Length > 0 && alternativas1[0] != "")
                {
                    for (int i = 0; i < alternativas1.Length; i++)
                    {
                        alternativaPergunta1[i].SetActive(true);
                        tmpAlternativaPergunta1[i].text = alternativas1[i].ToString();
                    }
                }
                else
                {
                    inpRespDescritiva[0].SetActive(true);
                }
                    if(alternativas2.Length > 0 && alternativas2[0] != "")
                    {
                        for (int x = 0; x < alternativas2.Length; x++)
                        {
                            alternativaPergunta2[x].SetActive(true);
                            tmpAlternativaPergunta2[x].text = alternativas2[x].ToString();
                        }
                    }
                    else
                    {
                        inpRespDescritiva[1].SetActive(true);
                    }
                    

                        if(alternativas3.Length > 0 && alternativas3[0] != "")
                        {
                            
                            for (int y = 0; y < alternativas3.Length; y++)
                            {
                                alternativaPergunta3[y].SetActive(true);
                                tmpAlternativaPergunta3[y].text = alternativas3[y].ToString();
                            }
                        }
                        else
                        {
                            
                            inpRespDescritiva[2].SetActive(true);
                        }
                    
            }
                
                
    }
}
