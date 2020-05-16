using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerFase : MonoBehaviour
{
    private GameController _gameController;
    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
    }

   
    void Update()
    {
        
    }

    public void testar()
    {
        SceneManager.LoadScene("SelecaoFase");
        _gameController.fasesConcluidas = _gameController.fasesConcluidas + 1;
    }
}
