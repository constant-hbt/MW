using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PainelFaseIncompleta : MonoBehaviour
{

    public Button btnClose;
    public GameObject painelFaseIncompleta;
    void Start()
    {
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
