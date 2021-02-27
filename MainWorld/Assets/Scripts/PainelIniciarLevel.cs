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

    public void btnIniciarLevel()//botaoUtilizado no painel que inicia com cada level
    {
        
       // DisponibilizarToobox();
       // SistemaDeEnableDisableBlocos(false);//quando o jogo estiver na tela inicial os blocos estarão desabilitados e não mostrar a mensagem com o restante dos blocos
        this.gameObject.SetActive(false);
    }

    public void btnIniciar() //botao utilizado no painelIntroChefe
    {
       //  DisponibilizarToobox();
       //   SistemaDeEnableDisableBlocos(false);
        this.gameObject.SetActive(false);
       
    }
    public void HabilitarBtnIniciarLevel()
    {
       
            botaoIniciarLevel.enabled = true;
        
    }
}
