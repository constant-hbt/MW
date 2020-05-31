using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class RunaWin : MonoBehaviour
{

    /// <summary>
    /// RESPONSAVEL PELO OBJETO DE CONCLUSAO DA FASE, HABILITA E DESABILITA O PAINEL DE CONCLUSAO DE FASE APÓS A COLISÃO DO PLAYER COM O OBJETO QUE CONTÉM ESTE SCRIPT
    /// </summary>
    public GameObject painelFaseConcluida;

    [DllImport("__Internal")]
    public static extern void SistemaVerifConclusaoFase(bool situacaoFase);
    [DllImport("__Internal")]
    public static extern void SistemaReiniciarWorkspaceBlockly();
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    void ativarPainel()
    {
        SistemaReiniciarWorkspaceBlockly();
        SistemaVerifConclusaoFase(true);//se colidir com a runa quer dizer que passei de fase, portanto eu reseto o espaco blockly
        painelFaseConcluida.SetActive(true);
    }


}
