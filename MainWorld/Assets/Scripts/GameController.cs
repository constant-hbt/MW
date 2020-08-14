using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;

public class GameController : MonoBehaviour
{

    /// <summary>
    /// ARMAZENA TODOS OS DADOS QUE DEVEM SER TRANSITADOS DENTRE AS FASES
    /// </summary>
    /// 

    [Header("Variaveis vinda da pagina anterior")]
    public int id_usuario_ativ_turma;
    public int idGame;
    public string descricaoFase;//ao clicar para abrir a fase essa variavel sera alimentada

    [Header("Configurações do GAME")]
    public GameObject[] gameControllers;
    private int numeroFasesGAME = 9; //contém o numero específico de fases que o jogo possui no momento

    [Header("Banco de Dados Seleção de Fase")]
    public int idFaseEmExecucao; //contem o id da fase que esta sendo executada no momento ---> ao clicar para abrir a fase essa variavel sera alimentada
    public int fasesConcluidas;// numero de fases concluidas 
    public int progressoAtual;
    public int numGold;//numero total de moedas coletadas dentro do jogo
    public int numVida = 10;//numero de vidas dentro do jogo
    public int numEstrelas;//numero total de estrelas coletadas dentro do jogo
    public int[] errosFase;//contém a quantidade de erros que o usuario teve ate a conclusao da fase --> será acrescentado um de erro a posicao da respectiva fase assim que o painel de fase incompleta for acionado --> assim que a fase em questao for concluida ao ativar o painel de conclusao de fase os erros respectivos aquela fase serao excluidos
    public int[] EstrelasFases; //contém as quantidade de estrelas adquiridas em cada fase-- Fase 1=posição 0, 2 = pos 1 ..


    [Header("Banco de Dados do Player")]
    public      int         vidaMax = 3;
    public int manaPlayer = 1;


    void Start()
    {
        gameControllers = GameObject.FindGameObjectsWithTag("GameController");
        if (gameControllers.Length >= 2)
        {//Ao mudar de cena caso tenha 2 scripts gameController ele deleta um e deixa somente o script vinculado a primeira fase que fica transitando entre as fases
            Destroy(gameControllers[1]);
        }
        DontDestroyOnLoad(this.gameObject);

        errosFase = new int[numeroFasesGAME];
        for (int i = 0; i < numeroFasesGAME; i++)//quando iniciar o game o objGameController vai iniciar todas as posicoes no array de acordo com o numero de fase existente no jog
        {                                    //Fase1 = 0 ..Fase2 = 1.. Fase3 =2
            errosFase[i] = 0;
        }

        id_usuario_ativ_turma = 1;//vai ser alimentado com dados vindo do js ---> MUDAR MAIS A FRENTE QUANDO FOR INTEGRAR
        idGame = 1;//vai ser alimentado com dados vindo do js -------> MUDAR MAIS A FRENTE QUANDO FOR INTEGRAR 
    }

    void Update()
    {

        
    }
    //SISTEMA DE ALTERNAR O FOCO NA PAGINA E NO WEBGL
    public void FocusCanvas(int focus)
    {
        if(focus == 0)
        {
            WebGLInput.captureAllKeyboardInput = false;//página estará com o foco
            
        }
        else if(focus == 1)
        {
            WebGLInput.captureAllKeyboardInput = true;//WebGL estará com o foco
            
        }
    }

    public void adicionarErro()//adiciona o 1 de erro ao usuario falhar na tentativa de concluir a fase
    {
        errosFase[idFaseEmExecucao - 1] += 1;
    }

    //Apos concluir a fase , a funcao zera a quantidade de erros obtidos antes da realizacao da fase que estao armazenados dentro do array errosFase
    public void zerarVarBanco()
    {
        errosFase[idFaseEmExecucao - 1] = 0;
        idFaseEmExecucao = 0;
        descricaoFase = "";
    }


}
