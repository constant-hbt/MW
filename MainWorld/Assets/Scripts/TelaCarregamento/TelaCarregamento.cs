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
    public float tempoFixoSeg = 0.5f;
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
            Debug.Log("Entrei aqui");
            _audioController.trocarMusica(_audioController.musicaSelecaoFases, descricaoFase, true);
        }else if(descricaoFase == "Fase1" || descricaoFase == "Fase2" || descricaoFase == "Fase3" || descricaoFase == "Fase4"||
                 descricaoFase == "Fase5" || descricaoFase == "Fase6" || descricaoFase == "Fase7" || descricaoFase == "Fas8" || descricaoFase == "Fase9"){
            _audioController.trocarMusica(_audioController.musicaFases, descricaoFase, true);
        }
            
       
    }
}
