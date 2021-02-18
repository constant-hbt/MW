using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;


public class ControllerLoginUsuario : MonoBehaviour
{
    private GameController _gameController;
    private TelaInicial _telaInicial;

    [DllImport("__Internal")]
    private static extern void ReceberDadosPlayerLogado();

    [DllImport("__Internal")]
    private static extern void VerificarRegistroPlayerLogado();

    
    void Start()
    {
      /*  _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        _telaInicial = FindObjectOfType(typeof(TelaInicial)) as TelaInicial;

        VerificarRegistroPlayerLogado();

        if ( _telaInicial.haRegistroPlayerL == "haRegistro")
        {
            ReceberDadosPlayerLogado();
        }
        */
    }

    void Update()
    {
        
    }
}
