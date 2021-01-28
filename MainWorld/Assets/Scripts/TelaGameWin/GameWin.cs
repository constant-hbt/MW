using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.InteropServices;
public class GameWin : MonoBehaviour
{
    private GameController _gameController;
    public Button btnVoltarMenu;

    [DllImport("__Internal")]
    public static extern void CentralizarWebGl();
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
        _gameController.descricaoFase = "TelaInicio";
        yield return new WaitForSeconds(180f);
        SceneManager.LoadScene("TelaCarregamento");
    }

    public void btnPlay()
    {
        StartCoroutine(CoroutineBtnPlay());
    }
    IEnumerator CoroutineBtnPlay()
    {
        _gameController.descricaoFase = "TelaInicio";
        yield return new WaitForSeconds(0.6f);
        SceneManager.LoadScene("TelaCarregamento");
    }
}
