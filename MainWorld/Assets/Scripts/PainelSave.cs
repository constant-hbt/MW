using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
public class PainelSave : MonoBehaviour
{
    private GameController _gameController;
    private PainelCarregar _painelCarregar;
    private Save_Controller _saveController;

    public int id_save_game;
    public TextMeshProUGUI porcConcluida;
    public TextMeshProUGUI txtEstrelas;
    public TextMeshProUGUI txtMoedas;
    public TextMeshProUGUI txtVidas;
    public int fasesConcluidas;
    

    public Button btnJogar;

    private void Awake()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        _painelCarregar = FindObjectOfType(typeof(PainelCarregar)) as PainelCarregar;
        _saveController = FindObjectOfType(typeof(Save_Controller)) as Save_Controller;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jogar()
    {
        _gameController.numEstrelas = Convert.ToInt32(this.txtEstrelas.text.ToString());
        _gameController.numGold = Convert.ToInt32(this.txtMoedas.text.ToString());
        _gameController.numVida = Convert.ToInt32(this.txtVidas.text.ToString());
        _gameController.fasesConcluidas = this.fasesConcluidas;

        _gameController.descricaoFase = "SelecaoFase";
        SceneManager.LoadScene("TelaCarregameto");

    }
    public void btnExcluirSave()
    {
        _saveController.ChamarDeletarSave(id_save_game);

        _painelCarregar.LimparPaineis();
        _saveController.ChamarBuscarSaves(/*_gameController.id_usuario*/50, _painelCarregar.CarregarPainelSaves);


    }

    
}
