using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameController _gameController;
    private ControllerFase _controllerFase;
    public int valorCoin;
    private bool jaColidiu = false;
    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        _controllerFase = FindObjectOfType(typeof(ControllerFase))as ControllerFase;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void coletar(string tipoMoeda)
    {
        switch (tipoMoeda)
        {
            case "coletavel":
                if (!jaColidiu)
                {
                    _controllerFase.qtdMoedasColetadas += valorCoin;
                    jaColidiu = true;
                }
               
                Destroy(this.gameObject);
                break;
            case "loot":
                _controllerFase.qtdMoedasColetadas += valorCoin;
                _controllerFase.qtdMoedasLootColetadas += valorCoin;
                Destroy(this.gameObject);
                break;
        }
        
    }
    
}
