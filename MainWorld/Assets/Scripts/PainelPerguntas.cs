using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PainelPerguntas : MonoBehaviour
{
    public TextMeshProUGUI tmpPergunta;
    
    public Toggle opcaoA;
    public Toggle opcaoB;
    public Toggle opcaoC;
    public Toggle opcaoD;
    public Toggle opcaoE;

    public TextMeshProUGUI tmpOpcaoA;
    public TextMeshProUGUI tmpOpcaoB;
    public TextMeshProUGUI tmpOpcaoC;
    public TextMeshProUGUI tmpOpcaoD;
    public TextMeshProUGUI tmpOpcaoE;

    public TMP_InputField respDescritiva;

    public Button btnSalvar;

    public string resposta; //resposta que sera enviada ao banco

    void Awake()
    {
        btnSalvar.enabled = false;
        
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        if (opcaoA.isOn || opcaoB.isOn || opcaoC.isOn || opcaoD.isOn || opcaoE.isOn || respDescritiva.text != "")
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
        if (opcaoA.isOn)
        {
            resposta = tmpOpcaoA.text;
        }else if (opcaoB.isOn)
        {
            resposta = tmpOpcaoB.text;
        }
        else if (opcaoC.isOn)
        {
            resposta = tmpOpcaoC.text;
        }
        else if (opcaoD.isOn)
        {
            resposta = tmpOpcaoD.text;
        }
        else if (opcaoE.isOn)
        {
            resposta = tmpOpcaoE.text;
        }

    }
}
