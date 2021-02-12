using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Runtime.InteropServices;

public class TelaInicial : MonoBehaviour
{
    private Pergunta_Controller _perguntaController;
    private GameController _gameController;
    private AudioController _audioController;

    public bool botaoIniciarClicado = false;
    public string haRegistroPlayerL = "";

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
          SistemaDeEnableDisableBlocos(true);//quando o jogo estiver na tela inicial os blocos estarão desabilitados e não mostrar a mensagem com o restante dos blocos

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
                    StartCoroutine("IniciarJogo");
                    botaoIniciarClicado = true;
                    HabilitarCliqueBtnIniciar();
                }
                
                break;
            
        }
        
    }

    //Script ControllerTelaInicial
    IEnumerator IniciarJogo()
    {
        if (haRegistroPlayerL == "naoHaRegistro")
        {
            _perguntaController.ChamarPegarUltimoId(PreencherIdUsuario);
        }
        _gameController.descricaoFase = "SelecaoFase";
        yield return new WaitForSeconds(0.42f);
        SceneManager.LoadScene("TelaCarregamento");
    }

    
    void PreencherIdUsuario(int id_usuario)
    {
        _gameController.id_usuario = id_usuario;
        GravarDadosPlayerLogado(id_usuario, _gameController.fasesConcluidas, _gameController.numGold, _gameController.numVida, _gameController.numEstrelas, _gameController.ultima_fase_concluida);

    }

   IEnumerator HabilitarCliqueBtnIniciar()
    {   
        
        yield return new WaitForSeconds(0.5f);
        botaoIniciarClicado = false;
    }
    //Script ControllerTelaInicial
    public void PreencherDadosPlayer(string dadosPlayer)
    {
        if(dadosPlayer != "")
        {
            string playerMW = dadosPlayer;
            DadosPlayer objDadosP = JsonUtility.FromJson<DadosPlayer>(playerMW);

            _gameController.id_usuario = objDadosP.Id_usuario;
            _gameController.fasesConcluidas = objDadosP.Fase_concluida;
            _gameController.numGold = objDadosP.Moedas;
            _gameController.numVida = objDadosP.Vidas;
            _gameController.numEstrelas = objDadosP.Estrelas;
            _gameController.ultima_fase_concluida = objDadosP.Ultima_fase_concluida;

            if(_gameController.fasesConcluidas != 0)
            {
                for(int i =0; i<_gameController.fasesConcluidas; i++)
                {
                
                    _gameController.perguntasRespondidas[i] = true;
                }
            }

        }
    }

    //Script ControllerTelaInicial
    public void VerificarPlayerL(string situacaoDadoP)
    {
        haRegistroPlayerL = situacaoDadoP;
    }
}
