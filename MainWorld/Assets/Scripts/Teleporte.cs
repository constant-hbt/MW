using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class Teleporte : MonoBehaviour
{
    /// <summary>
    /// RESPONSAVEL PELOS OBJETOS DE TELEPORTE DO PLAYER DE UMA PARTE DA FASE PARA OUTRA
    /// </summary>

    private         PlayerController            _playerController;// transform do player
    private         ControllerFase              _controllerFase;
    public          Transform                   destino;
    public          Camera                      cam;
    public          Transform[]                 transicaoCamera;//posicao que a camera deve se encontrar ao mudar de uma parte da fase para outra

    [Header("Configuração de Limite de blocos")]
    public          int                         blocosDisponiveis;
    [DllImport("__Internal")]
    public static extern void            SistemaLimiteBloco(int qtdBlocoFase);

    [DllImport("__Internal")]
    public static extern void            SistemaReiniciarWorkspaceBlockly();
    
    void Start()
    {
        _playerController = FindObjectOfType(typeof(PlayerController)) as PlayerController;
        _controllerFase = FindObjectOfType(typeof(ControllerFase)) as ControllerFase;
    }
    void Update()
    {
        
    }

    void interagindo()
    {
       
        SistemaReiniciarWorkspaceBlockly();//Ao teleportar para outra etapa da fase reseta o espaco blockly
        _controllerFase.fases[1].SetActive(true);//habilita a próxima parte da fase
        _playerController.zerarVelocidadeP();//zera a velocidade do player
        _playerController.transform.position = destino.position;//muda a posição do player para o inicio da proxima parte da fase
        cam.transform.position = transicaoCamera[0].position;//muda a posição da câmera para a proxima parte da fase
        SistemaLimiteBloco(blocosDisponiveis);//Ao teleportar para outra etapa da fase modifica o limite de blocos para aquela parte da fase
        _controllerFase.fases[0].SetActive(false);//desabilita a parte anterior da fase
        _playerController.qtdBlocosUsados = -1;//necessário para não entrar no if que habilita o painel de fase incompleta , pois ao iniciar a próxima etapa o usuário necessitará de tempo ate dispor os blocos
    }
}
