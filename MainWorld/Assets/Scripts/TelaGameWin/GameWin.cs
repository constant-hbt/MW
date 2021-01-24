using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameWin : MonoBehaviour
{
    private GameController _gameController;
    public Button btnVoltarMenu;
    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        StartCoroutine(voltarTelaInicial());


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator voltarTelaInicial()
    {
        btnVoltarMenu.Select();
      //  _gameController.FocusCanvas(1);
        yield return new WaitForSeconds(20f);
        SceneManager.LoadScene("TelaInicio");
    }

    public void pressionarEspaco()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene("TelaInicio");
        }
    }
}
