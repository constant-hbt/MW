using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Flecha : MonoBehaviour
{
    private PlayerController _playerController;

    public GameObject danoTxtPrefab;
    public int valorDano;
    public GameObject efeitoExplosaoPrefab;
    void Start()
    {
        _playerController = FindObjectOfType(typeof(PlayerController)) as PlayerController;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Player":
                _playerController.GetComponent<Animator>().SetTrigger("hit");
                retirarVidaPlayer();
                Destroy(this.gameObject);
                break;
            case "invencivel":
                GameObject efeitoTemp = Instantiate(efeitoExplosaoPrefab,this.gameObject.transform.position, this.gameObject.transform.localRotation);
                Destroy(efeitoTemp, 0.2f);
                Destroy(this.gameObject);
                break;
        }
    }

    void retirarVidaPlayer()
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
