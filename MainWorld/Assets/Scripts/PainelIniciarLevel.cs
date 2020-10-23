using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class PainelIniciarLevel : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void SistemaDeEnableDisableBlocos(bool situacao);

    [DllImport("__Internal")]
    private static extern void DisponibilizarToobox();
    public Button botaoIniciarLevel;
    
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void btnIniciarLevel()
    {
        //Teste com estado do game
        //estadoAtual = GameState.GAMEPLAY; APAGAR DEPOIS

       // DisponibilizarToobox();
      //  SistemaDeEnableDisableBlocos(false);//quando o jogo estiver na tela inicial os blocos estarão desabilitados e não mostrar a mensagem com o restante dos blocos
        this.gameObject.SetActive(false);
    }

    public void HabilitarBtnIniciarLevel()
    {
       
            botaoIniciarLevel.enabled = true;
        
    }
}
