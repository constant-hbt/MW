using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerFase : MonoBehaviour
{
    private GameController _gameController;

    public GameObject[] fases;
    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;

        if(fases.Length > 0)
        {
            foreach(GameObject o in fases)
            {
                o.SetActive(false);
            }//desabilita todas as partes da fase , e em seguida habilita somente a primeira parte
            fases[0].SetActive(true);
        }
    }

   
    void Update()
    {
        
    }

}
