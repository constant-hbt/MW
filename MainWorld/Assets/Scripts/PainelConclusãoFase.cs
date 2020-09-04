﻿using System.Collections;
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

    public              TextMeshProUGUI         tmpEstrelas;
    public              TextMeshProUGUI         tmpMoedas;
    public GameObject painelConclusaFase;
    private bool jaEnvieiRegistro = false; //verifica se o registro ja foi enviado ao ativar o painel ao concluir a fase

    [Header("Configuração Estrelas")]

    public              GameObject              estrela1;
    public              GameObject              estrela2;
    public              GameObject              estrela3;
    private             int                     qtdEstrelasAdquiridas;

    [DllImport("__Internal")]
    public static extern void                   SistemaReiniciarWorkspaceBlockly();

    [DllImport("__Internal")]
    public static extern void                   ChamandoAlertFinalFase();

    private             bool                    habilitarAlertCodigo = false;

    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        _controllerFase = FindObjectOfType(typeof(ControllerFase)) as ControllerFase;
        _desempenhoController = FindObjectOfType(typeof(Desempenho_Controller)) as Desempenho_Controller;

        qtdEstrelasAdquiridas = _controllerFase.distribuicaoEstrelas();
        Debug.Log("Conclui a fase e consegui "+qtdEstrelasAdquiridas+" estrelas");
        tmpEstrelas.text = qtdEstrelasAdquiridas.ToString();
        tmpMoedas.text = _controllerFase.qtdMoedasColetadas.ToString();

        
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
    }
    void Update()
    {
        if (habilitarAlertCodigo)
        {
            ChamandoAlertFinalFase();
            habilitarAlertCodigo = false;
        }
    }
    private void FixedUpdate()
    {
        if (painelConclusaFase.activeSelf && !jaEnvieiRegistro)
        {
            Debug.Log("Vou enviar os registros ao banco");

            Debug.Log("Estou enviando os dados para o banco");
            _desempenhoController.EnviarRegistroDesempenho();

            jaEnvieiRegistro = true;
        }
    }

    public void BtnReiniciar(int numeroFase)
    {
        SceneManager.LoadScene("Fase" + numeroFase);
        _gameController.idFaseEmExecucao = numeroFase;
        _gameController.descricaoFase = "Fase" + numeroFase;
    }

    public void btnPlay()
    {
        SceneManager.LoadScene("SelecaoFase");
    }

   public void contabilizarDesempenho(int idFase)
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

        // }
    }

}
