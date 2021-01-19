using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class PainelFaseIncompleta : MonoBehaviour
{
    private         GameController      _gameController;
    private ControllerFase _controllerFase;
    private Pergunta_Controller _perguntaController;

    public          Button              btnClose;
    public GameObject btnJogarNovamente;
    public          GameObject          painelFaseIncompleta;

    public RectTransform rectPainelDerrotaFase;
    public RectTransform rectHud;

    [DllImport("__Internal")]
    public static extern void SistemaReiniciarWorkspaceBlockly();

    [DllImport("__Internal")]
    public static extern void ReiniciarVarCodeCompleto();

    [DllImport("__Internal")]
    public static extern void ReiniciarVarBlocosTotais();

    [DllImport("__Internal")]
    public static extern void CentralizarWebGl();

    [DllImport("__Internal")]
    public static extern void DisponibilizarToobox();

    [DllImport("__Internal")]
    private static extern void SistemaDeEnableDisableBlocos(bool situacao);

    [DllImport("__Internal")]
    public static extern void ResetarInterprete();

    void Start()
    {
        
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        _controllerFase = FindObjectOfType(typeof(ControllerFase)) as ControllerFase;
        _perguntaController = FindObjectOfType(typeof(Pergunta_Controller)) as Pergunta_Controller;
        rectPainelDerrotaFase.localPosition = new Vector4(rectHud.localPosition.x, rectHud.localPosition.y, 0, 0);
       
        btnClose.Select();
    }
    void Update()
    {
        
    }

    public void jogarNovamente(int idFase)
    {
        // SistemaReiniciarWorkspaceBlockly();
        ResetarInterprete();
        DisponibilizarToobox();
        ReiniciarVarCodeCompleto();
        ReiniciarVarBlocosTotais();
        SceneManager.LoadScene("Fase"+idFase);
        if(_gameController == null)
        {
            _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        }
        _gameController.idFaseEmExecucao = idFase;
        _gameController.descricaoFase = "Fase" + idFase;

        if(_controllerFase == null)
        {
            _controllerFase = FindObjectOfType(typeof(ControllerFase)) as ControllerFase;
        }
        
       
    }
    public void voltarSelecaoFase()
    {
        StartCoroutine(voltarSelecaoF());
    }

    public void ativarFaseIncompleta()
    {
       
        painelFaseIncompleta.SetActive(true);
       
    }

    IEnumerator voltarSelecaoF()
    {
        CentralizarWebGl();
        yield return new WaitForSeconds(1.7f);
        SistemaReiniciarWorkspaceBlockly();
        ReiniciarVarCodeCompleto();
        ReiniciarVarBlocosTotais();

      
        if(_perguntaController == null)
        {
           
            _perguntaController = FindObjectOfType(typeof(Pergunta_Controller)) as Pergunta_Controller;

        }
        if(_gameController == null)
        {
            _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        }
        _perguntaController.ChamarPegarPergunta(_gameController.idFaseEmExecucao, GetVerifPergunta);

            

    }

    public void GetVerifPergunta(Perguntas objPerguntas)
    {
        if (objPerguntas == null || objPerguntas.perguntas[0].descricao == "" || _gameController.perguntasRespondidas[_gameController.idFaseEmExecucao - 1])
        {
            SceneManager.LoadScene("SelecaoFase");
        }
        else if (!_gameController.perguntasRespondidas[_gameController.idFaseEmExecucao - 1])
        {
            SceneManager.LoadScene("Perguntas");
            _gameController.perguntasRespondidas[_gameController.idFaseEmExecucao - 1] = true;
        }
    }
}
