using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
public class RunaWin : MonoBehaviour
{

    /// <summary>
    /// RESPONSAVEL PELO OBJETO DE CONCLUSAO DA FASE, HABILITA E DESABILITA O PAINEL DE CONCLUSAO DE FASE APÓS A COLISÃO DO PLAYER COM O OBJETO QUE CONTÉM ESTE SCRIPT
    /// </summary>
    /// 
    private ControllerFase _controllerFase;
    public              GameObject      painelFaseConcluida;

    [DllImport("__Internal")]
    public static extern void           SistemaReiniciarWorkspaceBlockly();

    


    void Start()
    {
        _controllerFase = FindObjectOfType(typeof(ControllerFase)) as ControllerFase;
    }

    
    void Update()
    {
        
    }

    void ativarPainel()
    {
        _controllerFase.data_FimFase = DateTime.Now.ToLocalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");//pega a hora em que ativa o painel de conclusao de fase e assim o usuario terá concluido a fase
        painelFaseConcluida.SetActive(true);
        StartCoroutine("reiniciarWorkspace");//assim que o painel aparecer espera-se alguns segundos e reinicia o espaço blockly
    }
    IEnumerator reiniciarWorkspace()
    {
        yield return new WaitForSeconds(0.5f);
        SistemaReiniciarWorkspaceBlockly();
    }


}
