using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;


public class ControllerLoginUsuario : MonoBehaviour
{
    private GameController _gameController;
    private TelaInicial _telaInicial;
    private Save_Controller _saveController;

    [Header("Paineis save")]
    public GameObject prefabPainelSave;
    public GameObject contentJogoSalvo;

    [DllImport("__Internal")]
    private static extern void PegarIdPlayer();


    private void Awake()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        _saveController = FindObjectOfType(typeof(Save_Controller)) as Save_Controller;

        PegarIdPlayer();
        _saveController.ChamarBuscarSaves(_gameController.id_usuario, CarregarPainelSaves);

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

    public void CarregarPainelSaves(Lista_saves lista_saves)
    {
        if(lista_saves.saves.Length > 0)
        {
            for(int i=0; i< lista_saves.saves.Length; i++)
            {
                Instantiate(prefabPainelSave, new Vector3(contentJogoSalvo.transform.position.x, 0, 0), Quaternion.identity);
            }
        }
    }
}
