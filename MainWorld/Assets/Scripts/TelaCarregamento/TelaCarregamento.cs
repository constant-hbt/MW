using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class TelaCarregamento : MonoBehaviour
{
    private GameController _gameController;
    private AudioController _audioController;

    public string cenaACarregar;//cena que irei carregar
    public float tempoFixoSeg = 0.2f;
    private bool ativarCena = false;
    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        _audioController = FindObjectOfType(typeof(AudioController)) as AudioController;
        ativarCena = false;
    }

    void Update()
    {
        if (!ativarCena)
        {
            ativarCena = true;
            StartCoroutine(tempoFixo());
        }
    }

    IEnumerator tempoFixo()
    {
        string descricaoFase = _gameController.descricaoFase;
        yield return new WaitForSeconds(tempoFixoSeg);
        //SceneManager.LoadScene(_gameController.descricaoFase);
        
        if (descricaoFase == "SelecaoFase" || descricaoFase == "Perguntas")
        {
            _audioController.trocarMusica(_audioController.musicaSelecaoFases, descricaoFase, true);
        }else if(descricaoFase == "Fase1" || descricaoFase == "Fase2" || descricaoFase == "Fase3"   ){
            _audioController.trocarMusica(_audioController.musicaFase1a3, descricaoFase, true);
        }else if (descricaoFase == "Fase4" || descricaoFase == "Fase5")
        {
            _audioController.trocarMusica(_audioController.musicaFase4a5, descricaoFase, true);
        }
        else if (descricaoFase == "Fase6" || descricaoFase == "Fase7")
        {
            _audioController.trocarMusica(_audioController.musicaFase6a7, descricaoFase, true);
        }else if (descricaoFase == "Fase8")
        {
            _audioController.trocarMusica(_audioController.musicaFase6a7, descricaoFase, true);
        }
        else if ( descricaoFase == "Fase9")
        {
            _audioController.trocarMusica(_audioController.musicaFase9Parte0e1, descricaoFase, true);
        }else if(descricaoFase == "TelaGameWin" || descricaoFase == "TelaInicio")
        {
            _audioController.trocarMusica(_audioController.musicaTitulo, descricaoFase, true);
        }else if(descricaoFase == "TelaGameOver")
        {
            _audioController.trocarMusica(_audioController.musicaTelaGameOver, descricaoFase, true);
        }
            
       
    }
}
