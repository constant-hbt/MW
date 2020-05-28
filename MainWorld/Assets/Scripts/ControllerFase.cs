using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class ControllerFase : MonoBehaviour
{
    /// <summary>
    /// RESPONSAVEL PELAS CONFIGURAÇÕES UNIVERSAIS DENTRO DE CADA FASE
    /// </summary>
    private GameController _gameController;

    [Header("Coletáveis durante a fase")]
    public int gold;//todas as moedas coletadas durante a fase;
    public int estrelas;//estrelas adquiras com o desempenho na fase

    public GameObject[] fases;

    //Configuração do Limite de blocos por fase
    //Ao iniciar a fase a funcao SistemaLimiteBloco muda o campo no html da pagina que delimita a quantidade de bloco
    //maximos que pode ser utilizado durante aquela fase
    [Header("Configuração de Limite de blocos")]
    public int blocosDisponiveis;
    [DllImport("__Internal")]
    private static extern void SistemaLimiteBloco(int qtdBlocoFase);

    void Start()
    {
        SistemaLimiteBloco(blocosDisponiveis);

        _gameController = FindObjectOfType(typeof(GameController)) as GameController;

        if(fases.Length != 0)
        {
            foreach (GameObject o in fases)
            {
                o.SetActive(false);
            }//desabilita todas as partes da fase , e em seguida habilita somente a primeira parte
            fases[0].SetActive(true);
        }
    }

   
    void Update()
    {
        
    }
}
