using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunaWin : MonoBehaviour
{

    /// <summary>
    /// RESPONSAVEL PELO OBJETO DE CONCLUSAO DA FASE, HABILITA E DESABILITA O PAINEL DE CONCLUSAO DE FASE APÓS A COLISÃO DO PLAYER COM O OBJETO QUE CONTÉM ESTE SCRIPT
    /// </summary>
    public GameObject painelFaseConcluida;

    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    void ativarPainel()
    {
        painelFaseConcluida.SetActive(true);
    }


}
