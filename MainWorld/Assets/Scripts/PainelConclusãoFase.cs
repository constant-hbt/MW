using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;
public class PainelConclusãoFase : MonoBehaviour
{
    private             GameController          _gameController;
    private             ControllerFase          _controllerFase;
    private Pergunta_Controller _perguntaController;
    private PlayerController _playerController;
    private Save_Controller _saveController;

    public              TextMeshProUGUI         tmpEstrelas;
    
    public              TextMeshProUGUI         tmpQtdMoedasColetadas;
    public              GameObject              legendaMoedasColetadas;
    public              TextMeshProUGUI         tmpQtdMoedasDisponiveis;
    public              GameObject              legendaMoedasDisponiveis;
    public              GameObject[]            SimbolosDesempenhoMoedas;

    public              TextMeshProUGUI         tmpQtdBlocosUtilizados;
    public              GameObject              legendaBlocosUtilizados;
    public              TextMeshProUGUI         tmpQtdBlocosDisponiveis;
    public              GameObject              legendaBlocosDisponiveis;
    public              GameObject[]            SimbolosDesempenhoBlocos;

    public GameObject painelConclusaFase;
//    private bool jaEnvieiRegistro = false; //verifica se o registro ja foi enviado ao ativar o painel ao concluir a fase

    [Header("Configuração Estrelas")]

    public              GameObject              estrela1;
    public GameObject x1;
    public              GameObject              estrela2;
    public GameObject x2;
    public              GameObject              estrela3;
    public GameObject x3;
    private             int                     qtdEstrelasAdquiridas;



    public Camera cam;
    public RectTransform pConclusaoFase;
    public RectTransform rectHud;
    public GameObject botaoProximaF;
    public GameObject botaoFaseAnterior;
    public GameObject tituloFaseC;
    public GameObject tituloFaseInc;

    [DllImport("__Internal")]
    public static extern void                   SistemaReiniciarWorkspaceBlockly();

    [DllImport("__Internal")]
    public static extern void                   ChamandoAlertFinalFase();

    [DllImport("__Internal")]
    public static extern void CentralizarWebGl();

    [DllImport("__Internal")]
    public static extern void ResetarInterprete();


    private             bool                    habilitarAlertCodigo = false;
    private bool habilitarContabilDesemp = false;

    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        _controllerFase = FindObjectOfType(typeof(ControllerFase)) as ControllerFase;
        _perguntaController = FindObjectOfType(typeof(Pergunta_Controller)) as Pergunta_Controller;
        _playerController = FindObjectOfType(typeof(PlayerController)) as PlayerController;
        _saveController = FindObjectOfType(typeof(Save_Controller)) as Save_Controller;

        qtdEstrelasAdquiridas = _controllerFase.distribuicaoEstrelas();
       // tmpEstrelas.text = qtdEstrelasAdquiridas.ToString();
        tmpQtdMoedasColetadas.text = _controllerFase.qtdMoedasColetadas.ToString();
        tmpQtdMoedasDisponiveis.text = _controllerFase.qtdMoedasDisponiveis.ToString();
        calcularDesempenhoColetaMoeda();

        tmpQtdBlocosUtilizados.text = _controllerFase.qtdBlocosUsados.ToString();
        tmpQtdBlocosDisponiveis.text = _controllerFase.qtdMinimaDeBlocosParaConclusao.ToString();
        calcularDesempenhoColetaBlocos();

        pConclusaoFase.localPosition = new Vector4(rectHud.localPosition.x, rectHud.localPosition.y, 0, 0);

        if (_playerController.passeiFase)
        {
            _gameController.ultima_fase_concluida = _gameController.idFaseEmExecucao;

            switch (qtdEstrelasAdquiridas)
            {
                case 1:
                    estrela1.SetActive(true);
                    estrela2.SetActive(false);
                    estrela3.SetActive(false);
                    break;
                case 2:
                    estrela1.SetActive(true);
                    estrela2.SetActive(true);
                    estrela3.SetActive(false);
                    break;
                case 3:
                    estrela1.SetActive(true);
                    estrela2.SetActive(true);
                    estrela3.SetActive(true);
                    break;
            }

            tituloFaseC.SetActive(true);
            tituloFaseInc.SetActive(false);

         /*   if (_gameController.idFaseEmExecucao < 9)
            {
                botaoProximaF.SetActive(true);
            }
            if(_gameController.idFaseEmExecucao > 1)
            {
                botaoFaseAnterior.SetActive(true);
            }
           */ 
        }
        else
        {
            estrela1.SetActive(false);
            estrela2.SetActive(false);
            estrela3.SetActive(false);

            x1.SetActive(true);
            x2.SetActive(true);
            x3.SetActive(true);

            tituloFaseC.SetActive(false);
            tituloFaseInc.SetActive(true);
          //  botaoProximaF.SetActive(false);
          //  botaoFaseAnterior.SetActive(false);
        }
        

        habilitarAlertCodigo = true;
       // jaEnvieiRegistro = false;

     //   _controllerFase.EnviarHistorico("Fase" + _gameController.idFaseEmExecucao + "-Parte" + (_gameController.parteFaseAtual + 1), _controllerFase.qtdMoedasColetadasCadaParte, _controllerFase.estrelas,
     //                                        _gameController.numVida, _gameController.ultima_fase_concluida, _gameController.id_usuario, _gameController.id_atividade);
        
    }
    private void FixedUpdate()
    {
        if (habilitarAlertCodigo)
        {
          //  ChamandoAlertFinalFase();
            habilitarAlertCodigo = false;
        }
    }

    public void BtnReiniciar(int numeroFase)
    {
       // ResetarInterprete();
        _gameController.ZerarVarBancoTentativasFase(); //zera as variaveis para reiniciar a fase do inicio
        _gameController.tentativaFaseAlter = false; // reinicio a variavel para permitir iniciar correntamente a variavel de tentativas disponibilizadas dentro de cada fase
        _gameController.numTentativasFixo = 0;
        _gameController.numTentativasFase = 0;
        SceneManager.LoadScene("Fase" + numeroFase);
        _gameController.idFaseEmExecucao = numeroFase;
        _gameController.descricaoFase = "Fase" + numeroFase;

     }

    public void btnPlay()
    {
        if(_gameController.idFaseEmExecucao == 9 && _gameController.parteFaseAtual == 2)
        {
            StartCoroutine(voltarSelecaoFase("TelaGameWin"));
            
        }
        else
        {
            if (!_gameController.perguntasRespondidas[_gameController.idFaseEmExecucao - 1])
            {
                StartCoroutine(voltarSelecaoFase("IrAoPainelPergunta"));

            }
            else
            {
               
                StartCoroutine(voltarSelecaoFase("voltarSelecaoFase"));
            }
        }


       
    }
    public void btnProximaF()
    {
            SceneManager.LoadScene("Fase" + (_gameController.idFaseEmExecucao + 1));
        
    }
    public void btnFaseAnterior()
    {
        SceneManager.LoadScene("Fase" + (_gameController.idFaseEmExecucao -1));
    }

   public void contabilizarDesempenho(int idFase)
    {
        if (_playerController.passeiFase)
        {
            if (!habilitarContabilDesemp)
            {
                if (_gameController.fasesConcluidas < idFase)
                {//caso o numero de fases concluidas for menor que o id da Fase quer dizer que o jogador ainda nao havia concluido aquela fase
                 //portanto a variavel recebe o idFase , habilitando o mapa para a proxima fase, e deixando a fase correpondente
                 //ao idFase como concluida
                    _gameController.fasesConcluidas = idFase;

                }
                //if (_gameController.EstrelasFases[idFase - 1] <= 3 && _gameController.EstrelasFases[idFase - 1] < qtdEstrelasAdquiridas)
                //{
                _gameController.numEstrelas += qtdEstrelasAdquiridas;//soma somente a diferenca entre as estrelas que ja havia adquirido nesta fase , com as que adquiri a mais em uma nova tentativa
                _gameController.EstrelasFases[idFase - 1] += qtdEstrelasAdquiridas;
                _gameController.numGold += _controllerFase.qtdMoedasColetadas;

            }
            habilitarContabilDesemp = true;
            // }

            //Atualiza o save do usuário
            _saveController.ChamarAtualizarSave(_gameController.id_save_game, _gameController.numGold, _gameController.numEstrelas, _gameController.numVida, _gameController.fasesConcluidas
                                                , _gameController.idGame, _gameController.id_usuario);
        }

    }

    public void calcularDesempenhoColetaMoeda()
    {
        int qtdMoedasColetadas = _controllerFase.qtdMoedasColetadas;
        int qtdMoedasDisponiveis = _controllerFase.qtdMoedasDisponiveis;

        if (_playerController.passeiFase)
        {
            if (qtdMoedasColetadas == qtdMoedasDisponiveis)
            {
                SimbolosDesempenhoMoedas[0].SetActive(true);
                SimbolosDesempenhoMoedas[1].SetActive(false);
                SimbolosDesempenhoMoedas[2].SetActive(false);
            }
            else if (qtdMoedasColetadas > 0 && qtdMoedasColetadas < qtdMoedasDisponiveis)
            {
                SimbolosDesempenhoMoedas[0].SetActive(false);
                SimbolosDesempenhoMoedas[1].SetActive(false);
                SimbolosDesempenhoMoedas[2].SetActive(true);
            }
            else
            {
                SimbolosDesempenhoMoedas[0].SetActive(false);
                SimbolosDesempenhoMoedas[1].SetActive(true);
                SimbolosDesempenhoMoedas[2].SetActive(false);
            }
        }
        else
        {
            SimbolosDesempenhoMoedas[0].SetActive(false);
            SimbolosDesempenhoMoedas[1].SetActive(true);
            SimbolosDesempenhoMoedas[2].SetActive(false);
        }
        
    }

    public void calcularDesempenhoColetaBlocos()
    {
        int qtdBlocosUtilizados = _controllerFase.qtdBlocosUsados;
        int qtdBlocosMinimosConclusao = _controllerFase.qtdMinimaDeBlocosParaConclusao;

        int qtdMoedasColetadas = _controllerFase.qtdMoedasColetadas;
        int qtdMoedasDisponiveis = _controllerFase.qtdMoedasDisponiveis;

        if (_playerController.passeiFase)
        {
            if (qtdBlocosUtilizados <= qtdBlocosMinimosConclusao)
            {
                SimbolosDesempenhoBlocos[0].SetActive(true);
                SimbolosDesempenhoBlocos[1].SetActive(false);
                SimbolosDesempenhoBlocos[2].SetActive(false);
            }
            else if (qtdBlocosUtilizados > qtdBlocosMinimosConclusao || qtdBlocosUtilizados < qtdBlocosMinimosConclusao && qtdMoedasColetadas < qtdMoedasDisponiveis)
            {
                SimbolosDesempenhoBlocos[0].SetActive(false);
                SimbolosDesempenhoBlocos[1].SetActive(false);
                SimbolosDesempenhoBlocos[2].SetActive(true);
            }
            else
            {
                SimbolosDesempenhoBlocos[0].SetActive(false);
                SimbolosDesempenhoBlocos[1].SetActive(true);
                SimbolosDesempenhoBlocos[2].SetActive(false);
            }
        }
        else
        {
            SimbolosDesempenhoBlocos[0].SetActive(false);
            SimbolosDesempenhoBlocos[1].SetActive(true);
            SimbolosDesempenhoBlocos[2].SetActive(false);
        }
        

    }

    IEnumerator voltarSelecaoFase(string acao)
    {
        switch (acao)
        {
            case "voltarSelecaoFase":
            //   CentralizarWebGl();
                yield return new WaitForSeconds(1.7f);
                _gameController.descricaoFase = "SelecaoFase";
                SceneManager.LoadScene("TelaCarregamento");
                break;
            case "IrAoPainelPergunta":
            //    CentralizarWebGl();
                yield return new WaitForSeconds(1.7f);
                _gameController.descricaoFase = "Perguntas";
                _perguntaController.ChamarPegarPergunta(_gameController.idFaseEmExecucao, GetVerifPergunta);
               break;
            case "TelaGameWin":
            //    CentralizarWebGl();
                yield return new WaitForSeconds(1.7f);
                _gameController.descricaoFase = "TelaGameWin";
                SceneManager.LoadScene("TelaCarregamento");
                break;
        }

       
    }

    public void GetVerifPergunta(Perguntas objPerguntas)
    {
        if (objPerguntas == null)
        {
            SceneManager.LoadScene("SelecaoFase");
        }
        else if (!_gameController.perguntasRespondidas[_gameController.idFaseEmExecucao - 1])
        {
            SceneManager.LoadScene("Perguntas");
            _gameController.perguntasRespondidas[_gameController.idFaseEmExecucao-1] = true;
        }
    }

    public void AtivarLegenda(string nomeCampo)
    {
        switch (nomeCampo)
        {
            case "moedasColetadas":
                legendaMoedasColetadas.SetActive(true);
                break;
            case "moedasDisponiveis":
                legendaMoedasDisponiveis.SetActive(true);
                break;
            case "blocosUtilizados":
                legendaBlocosUtilizados.SetActive(true);
                break;
            case "blocosDisponiveis":
                legendaBlocosDisponiveis.SetActive(true);
                break;
        }
    }

    public void DesativarLegenda(string nomeCampo)
    {
        switch (nomeCampo)
        {
            case "moedasColetadas":
                legendaMoedasColetadas.SetActive(false);
                break;
            case "moedasDisponiveis":
                legendaMoedasDisponiveis.SetActive(false);
                break;
            case "blocosUtilizados":
                legendaBlocosUtilizados.SetActive(false);
                break;
            case "blocosDisponiveis":
                legendaBlocosDisponiveis.SetActive(false);
                break;
        }
    }
}
