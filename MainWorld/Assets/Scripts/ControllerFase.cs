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
    public int estrelas = 100;//estrelas adquiras com o desempenho na fase

    public GameObject[] fases;

   
    [DllImport("__Internal")]
    private static extern void SistemaLimiteBloco(int qtdBlocoFase);

    //Configuração do Limite de blocos por fase
    //Ao iniciar a fase a funcao SistemaLimiteBloco muda o campo no html da pagina que delimita a quantidade de bloco
    //maximos que pode ser utilizado durante aquela fase
    [Header("Distribuição de Estrelas")]
    public int qtdBlocosDisponiveis;//quantidade de blocos disponiveis para poder concluir a fase
    public int qtdMinimaDeBlocosParaConclusao;//quantidade de blocos minimos que devem ser usados para concluir a fase
    public int qtdBlocosUsados;//quantidade de blocos que foram utilizados para concluir a fase
    public int qtdMoedasDisponiveis;//quantidade de moedas disponiveis para coleta na fase
    public int qtdMoedasColetadas;//quantidade de moedas coletadas durante a fase
    
    void Start()
    {
        SistemaLimiteBloco(qtdBlocosDisponiveis);

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
        float porcBlocosMinimos = 0;//contem a porcentagem de blocos minimos que podem ser usados para passar de fase
        float porcBlocosUsados = 0;//contem a porcentagem de blocos que foram usados para passar a fase
        float porcMoedasColetadas = 0;//contem a porcentagem de estrelas que foi coletada durante a fase

        //calculos
        porcBlocosMinimos = (qtdMinimaDeBlocosParaConclusao * 100) / qtdBlocosDisponiveis;
        porcBlocosUsados = (qtdBlocosUsados * 100) / qtdBlocosDisponiveis;
        porcMoedasColetadas = (qtdMoedasColetadas * 100) / qtdMoedasDisponiveis;


        if(porcBlocosUsados <= porcBlocosMinimos && porcMoedasColetadas == 100.0f)
        {
            estrelas = 3;
            //ganha 3 estrelas
            
        }else if(porcBlocosUsados > porcBlocosMinimos && porcMoedasColetadas > 0f  ||
                 porcBlocosUsados <= porcBlocosMinimos && porcMoedasColetadas > 0f )
        {
            estrelas = 2;
            //ganha 2 estrelas
            
        }
        else if(porcBlocosUsados > porcBlocosMinimos  && porcMoedasColetadas == 0f ||
                porcBlocosUsados <= porcBlocosMinimos && porcMoedasColetadas == 0f)
        {
            estrelas = 1;
            //ganha 1 estrela
            
        }
        return estrelas;
    }
}
