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

    [Header("Coletáveis durante a fase")]
    public int qtdMoedasColetadas;//quantidade de moedas coletadas durante a fase
    public int qtdMoedasLootColetadas; //moedas coletadas por loot adquiridos apartir dos inimigos
    public int estrelas = 0;//estrelas adquiras com o desempenho na fase
    public GameObject[] fases;
    public string data_InicioFase;//computado ao iniciar a fase
    public string data_FimFase;//vai ser computado ao colidir com o runaWin

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
        

    //Integração com o js da página
    [DllImport("__Internal")]
    public static extern void                    SistemaLimiteBloco(int qtdBlocoFase);
    [DllImport("__Internal")]
    private static extern void                      SistemaDeEnableDisableBlocos(bool situacao);

    [DllImport("__Internal")]
    public static extern void                       EnviarQTDBlocosMinimosParaPassarFase(int qtdBlocosMinimos);//envia a quantidade de blocos minimos necessarios para passar a fase

    [DllImport("__Internal")]
    public static extern void AlterarLimiteBlocoForcaAtaque(int limitForcaAtaque);

    

    void Start()
    {
        data_InicioFase = DateTime.Now.ToLocalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");//Pega a data/hora que a fase é iniciada

       SistemaDeEnableDisableBlocos(false);//quando o jogo estiver na tela inicial os blocos estarão desabilitados e não mostrar a mensagem com o restante dos blocos
       SistemaLimiteBloco(qtdBlocosDisponiveis);
       EnviarQTDBlocosMinimosParaPassarFase(qtdMinimaDeBlocosParaConclusao);


        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        _playerController = FindObjectOfType(typeof(PlayerController)) as PlayerController;
        _hud = FindObjectOfType(typeof(HUD)) as HUD;

        if (fases.Length != 0)
        {
            foreach (GameObject o in fases)
            {
                o.SetActive(false);
            }//desabilita todas as partes da fase , e em seguida habilita somente a primeira parte
            fases[0].SetActive(true);
        }

        if(_gameController.manaPlayer < qtdManaDisponivelFase)//a cada fase que passar a mana vai aumentar proporcionalmente, caso ja tenha passado a fase e volte a joga-la a mana vai estar no valor do adquirido na ultima fase que habilitou
        {
            _gameController.manaPlayer = qtdManaDisponivelFase;//alimento gameController com a quantidade de mana que o player tem no inicio daquela fase que ainda não jogou
        }
        
        //altero o limite do poder de ataque de acordo com a quantidade de mana que o playerKnight tem
        AlterarLimiteBlocoForcaAtaque(_gameController.manaPlayer);
    }

   
    void Update()
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

        if (qtdBlocosUsados <= qtdMinimaDeBlocosParaConclusao && moedasColetadas == qtdMoedasDisponiveis ||
            qtdBlocosUsados <= qtdMinimaDeBlocosParaConclusao && naoTemMoeda)
        {//se eu utilizar o minimo de blocos ou menos e coletar todas as moedas da fase eu ganho 3 estrelas
            estrelas = 3;
        }else if(qtdBlocosUsados <= qtdMinimaDeBlocosParaConclusao && moedasColetadas >= metadeMoedaD && moedasColetadas < qtdMoedasDisponiveis && !naoTemMoeda ||
                 qtdBlocosUsados >= qtdMinimaDeBlocosParaConclusao && moedasColetadas >= metadeMoedaD && moedasColetadas <= qtdMoedasDisponiveis && !naoTemMoeda)
        {//se eu usar o minimo ou mais de blocos e coletar mais doque 50% das moedas ganho 2 estrelas
            estrelas = 2;
        }
        else if(qtdBlocosUsados >= qtdMinimaDeBlocosParaConclusao && moedasColetadas < metadeMoedaD && !naoTemMoeda ||
                qtdBlocosUsados >= qtdMinimaDeBlocosParaConclusao && moedasColetadas == 0 && naoTemMoeda ||
                qtdBlocosUsados <= qtdMinimaDeBlocosParaConclusao && moedasColetadas < metadeMoedaD && !naoTemMoeda  /*Se nao tiver nenhuma moeda para ser coletada e mesmo assim ele utilizar blocos a mais que o minimo*/)
        {//se eu usar o minimo ou mais de blocos e coletar menos doque 50% das moedas ganho 1 estrelas
            estrelas = 1;
        }
        else
        {
            Debug.Log("Erro aqui no distribuicao Estrelas"); //SE ENTRAR AQUI É QUE TEM ALGO ERRADO NA FUNCAO
            estrelas = 100;
        }
        
        return estrelas;
    }

    public void alteracaoDisponibilidadeManaAdicao( int valorMana)//altera dinamicamente a mana a partir da utilizacao pelo player atraves do bloco valor_ataque
    {
       
        Debug.Log("Entrei dentro do adicaoMana");
        int manaAtual =qtdManaDisponivelFase;
        int novoValMana = manaAtual + valorMana;

        qtdManaDisponivelFase = novoValMana;
        _hud.manaText.text = novoValMana.ToString();
            AlterarLimiteBlocoForcaAtaque(novoValMana);

        
    }
    public void alteracaoDisponibilidadeManaRemocao(string valorAntigoNovoMana)

    {
        string []novoVal = valorAntigoNovoMana.Split(',');

        Debug.Log("Entrei dentro do retiradaMana");
        Debug.Log("novoVal pos 0 = " + novoVal[0] + " novoVal pos1 = " + novoVal[1]);
        int valorAntesAtt = Int32.Parse(novoVal[0]);
        int valorDepoisAtt = Int32.Parse(novoVal[1]);

        int manaAtual = qtdManaDisponivelFase + valorAntesAtt;
        int novoValMana = manaAtual - valorDepoisAtt;

        qtdManaDisponivelFase = novoValMana;
        _hud.manaText.text = novoValMana.ToString();
        AlterarLimiteBlocoForcaAtaque(novoValMana);
        Debug.Log("Dentro do alteracaoDisp , valor do novoValMan = " + novoValMana);
    }

}
