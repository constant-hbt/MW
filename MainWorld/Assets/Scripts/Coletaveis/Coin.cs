using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameController _gameController;
    private ControllerFase _controllerFase;
    public int valorCoin;
    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        _controllerFase = FindObjectOfType(typeof(ControllerFase))as ControllerFase;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void coletar()
    {
        //_gameController.numGold += valorCoin;
        //_controllerFase.gold += valorCoin; -- apagar depois
        Destroy(this.gameObject);
    }
}
