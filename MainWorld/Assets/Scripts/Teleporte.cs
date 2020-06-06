using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class Teleporte : MonoBehaviour
{
    /// <summary>
    /// RESPONSAVEL PELOS OBJETOS DE TELEPORTE DO PLAYER DE UMA PARTE DA FASE PARA OUTRA
    /// </summary>

    private PlayerController _playerController;// transform do player
    private ControllerFase _controllerFase;
    public Transform destino;
    public Camera cam;
    public Transform[] transicaoCamera;//posicao que a camera deve se encontrar ao mudar de uma parte da fase para outra

    [Header("Configuração de Limite de blocos")]
    public int blocosDisponiveis;
    [DllImport("__Internal")]
    public static extern void SistemaLimiteBloco(int qtdBlocoFase);

    [DllImport("__Internal")]
    public static extern void SistemaReiniciarWorkspaceBlockly();
    [DllImport("__Internal")]
    public static extern void SistemaVerifConclusaoFase(string situacaoFase);
    void Start()
    {
        _playerController = FindObjectOfType(typeof(PlayerController)) as PlayerController;
        _controllerFase = FindObjectOfType(typeof(ControllerFase)) as ControllerFase;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void interagindo()
    {
        SistemaVerifConclusaoFase("passeiFase");//quando os blocos acabarem de executar , nao irá executar as config de painel de derrota
        //Ao teleportar para outra etapa da fase reseta o espaco blockly
        SistemaReiniciarWorkspaceBlockly();
        _controllerFase.fases[1].SetActive(true);
        _playerController.zerarVelocidadeP();
        _playerController.transform.position = destino.position;
        cam.transform.position = transicaoCamera[0].position;
        //Ao teleportar para outra etapa da fase modifica o limite de blocos para aquela parte da fase
        SistemaLimiteBloco(blocosDisponiveis);
       _controllerFase.fases[0].SetActive(false);//desabilita a parte anterior da fase
    }
}
