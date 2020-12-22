using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjetosQueDaoDano : MonoBehaviour
{
    private PlayerController _playerController;

    public GameObject danoTxtPrefab;
    public int valorDano;
    void Start()
    {
        _playerController = FindObjectOfType(typeof(PlayerController)) as PlayerController;
        
    }

    
    void Update()
    {
        
    }

    public void retirarVidaPlayer()
    {
        _playerController.vidaPlayer -= this.valorDano;
        
        GameObject danoTemp = Instantiate(danoTxtPrefab, _playerController.transform.position, _playerController.transform.localRotation);//mostrando dano tomado
        danoTemp.GetComponentInChildren<TextMeshPro>().text = this.valorDano.ToString(); //atualizando o texto do prefab para mostrar o dano naquele momento
        danoTemp.GetComponentInChildren<MeshRenderer>().sortingLayerName = "HUD";
        int forcaX = 20;//Fazer o dano sair um pouco para o lado
        danoTemp.GetComponent<Rigidbody2D>().AddForce(new Vector2(forcaX, 150));//jogar o dano para cima
        Destroy(danoTemp, 0.7f);//DESTROI O DANO CRIADO
    }
}
