using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;

public class GameController : MonoBehaviour
{

    /// <summary>
    /// ARMAZENA TODOS OS DADOS QUE DEVEM SER TRANSITADOS DENTRE AS FASES
    /// </summary>
    [Header("Banco de Dados Seleção de Fase")]
    public int fasesConcluidas;
    public int progressoAtual;
    public int numGold;
    public int numVida;
    public int numEstrelas;

    [Header("Banco de Dados do Player")]
    public int vidaMax = 3;

    public GameObject[] gameControllers;
    void Start()
    {
        gameControllers = GameObject.FindGameObjectsWithTag("GameController");
        if (gameControllers.Length >= 2)
        {//Ao mudar de cena caso tenha 2 scripts gameController ele deleta um e deixa somente o script vinculado a primeira fase que fica transitando entre as fases
            Destroy(gameControllers[1]);
        }
        DontDestroyOnLoad(this.gameObject);

    }

    void Update()
    {

        
    }
    //SISTEMA DE ALTERNAR O FOCO NA PAGINA E NO WEBGL
    public void FocusCanvas(int focus)
    {
        if(focus == 0)
        {
            WebGLInput.captureAllKeyboardInput = false;//página estará com o foco
            
        }
        else if(focus == 1)
        {
            WebGLInput.captureAllKeyboardInput = true;//WebGL estará com o foco
            
        }
    }
}
