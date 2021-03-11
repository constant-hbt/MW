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

    private bool validCliqExcluir;
    private void Awake()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        _painelCarregar = FindObjectOfType(typeof(PainelCarregar)) as PainelCarregar;
        _saveController = FindObjectOfType(typeof(Save_Controller)) as Save_Controller;

        validCliqExcluir = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jogar()
    {
        _gameController.id_save_game = this.id_save_game;
        _gameController.numEstrelas = Convert.ToInt32(this.txtEstrelas.text.ToString());
        _gameController.numGold = Convert.ToInt32(this.txtMoedas.text.ToString());
        _gameController.numVida = Convert.ToInt32(this.txtVidas.text.ToString());
        _gameController.fasesConcluidas = this.fasesConcluidas;

        MarcarPerguntasRespondidas(this.fasesConcluidas);

        _gameController.descricaoFase = "SelecaoFase";
        SceneManager.LoadScene("TelaCarregamento");

    }

    public void MarcarPerguntasRespondidas(int ultima_fase_concluida)
    {
        for (int i = 0; i < ultima_fase_concluida; i++)
        {
            _gameController.perguntasRespondidas[i] = true;
        }
    }
    public void btnExcluirSave()
    {
        if (validCliqExcluir)
        {
            _saveController.ChamarDeletarSave(id_save_game, _painelCarregar.LimparPaineis);
            validCliqExcluir = false;
            StartCoroutine(HabilitBtnExcluir());
        }

    }

    IEnumerator HabilitBtnExcluir()
    {
        yield return new WaitForSeconds(0.3f);
        validCliqExcluir = true;
    }
}
