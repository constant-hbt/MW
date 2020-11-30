using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;
using System;



public class ControllerFase : MonoBehaviour
{
    /// <summary>
    /// RESPONSAVEL PELAS CONFIGURAÇÕES UNIVERSAIS DENTRO DE CADA FASE
    /// </summary>
    private                 GameController          _gameController;
    private PlayerController _playerController;
    private HUD _hud;
    private Historico_Controller _historico_Controller;
   
    [Header("Coletáveis durante a fase")]
    public int qtdMoedasColetadas;//quantidade de moedas coletadas durante a fase
    public int qtdMoedasLootColetadas; //moedas coletadas por loot adquiridos apartir dos inimigos
    public int estrelas = 0;//estrelas adquiras com o desempenho na fase
    public GameObject[] fases;
    public string data_InicioFase;//computado ao iniciar a fase
    public string data_FimFase;//vai ser computado ao colidir com o runaWin
    public string seqBlocos; //contém a sequência de blocos utilizados para passar a parte da fase
    //Configuração do Limite de blocos por fase
    //Ao iniciar a fase a funcao SistemaLimiteBloco muda o campo no html da pagina que delimita a quantidade de bloco
    //maximos que pode ser utilizado durante aquela fase
    [Header("Distribuição de Estrelas")]
    public                  int                     qtdBlocosDisponiveisEmTodaFase;//quantidade de blocos disponiveis para poder concluir a fase
    public                  int                     qtdMinimaDeBlocosParaConclusao;//quantidade de blocos minimos que devem ser usados para concluir a fase
    public                  int                     qtdBlocosUsados;//quantidade de blocos que foram utilizados para concluir a fase
    public                  int                     qtdMoedasDisponiveis;//quantidade de moedas disponiveis para coleta na fase


    [Header("Quantidade disponivel para a primeira parte da fase")]
    public                  int                   qtdBlocosDisponiveis;//para as fases que tem mais de uma parte o valor depositado aqui valerá para a primeira parte, nas partes subsequentes o valor deverá ser colocado no script que esta contido nos objetos de teleporte
    public int qtdManaDisponivelFase;

    [Header("Configuração de posições iniciais")]
    public GameObject[] posicoesIniciasPlayer;
    public GameObject[] posicoesCamera;
    public Camera camera;
    public float[] posicaoHud;
    //Integração com o js da página
    [DllImport("__Internal")]
    public static extern void                    SistemaLimiteBloco(int qtdBlocoFase, int toolbox);
    

    [DllImport("__Internal")]
    public static extern void                       EnviarQTDBlocosMinimosParaPassarFase(int qtdBlocosMinimos);//envia a quantidade de blocos minimos necessarios para passar a fase


    [DllImport("__Internal")]
    private static extern void SistemaDeEnableDisableBlocos(bool situacao);

    [DllImport("__Internal")]
    public static extern void AlterarToolboxFases(int idFase);





    private void Awake()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        _playerController = FindObjectOfType(typeof(PlayerController)) as PlayerController;
        _hud = FindObjectOfType(typeof(HUD)) as HUD;
        _historico_Controller = FindObjectOfType(typeof(Historico_Controller)) as Historico_Controller;
        //ativa a parte da fase
        if (fases.Length != 0)
        {
            foreach (GameObject o in fases)
            {
                o.SetActive(false);
            }//desabilita todas as partes da fase , e em seguida habilita somente a primeira parte
            fases[_gameController.parteFaseAtual].SetActive(true);

        }


        //inicia o player na parte da fase que ele estava anteriormente, se a fase estiver iniciando ele sera iniciado na parte 0
        _playerController.gameObject.transform.position = new Vector3(posicoesIniciasPlayer[_gameController.parteFaseAtual].transform.position.x, posicoesIniciasPlayer[_gameController.parteFaseAtual].transform.position.y, posicoesIniciasPlayer[_gameController.parteFaseAtual].transform.position.z);
        camera.transform.position = new Vector3(posicoesCamera[_gameController.parteFaseAtual].transform.position.x, posicoesCamera[_gameController.parteFaseAtual].transform.position.y, 0);
       
        if(_gameController.numTentativasFase < 3)
        {
            _hud.gameObject.transform.localPosition = new Vector4(posicaoHud[_gameController.parteFaseAtual], 0, 0, 0);
            
            PainelSugestão _painelIntro = FindObjectOfType(typeof(PainelSugestão)) as PainelSugestão;
            _painelIntro.gameObject.SetActive(false); //desativa o painel de Introducao, porque o player estaria voltando em partes posteriores ao inicio da fase
            SistemaDeEnableDisableBlocos(false); //VER ISSO AQUI
        }

        //variaveis que precisam ter seus valores preenchidos ao iniciar a nova tentativa da fase
        qtdMoedasColetadas = _gameController.qtdMoedasColetadas;
        qtdBlocosUsados = _gameController.qtdBlocosUsados;
        _playerController.parteFase = _gameController.parteFaseAtual;

    }
    void Start()
    {
        
        data_InicioFase = DateTime.Now.ToLocalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");//Pega a data/hora que a fase é iniciada

      
         SistemaLimiteBloco(qtdBlocosDisponiveis,_gameController.idFaseEmExecucao );
         EnviarQTDBlocosMinimosParaPassarFase(qtdMinimaDeBlocosParaConclusao);

        
    }

   
    void Update()
    {
        
      
    }
    private void FixedUpdate()
    {
    }
    public void quantidadeBlocoUsadosNaFase(int qtdBlocos)//passo por parametro os blocos usados de uma variavel que esta na pagina web
    {
        qtdBlocosUsados += qtdBlocos;
    }
    public int distribuicaoEstrelas()
    {
        bool naoTemMoeda = false;
        int moedasColetadas = qtdMoedasColetadas - qtdMoedasLootColetadas;

        if(moedasColetadas < 0)
        {
            moedasColetadas *= -1;
        }

        float metadeMoedaD = qtdMoedasDisponiveis * 0.5f;
       
        if(qtdMoedasDisponiveis == 0)
        {
            naoTemMoeda = true;
        }
        else
        {
            naoTemMoeda = false;
        }

        if (qtdBlocosUsados <= qtdMinimaDeBlocosParaConclusao && moedasColetadas >= qtdMoedasDisponiveis ||
            qtdBlocosUsados <= qtdMinimaDeBlocosParaConclusao && naoTemMoeda)
        {//se eu utilizar o minimo de blocos ou menos e coletar todas as moedas da fase eu ganho 3 estrelas
            estrelas = 3;
            Debug.Log("Consegui somente três estrelas porque usei " + qtdBlocosUsados + " essa qtd de blocos");
        }
        else if(qtdBlocosUsados <= qtdMinimaDeBlocosParaConclusao && moedasColetadas >= metadeMoedaD && moedasColetadas < qtdMoedasDisponiveis && !naoTemMoeda ||
                 qtdBlocosUsados >= qtdMinimaDeBlocosParaConclusao && moedasColetadas >= metadeMoedaD && moedasColetadas <= qtdMoedasDisponiveis && !naoTemMoeda)
        {//se eu usar o minimo ou mais de blocos e coletar mais doque 50% das moedas ganho 2 estrelas
            estrelas = 2;
            Debug.Log("Consegui somente duas estrelas porque usei " + qtdBlocosUsados + " essa qtd de blocos");
        }
        else if(qtdBlocosUsados >= qtdMinimaDeBlocosParaConclusao && moedasColetadas < metadeMoedaD && !naoTemMoeda ||
                qtdBlocosUsados >= qtdMinimaDeBlocosParaConclusao && moedasColetadas == 0 && naoTemMoeda ||
                qtdBlocosUsados <= qtdMinimaDeBlocosParaConclusao && moedasColetadas < metadeMoedaD && !naoTemMoeda  /*Se nao tiver nenhuma moeda para ser coletada e mesmo assim ele utilizar blocos a mais que o minimo*/)
        {//se eu usar o minimo ou mais de blocos e coletar menos doque 50% das moedas ganho 1 estrelas
            estrelas = 1;
            Debug.Log("Consegui somente uma estrelas porque usei " + qtdBlocosUsados + " essa qtd de blocos");
        }
        else
        {
            Debug.Log("Erro aqui no distribuicao Estrelas"); //SE ENTRAR AQUI É QUE TEM ALGO ERRADO NA FUNCAO
            estrelas = 100;
        }
        
        return estrelas;
    }

    //grava os dados obtidos pelo player naquela fase em execucao dentro do GameController, para caso o mesmo ainda possuir alguma tentativa na fase ao reinicia-la o seu desempenho seria mantido
    public void DadosFaseMemoria()
    {
        _gameController.qtdBlocosUsados = qtdBlocosUsados;
        _gameController.qtdMoedasColetadas = qtdMoedasColetadas;
    }

    public void EnviarHistorico(string descricao, int moedas, int estrelas, int vidas, int ultima_fase, int id_usuario_ativ_turma)
    {
        
        //string descricao = "Fase" + _gameController.idFaseEmExecucao + "-Parte" + _gameController.parteFaseAtual + 1;
        //int moedas = qtdMoedasColetadas; //_gameController.numGold;
       // int estrelas = this.estrelas;//_gameController.numEstrelas;
      //  int vidas = _gameController.numVida;
       // int ultima_fase_concluida = ultima_fase;
       // DateTime data_hora = DateTime.Now.ToLocalTime();
       // int id_usuario_ativ_turma = _gameController.id_usuario_ativ_turma;
       // string seq_BlocosUtilizados = seqBlocos;

        Historico objHistorico = new Historico();

        objHistorico.Descricao = descricao;
        objHistorico.Moedas = moedas;
        objHistorico.Estrelas = estrelas;
        objHistorico.Vidas = vidas ;
        objHistorico.Ultima_fase_concluida = ultima_fase;
        objHistorico.Data_hora = DateTime.Now.ToLocalTime();
        objHistorico.Blocos_utilizados = seqBlocos;
        objHistorico.Id_usuario_ativ_turma = id_usuario_ativ_turma;

        
        _historico_Controller.ChamarRegistrarHistorico(objHistorico);
        _historico_Controller.ChamarReceberHistoricoId(38);
        
        seqBlocos = "";
    }

    public void PegarBlocosUtilizados(string p_SeqBlocos)
    {
        seqBlocos = p_SeqBlocos;
    }

    public void ResetarHistorico()
    {
        
    }
}
