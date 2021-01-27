using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    private GameController _gameController;
    private bool jaColidiu = false;
    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void acrescentarVida()
    {   
                if (!jaColidiu)
                {
                    _gameController.numVida += 1;
                }
                Destroy(this.gameObject);   
    }
}
