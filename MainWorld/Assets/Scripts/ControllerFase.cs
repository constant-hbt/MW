using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerFase : MonoBehaviour
{
    /// <summary>
    /// RESPONSAVEL PELAS CONFIGURAÇÕES UNIVERSAIS DENTRO DE CADA FASE
    /// </summary>
    private GameController _gameController;

    [Header("Coletáveis durante a fase")]
    public int gold;//todas as moedas coletadas durante a fase;
    public int estrelas;//estrelas adquiras com o desempenho na fase

    public GameObject[] fases;
    void Start()
    {
       

        _gameController = FindObjectOfType(typeof(GameController)) as GameController;

        if(fases.Length != 0)
        {
            print("entrei");
            foreach (GameObject o in fases)
            {
                
                o.SetActive(false);
            }//desabilita todas as partes da fase , e em seguida habilita somente a primeira parte
            fases[0].SetActive(true);
        }
    }

   
    void Update()
    {
        
    }


    //MUDAR FOCO

    public void mudandoFocoParaWebGL()
    {

        WebGLInput.captureAllKeyboardInput = true;

    }
    public void mudandoFocoParaPagina()
    {
        WebGLInput.captureAllKeyboardInput = false;
    }
}
