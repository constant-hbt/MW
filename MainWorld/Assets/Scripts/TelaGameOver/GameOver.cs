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
        yield return new WaitForSeconds(120f);
        SceneManager.LoadScene("TelaInicio");
    }

    public void btnVoltarMenuInicial()
    {
        StartCoroutine(VoltarMenu());
    }
    IEnumerator VoltarMenu()
    {
        yield return new WaitForSeconds(0.6f);
        SceneManager.LoadScene("TelaInicio");
    }
}
