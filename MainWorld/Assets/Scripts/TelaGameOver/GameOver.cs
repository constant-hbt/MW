using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private GameController _gameController;
    
    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        StartCoroutine(voltarTelaInicial());
    }

    
    void Update()
    {
        
    }

    IEnumerator voltarTelaInicial()
    {
        Destroy(_gameController.gameObject);
        yield return new WaitForSeconds(6.5f);
        SceneManager.LoadScene("TelaInicio");
    }
}
