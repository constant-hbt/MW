using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.InteropServices;

public class HUD : MonoBehaviour
{

    /// <summary>
    /// RESPONSAVEL PELAS CONFIGURACOES DO HUD COM DADOS DO PLAYER
    /// </summary>
    private GameController _gameController;
    private ControllerFase _controllerFase;
 

    [Header("Controle HUD")]
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI vidaText;
    public TextMeshProUGUI manaText;

    [DllImport("__Internal")]
    public static extern void AlterarLimiteBlocoForcaAtaque(int limitForcaAtaque);
    
    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        _controllerFase = FindObjectOfType(typeof(ControllerFase)) as ControllerFase;


        //manaText.text = _gameController.manaPlayer.ToString();
    }
    void Update()
    {
        goldText.text = _controllerFase.qtdMoedasColetadas.ToString();
       // vidaText.text = _gameController.numVida.ToString();
        
        
    }

    public void alterarMana(int mana)
    {
        manaText.text = mana.ToString();
        AlterarLimiteBlocoForcaAtaque(mana);
    }

    
}
