using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class PainelConclusãoFase : MonoBehaviour
{
    private GameController _gameController;
    private ControllerFase _controllerFase;

    public TextMeshProUGUI tmpEstrelas;
    public TextMeshProUGUI tmpMoedas;

    [Header("Configuração Estrelas")]

    public GameObject estrela1;
    public GameObject estrela2;
    public GameObject estrela3;
    private int qtdEstrelasAdquiridas;

    void Start()
    {

        qtdEstrelasAdquiridas = _controllerFase.distribuicaoEstrelas();
        switch (qtdEstrelasAdquiridas)
        {
            case 1:
                estrela1.SetActive(true);
                estrela2.SetActive(false);
                estrela3.SetActive(false);
                break;
            case 2:
                estrela1.SetActive(true);
                estrela2.SetActive(true);
                estrela3.SetActive(false);
                break;
            case 3:
                estrela1.SetActive(true);
                estrela2.SetActive(true);
                estrela3.SetActive(true);
                break;
        }

        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        _controllerFase = FindObjectOfType(typeof(ControllerFase)) as ControllerFase;
        tmpEstrelas.text = qtdEstrelasAdquiridas.ToString() ;
         tmpMoedas.text = _controllerFase.qtdMoedasColetadas.ToString();
        
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
         //portanto a variavel recebe o idFase , habilitando o mapa para a proxima fase, e deixando a fase correpondente
         //ao idFase como concluida
            _gameController.fasesConcluidas = idFase;
            
        }
        if(_gameController.EstrelasFases[idFase - 1] <= 3 && _gameController.EstrelasFases[idFase - 1] < qtdEstrelasAdquiridas)
        {
            _gameController.numEstrelas += qtdEstrelasAdquiridas - _gameController.EstrelasFases[idFase - 1];//soma somente a diferenca entre as estrelas que ja havia adquirido nesta fase , com as que adquiri a mais em uma nova tentativa
            _gameController.EstrelasFases[idFase - 1] = qtdEstrelasAdquiridas;
            _gameController.numGold += _controllerFase.qtdMoedasColetadas;
            
        }
        SceneManager.LoadScene("SelecaoFase");

        
    }

}
