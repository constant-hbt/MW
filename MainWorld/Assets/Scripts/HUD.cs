using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        _controllerFase = FindObjectOfType(typeof(ControllerFase)) as ControllerFase;
       
    }
    void Update()
    {
        goldText.text = _controllerFase.qtdMoedasColetadas.ToString();
        vidaText.text = _gameController.numVida.ToString();
    }

    
}
