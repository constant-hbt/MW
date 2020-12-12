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

    public Toggle[] togglePergunta1;
    public Toggle[] togglePergunta2;
    public Toggle[] togglePergunta3;

    public GameObject[] objRespDescritiva;
    public TMP_InputField[] inpRespDescritiva;
    
    //Verifica se as respostas foram preenchidas
    public bool validarResp1;
    public bool validarResp2;
    public bool validarResp3;
    public int qtdPerguntas;

    public Button btnSalvar;

    //Captura as respostas

    public string respostaPerg1;
    public string respostaPerg2;
    public string respostaPerg3;
    public int id_pergunta1;
    public int id_pergunta2;
    public int id_pergunta3;

    //valida os toggle de cada pergunta para captar as respostas
    public bool flagPergunta1;
    public int qtdAlternativasPergunta1;
    public int qtdToggle1Vazio;
    public bool flagPergunta2;
    public int qtdAlternativasPergunta2;
    public int qtdToggle2Vazio;
    public bool flagPergunta3;
    public int qtdAlternativasPergunta3;
    public int qtdToggle3Vazio;

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
        if(flagPergunta1 && qtdAlternativasPergunta1 > 0)
        {
            qtdToggle1Vazio = 0;
            for(int i=0; i< qtdAlternativasPergunta1; i++)
            {
                if (togglePergunta1[i].isOn)
                {
                    validarResp1 = true;
                    respostaPerg1 = tmpAlternativaPergunta1[i].text;
                }
                else
                {
                    qtdToggle1Vazio += 1;
                }
            }

            if(qtdToggle1Vazio == qtdAlternativasPergunta1)
            {
                validarResp1 = false;
                respostaPerg1 = "";
            }

        }else if(flagPergunta1 && qtdAlternativasPergunta1 <= 0)
        {
            if (inpRespDescritiva[0].text != "")
            {
                validarResp1 = true;
                respostaPerg1 = inpRespDescritiva[0].text;
            }
            else
            {
                validarResp1 = false;
                respostaPerg1 = "";
            }
        }
     

        //CAPTA E VALIDA AS RESPOSTAS DA PERGUNTA 2
        if(flagPergunta2 && qtdAlternativasPergunta2 > 0)
        {
            qtdToggle2Vazio = 0;
            for(int y=0; y< qtdAlternativasPergunta2; y++)
            {
                if (togglePergunta2[y].isOn)
                {
                    validarResp2 = true;
                    respostaPerg2 = tmpAlternativaPergunta2[y].text;
                }
                else
                {
                    qtdToggle2Vazio += 1;
                }
            }

            if(qtdToggle2Vazio == qtdAlternativasPergunta2)
            {
                validarResp2 = false;
                respostaPerg2 = "";
            }
        }else if(flagPergunta2 &&  qtdAlternativasPergunta2 <= 0)
        {
            if (inpRespDescritiva[1].text != "")
            {
                validarResp2 = true;
                respostaPerg2 = inpRespDescritiva[1].text;
            }
            else
            {
                validarResp2 = false;
                respostaPerg2 = "";
            }
        }
        


        //CAPTA E VALIDA AS RESPOSTAS DA PERGUNTA 3
        if (flagPergunta3 && qtdAlternativasPergunta3 > 0)
        {
            qtdToggle3Vazio = 0;
            for (int z = 0; z < qtdAlternativasPergunta3; z++)
            {
                if (togglePergunta3[z].isOn)
                {
                    validarResp3 = true;
                    respostaPerg3 = tmpAlternativaPergunta3[z].text;
                }
                else
                {
                    qtdToggle3Vazio += 1;
                }
            }

            if(qtdToggle3Vazio == qtdAlternativasPergunta3)
            {
                validarResp3 = false;
                respostaPerg3 = "";
            }
        }
        else if (flagPergunta3 && qtdAlternativasPergunta3 <= 0)
        {
            
            if (inpRespDescritiva[2].text != "")
            {
                validarResp3 = true;
                respostaPerg3 = inpRespDescritiva[2].text;
            }
            else
            {
                validarResp3 = false;
                respostaPerg3 = "";
            }
        }

        if (qtdPerguntas == 1 && validarResp1 || qtdPerguntas == 2 && validarResp1 && validarResp2 || qtdPerguntas == 3 && validarResp1 && validarResp2 && validarResp3)
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
        //preenchendo o id de cada pergunta
        for(int i=0; i<objPerguntas.perguntas.Length; i++)
        {
            switch (i){
                case 0:
                    id_pergunta1 = objPerguntas.perguntas[i].id_pergunta;
                    break;
                case 1:
                    id_pergunta2 = objPerguntas.perguntas[i].id_pergunta;
                    break;
                case 2:
                    id_pergunta3 = objPerguntas.perguntas[i].id_pergunta;
                    break;
            }
        }
        
        qtdPerguntas = objPerguntas.perguntas.Length;
        ativarComponentes(qtdPerguntas, objPerguntas);
        
    }

    //Chama a função para pegar as perguntas no banco
    public void CaptarPerguntas(int id_fase)
    {
        _pergunta_Controller.ChamarPegarPergunta(id_fase, GetPerguntas);
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
                string[] alternativas1 = objPerguntas.perguntas[0].alternativas;

                flagPergunta1 = true;
                tmpDescricao[0].text = objPerguntas.perguntas[0].descricao;//preenche o tmpDescricao com a descricao da pergunta vinda do banco

                if (alternativas1.Length > 0 && alternativas1[0] != "")
                {
                    qtdAlternativasPergunta1 = alternativas1.Length;
                    for (int i = 0; i < alternativas1.Length; i++)//percorre o vetor de contendo a descricao das alternativas , habilitando os tmp de cada alternativa e as preenchendo com o conteudo
                    {
                        alternativaPergunta1[i].SetActive(true);
                        
                        tmpAlternativaPergunta1[i].text = alternativas1[i].ToString();
                    }
                }
                else
                {
                    qtdAlternativasPergunta1 = 0;
                    objRespDescritiva[0].SetActive(true);
                }
            }
            else if(qtdPerguntas == 2)
            {
                string[] alternativas1 = objPerguntas.perguntas[0].alternativas;
                string[] alternativas2 = objPerguntas.perguntas[1].alternativas;

                flagPergunta1 = true;
                tmpDescricao[0].text = objPerguntas.perguntas[0].descricao;
                
                flagPergunta2 = true;
                tmpDescricao[1].text = objPerguntas.perguntas[1].descricao;
                

            if (alternativas1.Length > 0 && alternativas1[0] != "")
            {
                qtdAlternativasPergunta1 = alternativas1.Length;
                for (int i = 0; i < alternativas1.Length; i++)
                {
                    alternativaPergunta1[i].SetActive(true);
                    tmpAlternativaPergunta1[i].text = alternativas1[i].ToString();
                }
            }
            else
            {
                qtdAlternativasPergunta1 = 0;
                objRespDescritiva[0].SetActive(true);
            }

                if (alternativas2.Length > 0 && alternativas2[0] != "")
                {
                    qtdAlternativasPergunta2 = alternativas2.Length;
                    for (int x = 0; x < alternativas2.Length; x++)
                    {
                        alternativaPergunta2[x].SetActive(true);
                        tmpAlternativaPergunta2[x].text = alternativas2[x].ToString();
                    }
                }
                else
                {
                    qtdAlternativasPergunta2 = 0;
                    objRespDescritiva[1].SetActive(true);
                }
            }
            else if(qtdPerguntas == 3)
            {
                 string[] alternativas1 = objPerguntas.perguntas[0].alternativas;
                 string[] alternativas2 = objPerguntas.perguntas[1].alternativas;
                 string[] alternativas3 = objPerguntas.perguntas[2].alternativas;

                flagPergunta1 = true;
                tmpDescricao[0].text = objPerguntas.perguntas[0].descricao;
                
                flagPergunta2 = true;
                tmpDescricao[1].text = objPerguntas.perguntas[1].descricao;

                flagPergunta3 = true;
                tmpDescricao[2].text = objPerguntas.perguntas[2].descricao;

                if(alternativas1.Length > 0 && alternativas1[0] != "")
                {
                     qtdAlternativasPergunta1 = alternativas1.Length;
                    for (int i = 0; i < alternativas1.Length; i++)
                    {
                        alternativaPergunta1[i].SetActive(true);
                        tmpAlternativaPergunta1[i].text = alternativas1[i].ToString();
                    }
                }
                else
                {
                    qtdAlternativasPergunta1 = 0;
                    objRespDescritiva[0].SetActive(true);
                }
                    if(alternativas2.Length > 0 && alternativas2[0] != "")
                    {
                        qtdAlternativasPergunta2 = alternativas2.Length;
                        for (int x = 0; x < alternativas2.Length; x++)
                        {
                            alternativaPergunta2[x].SetActive(true);
                            tmpAlternativaPergunta2[x].text = alternativas2[x].ToString();
                        }
                    }
                    else
                    {
                        qtdAlternativasPergunta2 = 0;
                        objRespDescritiva[1].SetActive(true);
                    }
                    

                        if(alternativas3.Length > 0 && alternativas3[0] != "")
                        {
                            qtdAlternativasPergunta3 = alternativas3.Length;
                            for (int y = 0; y < alternativas3.Length; y++)
                            {
                                alternativaPergunta3[y].SetActive(true);
                                tmpAlternativaPergunta3[y].text = alternativas3[y].ToString();
                            }
                        }
                        else
                        {
                            qtdAlternativasPergunta3 = 0;
                            objRespDescritiva[2].SetActive(true);
                        }
                    
            }
                
                
    }

    public void botaoSalvar()
    {
        Resposta resposta = new Resposta();
        resposta.Id_pergunta1 = id_pergunta1;
        resposta.Resposta1 = respostaPerg1;
        resposta.Id_pergunta2 = id_pergunta2;
        resposta.Resposta2 = respostaPerg2;
        resposta.Id_pergunta3 = id_pergunta3;
        resposta.Resposta3 = respostaPerg3;
        
        _pergunta_Controller.ChamarRegistrarResposta(resposta);
        
        SceneManager.LoadScene("SelecaoFase");
    }
}
