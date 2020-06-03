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
    public int estrelas = 0;//estrelas adquiras com o desempenho na fase

    public GameObject[] fases;

   
    

    //Configuração do Limite de blocos por fase
    //Ao iniciar a fase a funcao SistemaLimiteBloco muda o campo no html da pagina que delimita a quantidade de bloco
    //maximos que pode ser utilizado durante aquela fase
    [Header("Distribuição de Estrelas")]
    public int qtdBlocosDisponiveisEmTodaFase;//quantidade de blocos disponiveis para poder concluir a fase
    public int qtdMinimaDeBlocosParaConclusao;//quantidade de blocos minimos que devem ser usados para concluir a fase
    public int qtdBlocosUsados;//quantidade de blocos que foram utilizados para concluir a fase
    public int qtdMoedasDisponiveis;//quantidade de moedas disponiveis para coleta na fase
    public int qtdMoedasColetadas;//quantidade de moedas coletadas durante a fase


    [Header("Quantidade disponivel para a primeira parte da fase")]
    public int qtdBlocosDisponiveis;//para as fases que tem mais de uma parte o valor depositado aqui valerá para a primeira parte, nas partes subsequentes o valor deverá ser colocado no script que esta contido nos objetos de teleporte
    //Integração com o js da página
    [DllImport("__Internal")]
    public static extern void SistemaLimiteBloco(int qtdBlocoFase);
    [DllImport("__Internal")]
    private static extern void SistemaDeEnableDisableBlocos(bool situacao);

    [DllImport("__Internal")]
    public static extern void EnviarQTDBlocosMinimosParaPassarFase(int qtdBlocosMinimos);//envia a quantidade de blocos minimos necessarios para passar a fase
    void Start()
    {
        SistemaDeEnableDisableBlocos(false);//quando o jogo estiver na tela inicial os blocos estarão desabilitados e não mostrar a mensagem com o restante dos blocos
        SistemaLimiteBloco(qtdBlocosDisponiveis);
        EnviarQTDBlocosMinimosParaPassarFase(qtdMinimaDeBlocosParaConclusao);


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
    public void quantidadeBlocoUsadosNaFase(int qtdBlocos)//passo por parametro os blocos usados de uma variavel que esta na pagina web
    {
        qtdBlocosUsados += qtdBlocos;
    }
    public int distribuicaoEstrelas()
    {
        bool naoTemMoeda = false;
       /* float porcBlocosMinimos = 0;//contem a porcentagem de blocos minimos que podem ser usados para passar de fase
        float porcBlocosUsados = 0;//contem a porcentagem de blocos que foram usados para passar a fase
        //float porcMoedasColetadas = 0;//contem a porcentagem de estrelas que foi coletada durante a fase -- APAGAR DEPOIS

        //calculos
        porcBlocosMinimos = (qtdMinimaDeBlocosParaConclusao * 100) / qtdBlocosDisponiveis;
        porcBlocosUsados = (qtdBlocosUsados * 100) / qtdBlocosDisponiveis;
        //porcMoedasColetadas = (qtdMoedasColetadas * 100) / qtdMoedasDisponiveis; -- APAGAR DEPOIS
        //porcMoedasColetadas == 100.0f -- APAGAR DEPOIS
           
                                APAGAR DEPOIS SE AS MODIFICACOES DERAM CERTO

        //TEM ERRO AQUI
        */
        if(qtdMoedasDisponiveis == 0)
        {
            naoTemMoeda = true;
        }
        else
        {
            naoTemMoeda = false;
        }

        if (qtdBlocosUsados <= qtdBlocosDisponiveis &&qtdMoedasColetadas == qtdMoedasDisponiveis )
        {//se eu utilizar o minimo de blocos ou menos e coletar todas as moedas da fase eu ganho 3 estrelas
            estrelas = 3;
        }else if(qtdBlocosUsados > qtdBlocosDisponiveis && !naoTemMoeda && qtdMoedasColetadas >= (qtdMoedasDisponiveis / 2) && qtdMoedasColetadas < qtdMoedasDisponiveis ||
                 qtdBlocosUsados <= qtdBlocosDisponiveis && !naoTemMoeda && qtdMoedasColetadas >= (qtdMoedasDisponiveis / 2) && qtdMoedasColetadas < qtdMoedasDisponiveis )
        {//se eu usar o minimo ou mais de blocos e coletar mais doque 50% das moedas ganho 2 estrelas
            estrelas = 2;
        }
        else if(qtdBlocosUsados > qtdBlocosDisponiveis && !naoTemMoeda && qtdMoedasColetadas < (qtdMoedasDisponiveis / 2) ||/*Ex:Não tem nenhuma moeda na fase e mesmo assim ele usa uma quantidade de blocos maior que o minimo*/
                qtdBlocosUsados <= qtdBlocosDisponiveis && !naoTemMoeda && qtdMoedasColetadas < (qtdMoedasDisponiveis / 2) ||
                qtdBlocosUsados > qtdBlocosDisponiveis && naoTemMoeda/*Se nao tiver nenhuma moeda para ser coletada e mesmo assim ele utilizar blocos a mais que o minimo*/)
        {//se eu usar o minimo ou mais de blocos e coletar menos doque 50% das moedas ganho 1 estrelas
            estrelas = 1;
        }
        return estrelas;
    }

}
