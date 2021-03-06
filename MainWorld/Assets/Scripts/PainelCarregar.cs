﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PainelCarregar : MonoBehaviour
{
    private Save_Controller _saveController;
    private GameController _gameController;
    private PainelSave _painelSave;

    [Header("Paineis save")]
    public GameObject prefabPainelSave;
    public GameObject contentJogoSalvo;
    public GameObject objNaoPossuiJogoSalvo;
    public GameObject[] paineis;

    private void Awake()
    {
        _saveController = FindObjectOfType(typeof(Save_Controller)) as Save_Controller;
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        _painelSave = FindObjectOfType(typeof(PainelSave)) as PainelSave;

        _saveController.ChamarBuscarSaves(_gameController.id_usuario, CarregarPainelSaves);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BtnClose()
    {
        this.gameObject.SetActive(false);
    }

    public void CarregarPainelSaves(Lista_saves lista_saves)
    {
        if (lista_saves != null)
        {
            if (lista_saves.saves.Length > 0)
            {
                objNaoPossuiJogoSalvo.SetActive(false);
                paineis = new GameObject[lista_saves.saves.Length];

                for (int i = 0; i < lista_saves.saves.Length; i++)
                {
                    paineis[i] = Instantiate(prefabPainelSave, new Vector3(contentJogoSalvo.transform.position.x, 0, 0), Quaternion.identity, contentJogoSalvo.transform);

                    paineis[i].GetComponentInChildren<PainelSave>().id_save_game = lista_saves.saves[i].id_save_game;
                    paineis[i].GetComponentInChildren<PainelSave>().porcConcluida.text = calcPorcConcluida(lista_saves.saves[i].Ultima_fase_concluida);
                    paineis[i].GetComponentInChildren<PainelSave>().txtEstrelas.text = lista_saves.saves[i].Estrelas.ToString();
                    paineis[i].GetComponentInChildren<PainelSave>().txtMoedas.text = lista_saves.saves[i].Moedas.ToString();
                    paineis[i].GetComponentInChildren<PainelSave>().txtVidas.text = lista_saves.saves[i].Vidas.ToString();
                    paineis[i].GetComponentInChildren<PainelSave>().fasesConcluidas = lista_saves.saves[i].Ultima_fase_concluida;

                }
            }
        }
        else
        {
            objNaoPossuiJogoSalvo.SetActive(true);
            Debug.Log("Não há registro de jogos salvos!!");
        }
    }

    public string calcPorcConcluida(int qtdFaseConcluida)
    {
        int numFases = _gameController.numeroFasesGAME;
        float porc = (qtdFaseConcluida * 100) / numFases;

        string resultado = porc + "%";

        return resultado;
    }


    public void LimparPaineis(bool flag)
    {
        //limpa os paineis ja existentes
        if (flag)
        {
            paineis = new GameObject[0];

            PainelSave[] pSaves = FindObjectsOfType<PainelSave>();

            for (int i = 0; i < pSaves.Length; i++)
            {
             
                Destroy(pSaves[i].gameObject);
            }

            _saveController.ChamarBuscarSaves(_gameController.id_usuario, CarregarPainelSaves);

        }
        
    }
}
