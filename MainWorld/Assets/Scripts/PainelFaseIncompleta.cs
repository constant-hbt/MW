using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class PainelFaseIncompleta : MonoBehaviour
{
    private         GameController      _gameController;

    public          Button              btnClose;
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
    void Start()
    {
       // SistemaDeEnableDisableBlocos(true);
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        rectPainelDerrotaFase.localPosition = new Vector4(rectHud.localPosition.x, rectHud.localPosition.y, 0, 0);
       
        btnClose.Select();
        

    }
    void Update()
    {
        
    }

    public void jogarNovamente(int idFase)
    {
       // SistemaReiniciarWorkspaceBlockly();
       // DisponibilizarToobox();
       // ReiniciarVarCodeCompleto();
       // ReiniciarVarBlocosTotais();
        SceneManager.LoadScene("Fase"+idFase);
        if(_gameController == null)
        {
            _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        }
        _gameController.idFaseEmExecucao = idFase;
        _gameController.descricaoFase = "Fase" + idFase;
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
       // CentralizarWebGl();
        yield return new WaitForSeconds(1.7f);
       // SistemaReiniciarWorkspaceBlockly();
       // ReiniciarVarCodeCompleto();
       // ReiniciarVarBlocosTotais();
        SceneManager.LoadScene("SelecaoFase");

    }

    
}
