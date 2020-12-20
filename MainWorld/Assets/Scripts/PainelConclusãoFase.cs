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
    private Desempenho_Controller _desempenhoController;
    private Teleporte _teleporte;
    private Pergunta_Controller _perguntaController;

    public              TextMeshProUGUI         tmpEstrelas;
    public              TextMeshProUGUI         tmpMoedas;
    public GameObject painelConclusaFase;
    private bool jaEnvieiRegistro = false; //verifica se o registro ja foi enviado ao ativar o painel ao concluir a fase

    [Header("Configuração Estrelas")]

    public              GameObject              estrela1;
    public              GameObject              estrela2;
    public              GameObject              estrela3;
    private             int                     qtdEstrelasAdquiridas;

    public Camera cam;
    public RectTransform pConclusaoFase;
    public RectTransform rectHud;

    [DllImport("__Internal")]
    public static extern void                   SistemaReiniciarWorkspaceBlockly();

    [DllImport("__Internal")]
    public static extern void                   ChamandoAlertFinalFase();

    [DllImport("__Internal")]
    public static extern void CentralizarWebGl();

    private             bool                    habilitarAlertCodigo = false;
    private bool habilitarContabilDesemp = false;

    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        _controllerFase = FindObjectOfType(typeof(ControllerFase)) as ControllerFase;
        _desempenhoController = FindObjectOfType(typeof(Desempenho_Controller)) as Desempenho_Controller;
        _teleporte = FindObjectOfType(typeof(Teleporte)) as Teleporte;
        _perguntaController = FindObjectOfType(typeof(Pergunta_Controller)) as Pergunta_Controller;


        qtdEstrelasAdquiridas = _controllerFase.distribuicaoEstrelas();
        Debug.Log("Conclui a fase e consegui "+qtdEstrelasAdquiridas+" estrelas");
        tmpEstrelas.text = qtdEstrelasAdquiridas.ToString();
        tmpMoedas.text = _controllerFase.qtdMoedasColetadas.ToString();
        _gameController.ultima_fase_concluida = _gameController.idFaseEmExecucao;
        
        pConclusaoFase.localPosition = new Vector4(rectHud.localPosition.x, rectHud.localPosition.y,0 ,0) ;
        

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

        habilitarAlertCodigo = true;
        jaEnvieiRegistro = false;

        Debug.Log("Passei a fase e estou salvando o historico");
        _controllerFase.EnviarHistorico("Fase" + _gameController.idFaseEmExecucao + "-Parte" + (_gameController.parteFaseAtual + 1), _controllerFase.qtdMoedasColetadasCadaParte, _controllerFase.estrelas,
                                             _gameController.numVida, _gameController.ultima_fase_concluida, _gameController.id_usuario, _gameController.id_atividade);
    }
    void Update()
    {
        if (habilitarAlertCodigo)
        {
         //   ChamandoAlertFinalFase();
            habilitarAlertCodigo = false;
        }
    }
    private void FixedUpdate()
    {
       /* if (painelConclusaFase.activeSelf && !jaEnvieiRegistro)
        {
            Debug.Log("Vou enviar os registros ao banco");

            Debug.Log("Estou enviando os dados para o banco");
            _desempenhoController.EnviarRegistroDesempenho();

            jaEnvieiRegistro = true;
        }*/
    }

    public void BtnReiniciar(int numeroFase)
    {
        SceneManager.LoadScene("Fase" + numeroFase);
        _gameController.idFaseEmExecucao = numeroFase;
        _gameController.descricaoFase = "Fase" + numeroFase;
    }

    public void btnPlay()
    {
        

        if(!_gameController.perguntasRespondidas[_gameController.idFaseEmExecucao - 1])
        {
            StartCoroutine(voltarSelecaoFase("IrAoPainelPergunta"));
            
        }
        else
        {
            StartCoroutine(voltarSelecaoFase("voltarSelecaoFase"));
        }

    }

   public void contabilizarDesempenho(int idFase)
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
    }

    IEnumerator voltarSelecaoFase(string acao)
    {
        switch (acao)
        {
            case "voltarSelecaoFase":
             //   CentralizarWebGl();
                yield return new WaitForSeconds(1.7f);
                SceneManager.LoadScene("SelecaoFase");
                break;
            case "IrAoPainelPergunta":
              //  CentralizarWebGl();
                yield return new WaitForSeconds(1.7f);
                _perguntaController.ChamarPegarPergunta(_gameController.idFaseEmExecucao, GetVerifPergunta);
               break;
        }

       
    }

    public void GetVerifPergunta(Perguntas objPerguntas)
    {
        if (objPerguntas == null)
        {
            SceneManager.LoadScene("SelecaoFase");
            Debug.Log("Id fase em execucao = " + _gameController.idFaseEmExecucao);
        }
        else if (!_gameController.perguntasRespondidas[_gameController.idFaseEmExecucao - 1])
        {
            SceneManager.LoadScene("Perguntas");
            _gameController.perguntasRespondidas[_gameController.idFaseEmExecucao-1] = true;
        }
    }


}
