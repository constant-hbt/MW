using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporte : MonoBehaviour
{

    private PlayerController _playerController;// transform do player
    private ControllerFase _controllerFase;
    public Transform destino;
    public Camera cam;
    public Transform[] transicaoCamera;//posicao que a camera deve se encontrar ao mudar de uma parte da fase para outra

    void Start()
    {
        _playerController = FindObjectOfType(typeof(PlayerController)) as PlayerController;
        _controllerFase = FindObjectOfType(typeof(ControllerFase)) as ControllerFase;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void interagindo()
    {
        _controllerFase.fases[1].SetActive(true);
        _playerController.transform.position = destino.position;
        cam.transform.position = transicaoCamera[0].position;
       _controllerFase.fases[0].SetActive(false);
    }
}
