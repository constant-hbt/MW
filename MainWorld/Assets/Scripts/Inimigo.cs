using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    // Start is called before the first frame update

    //TESTE
    private PlayerController _playerController;
    private SpriteRenderer sRender;
    private Animator animator;


    [Header("Sistema de KnockBack")]
    public GameObject knockForcePrefab; //força de repulsão
    public Transform knockPosition;
    private float kX;
    public float knockX;//pega o valor padrao do position x
    public bool olhandoEsquerda, playerEsquerda; //INDICA SE ESTOU OLHANDO A ESQUERDA OU DIREITA / INDICA SE O PLAYER ESTÁ A ESQUERDA OU DIREITA DO PERSONAGEM

    [Header("Sistema de vida")]
    public int vida;
    public GameObject barrasVida;
    public Transform vidaCheia;

    [Header("Configuração de loot")]
    public GameObject loots;

    void Start()
    {
        _playerController = FindObjectOfType(typeof(PlayerController)) as PlayerController;
        sRender = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "arma":
                //IRA FAZER UM TESTE COM A VIDA DO INIMIGO SE A FORCA DO ATAQUE FOR IGUAL A VIDA DO INIMIGO O INIMIGO MORRE E O PLAYER NAO SOFRE NADA
                //SE O ATAQUE FOR MENOR QUE A VIDA DO INIMIGO ENTAO O INIMIGO DA UM HIT NO PLAYER E O PLAYER MORRE E TEM QUE REINICIAR A FASE
                //E SE O DANO DO PLAYER FOR MAIOR QUE A VIDA DO INIMIGO O INIMIGO EXPLODE E MORRE , MAIS DA DANO NO PLAYER E O PLAYER TBM MORRE
                Debug.Log("Tomei dano do player, com a força de "+ _playerController.forcaDano);
                break;
        }
    }
    
}
