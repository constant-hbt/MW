using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class TelaCarregamento : MonoBehaviour
{
    private GameController _gameController;

    public string cenaACarregar;//cena que irei carregar
    public float tempoFixoSeg = 4;
    private bool ativarCena = false;
    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
       
    }

    void Update()
    {
        if (!ativarCena)
        {
            StartCoroutine(tempoFixo());
        }
    }

    IEnumerator tempoFixo()
    {
        yield return new WaitForSeconds(tempoFixoSeg);
        SceneManager.LoadScene(_gameController.descricaoFase);
        ativarCena = true;
    }
}
