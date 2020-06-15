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

    [DllImport("__Internal")]
    public static extern void SistemaReiniciarWorkspaceBlockly();

    [DllImport("__Internal")]
    public static extern void ReiniciarVarCodeCompleto();
    void Start()
    {
        
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;

       
        btnClose.Select();
    }
    void Update()
    {
        
    }

    public void jogarNovamente(int idFase)
    {
        ReiniciarVarCodeCompleto();
        SceneManager.LoadScene("Fase"+idFase);
    }
    public void voltarSelecaoFase()
    {

        SistemaReiniciarWorkspaceBlockly();
        SceneManager.LoadScene("SelecaoFase");
    }

    public void ativarFaseIncompleta()
    {
        painelFaseIncompleta.SetActive(true);
    }

    
}
