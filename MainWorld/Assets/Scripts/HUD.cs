﻿using System.Collections;
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
    public GameObject objVidaChefao;

    [Header("Controle HUD")]
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI vidaText;
    public TextMeshProUGUI tentativasText;
    public TextMeshProUGUI vidaChefaoText;

    [DllImport("__Internal")]
    public static extern void AlterarLimiteBlocoForcaAtaque(int limitForcaAtaque);
    
    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        _controllerFase = FindObjectOfType(typeof(ControllerFase)) as ControllerFase;


    }
    void Update()
    {
        goldText.text = _controllerFase.qtdMoedasColetadas.ToString();
         vidaText.text = _gameController.numVida.ToString();
        tentativasText.text = _gameController.numTentativasFase.ToString();

        if (objVidaChefao.activeSelf)
        {
            InimigoChefe _inimigoChefe = FindObjectOfType(typeof(InimigoChefe)) as InimigoChefe;
            if(_inimigoChefe != null)
            {
                vidaChefaoText.text = _inimigoChefe.numMortes.ToString();
            }
        }
    }

    public void habilitarObjVidaChefao()
    {
        objVidaChefao.SetActive(true);
    }

    
}
