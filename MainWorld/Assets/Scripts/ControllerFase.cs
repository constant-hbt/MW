using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerFase : MonoBehaviour
{
    private GameObject HUD;
    void Start()
    {
        //HUD = GameObject.FindGameObjectWithTag("HUD");
       // HUD.SetActive(true);//garante que ao iniciar a seleção de fases o HUD das fases esteja ativado
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void testar()
    {
        SceneManager.LoadScene("SelecaoFase");
    }
}
