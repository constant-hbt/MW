using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Runtime.InteropServices;
using UnityEngine.Networking;
using System;
public class TelaInicial : MonoBehaviour
{
    private Pergunta_Controller _perguntaController;
    private GameController _gameController;
    private AudioController _audioController;

    public bool botaoIniciarClicado;
    public string haRegistroPlayerL = "";

    [Header("Botões")]
    public GameObject btnPlay;
    public GameObject btnNovoJogo;
    public GameObject btnCarregarJogo;

    [Header("Paineis")]
    public GameObject painelCarregar;

    //Integração com js da página
    [DllImport("__Internal")]
    private static extern void SistemaDeEnableDisableBlocos(bool situacao);

    [DllImport("__Internal")]
    public static extern void GravarDadosPlayerLogado(int p_id_usuario, int p_fase_concluida, int p_moedas, int p_vidas, int p_estrelas, int p_ultima_fase_concluida);

    private void Awake()
    {

        _perguntaController = FindObjectOfType(typeof(Pergunta_Controller)) as Pergunta_Controller;
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        _audioController = FindObjectOfType(typeof(AudioController)) as AudioController;

        _audioController.trocarMusica(_audioController.musicaTitulo, "TelaInicio", false);
        _gameController.VerificarQtdObjGameC();
        _audioController.VerificarQtdObjAudioC();
      //    SistemaDeEnableDisableBlocos(true);//quando o jogo estiver na tela inicial os blocos estarão desabilitados e não mostrar a mensagem com o restante dos blocos

        botaoIniciarClicado = false;
    }
    // Update is called once per frame
    void Update()
    {
    }

    public void ControladorDeCoroutine(int id)
    {
        switch (id)
        {
            case 1:
                if (!botaoIniciarClicado)
                {
                    Debug.Log("Entrei");
                    StartCoroutine("IniciarJogo");
                    botaoIniciarClicado = true;
                    HabilitarCliqueBtnIniciar();
                }
                break;
            case 2:
                StartCoroutine(AtivarBotoesNewLoadGame());
                break;
        }
        
    }

    //Script ControllerTelaInicial
    IEnumerator IniciarJogo()
    {
        _gameController.descricaoFase = "SelecaoFase";
        yield return new WaitForSeconds(0.42f);
        SceneManager.LoadScene("TelaCarregamento");
    }

   IEnumerator HabilitarCliqueBtnIniciar()
    {   
        
        yield return new WaitForSeconds(0.5f);
        botaoIniciarClicado = false;
    }
   
    IEnumerator AtivarBotoesNewLoadGame()//habilita os botões de novo jogo e carregar jogo
    {
        yield return new WaitForSeconds(0.25f);
        btnPlay.SetActive(false);
        btnNovoJogo.SetActive(true);
        btnCarregarJogo.SetActive(true);
    }

    public void BtnCarregarJogo()
    {
        if (painelCarregar.activeSelf)
        {
            painelCarregar.SetActive(false);
        }
        else
        {
            painelCarregar.SetActive(true);
        }
    }
}
