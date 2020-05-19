using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class PainelConclusãoFase : MonoBehaviour
{
    private GameController _gameController;
    private ControllerFase _controllerFase;

    public TextMeshProUGUI tmpEstrelas;
    public TextMeshProUGUI tmpMoedas;
    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;

        tmpEstrelas.text = (_gameController.numEstrelas += 3).ToString() ;
         tmpMoedas.text = _gameController.numGold.ToString();
        
    }
    void Update()
    {
        
    }

    public void btnReiniciar(int numeroFase)
    {
        switch (numeroFase)
        {
            case 1:
                SceneManager.LoadScene("Fase1");
                break;
            case 2:
                SceneManager.LoadScene("Fase2");
                break;
        }

        
    }

    public void btnPlay(int idFase)
    {
        if(_gameController.fasesConcluidas < idFase)
        {//caso o numero de fases concluidas for menor que o id da Fase quer dizer que o jogador ainda nao havia concluido aquela fase
         //portanto a varaivel recebe o idFase , habilitando o mapa para a proxima fase, e deixando a fase correpondente
         //ao idFase como concluida
            _gameController.fasesConcluidas = idFase;
        }
        SceneManager.LoadScene("SelecaoFase");

        
    }

}
