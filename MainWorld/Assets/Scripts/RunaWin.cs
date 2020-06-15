using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class RunaWin : MonoBehaviour
{

    /// <summary>
    /// RESPONSAVEL PELO OBJETO DE CONCLUSAO DA FASE, HABILITA E DESABILITA O PAINEL DE CONCLUSAO DE FASE APÓS A COLISÃO DO PLAYER COM O OBJETO QUE CONTÉM ESTE SCRIPT
    /// </summary>
    public              GameObject      painelFaseConcluida;

    [DllImport("__Internal")]
    public static extern void           SistemaReiniciarWorkspaceBlockly();

    


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    void ativarPainel()
    {
        painelFaseConcluida.SetActive(true);
        StartCoroutine("reiniciarWorkspace");//assim que o painel aparecer espera-se alguns segundos e reinicia o espaço blockly
    }
    IEnumerator reiniciarWorkspace()
    {
        yield return new WaitForSeconds(0.5f);
        SistemaReiniciarWorkspaceBlockly();
    }


}
