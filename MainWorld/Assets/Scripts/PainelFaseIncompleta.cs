using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PainelFaseIncompleta : MonoBehaviour
{
    private GameController _gameController;

    public Button btnClose;
    public GameObject painelFaseIncompleta;
    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;

        _gameController.numVida --;
        btnClose.Select();
    }
    void Update()
    {
        
    }

    public void jogarNovamente(int idFase)
    {
        SceneManager.LoadScene("Fase"+idFase);
    }
    public void voltarSelecaoFase()
    {
        SceneManager.LoadScene("SelecaoFase");
    }

    public void ativarFaseIncompleta()
    {
        painelFaseIncompleta.SetActive(true);
    }
}
