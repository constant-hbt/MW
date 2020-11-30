using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PainelPerguntas : MonoBehaviour
{


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


    void Awake()
    {
        btnSalvar.enabled = false;

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

    public void botaoSalvar()
    {
        Debug.Log("Resposta da pergunta 1 = " + respostaPerg1);
        Debug.Log("Resposta da pergunta 2 = " + respostaPerg2);
        Debug.Log("Resposta da pergunta 3 = " + respostaPerg3);


    }
}
