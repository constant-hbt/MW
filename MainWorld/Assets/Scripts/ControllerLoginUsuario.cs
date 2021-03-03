using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;


public class ControllerLoginUsuario : MonoBehaviour
{
    private GameController _gameController;
    
    [DllImport("__Internal")]
    private static extern void PegarIdPlayer();


    private void Awake()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        

        //PegarIdPlayer();
    }
    void Start()
    {
      
    }

    void Update()
    {
        
    }

    public void PreencherIdUsuario(int id_usuario)
    {
        _gameController.id_usuario = id_usuario;
        Debug.Log("Id_usuario vindo do js = " + id_usuario);
    }

   
}
