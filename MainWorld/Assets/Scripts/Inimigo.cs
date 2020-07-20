﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Inimigo : MonoBehaviour
{
    // Start is called before the first frame update

    //TESTE
    private PlayerController _playerController;
    private ControllerFase _controllerFase;
    private SpriteRenderer sRender;
    private Animator _animator;


    [Header("Sistema dano no player")]
    public GameObject objPlayer;

    [Header("Sistema de vida")]
    public int vida;
    public int vidaAtual;
    private float percVida;
    public GameObject barrasVida;
    public Transform vidaCheia;
    public TextMeshProUGUI tmpVida;
    private bool died = false;
    public Transform pontoAnimMorte; //ponto referencia da origem da animacao de morte
    //mostrar o dano
    public GameObject danoTxtPrefab;
    public GameObject explosaoPrefab;
    public GameObject posicaoExplosao;

    [Header("Flip")]
    public bool olhandoEsquerda;//INDICA SE O inimigo ESTA olhando A ESQUERDA OU DIREITA
    public bool playerEsquerda; //INDICA SE O PLAYER ESTÁ A ESQUERDA OU DIREITA DO PERSONAGEM

    [Header("Sistema de invunerabilidade")]
    private bool getHit =false;

    [Header("Prefabs")]
    public GameObject[] fxDano; //array responsavel por guardar as animacoes de dano
    public GameObject fxMorte; //guarda o prefab com a animacao de morte  

    /*
    [Header("Configuração de loot")]
    public GameObject loots;
    */
    void Start()
    {
        _playerController = FindObjectOfType(typeof(PlayerController)) as PlayerController;
        _controllerFase = FindObjectOfType(typeof(ControllerFase)) as ControllerFase;
        sRender = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();

       
        vidaCheia.localScale = new Vector3(0.3f, 0.7f, 1);
        vidaAtual = vida;
        atualizarTMPVida(tmpVida, vidaAtual, vida);
    }

    // Update is called once per frame
    void Update()
    {


        //FAZ COM QUE O INIMIGO SEMPRE ESTEJA VIRADO NA DIRECAO DO PLAYER
        verifDirPlayer();

        //Personagem no chao
        _animator.SetBool("Grounded", true);

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (died) { return; }
        switch (col.gameObject.tag)
        {
            case "arma":
                //IRA FAZER UM TESTE COM A VIDA DO INIMIGO SE A FORCA DO ATAQUE FOR IGUAL A VIDA DO INIMIGO O INIMIGO MORRE E O PLAYER NAO SOFRE NADA
                //SE O ATAQUE FOR MENOR QUE A VIDA DO INIMIGO ENTAO O INIMIGO DA UM HIT NO PLAYER E O PLAYER MORRE E TEM QUE REINICIAR A FASE
                //E SE O DANO DO PLAYER FOR MAIOR QUE A VIDA DO INIMIGO O INIMIGO EXPLODE E MORRE , MAIS DA DANO NO PLAYER E O PLAYER TBM MORRE
                Debug.Log("Tomei dano do player, com a força de "+ _playerController.forcaDano);
                if (!getHit)
                {
                    getHit = true;
                    _animator.SetTrigger("hit");

                    int forcaDanoPlayer = _playerController.forcaDano;
                    vidaAtual -= Mathf.RoundToInt(forcaDanoPlayer); //diminuo a vida do inimigo apartir da forcaDano do player
                    percVida = (float)vidaAtual / (float)vida;//recalculo o percentual de vida do inimigo
                    
                    if(percVida < 0) { percVida = 0; }
                    

                    vidaCheia.localScale = new Vector3(percVida * 0.3f, 0.7f, 1);//modifico o tamanho da barra representativa de acordo com o dano tomado, apos o ataque do player
                    atualizarTMPVida(tmpVida, vidaAtual, vida);
                   
                    if(vidaAtual == 0)
                    {
                        //INIMIGO SOMENTE MORRE
                        Debug.Log("INIMIGO MORREU");
                        died = true;
                        this.gameObject.layer = 9;// muda a layer do inimigo para que o player Principal nao possa arrasta-lo quando o mesmo estiver morto
                        _animator.SetInteger("idAnimation", 1);
                       // StartCoroutine("loot"); DESCOMENTAR QUANDO IMPLEMENTAR A COROUTINE DE LOOT

                    }
                    else if(vidaAtual < 0)
                    {
                        //QUER DIZER QUE PLAYER DEU UM DANO MAIS FORTE QUE O NUMERO DE VIDA DO INIMIGO, PORTANTO O INIMIGO MORRE , MAIS SOLTA UMA EXPLOSAO QUE ATINGE O PLAYER
                       
                        Debug.Log("INIMIGO MORREU MAIS EXPLODIU E DEU DANO NO PLAYER");
                        died = true;
                        this.gameObject.layer = 9;
                        _animator.SetInteger("idAnimation", 1);

                        objPlayer.SendMessage("explosaoInimigo", SendMessageOptions.DontRequireReceiver);

                        StartCoroutine("loot");//PROXIMO PASSO SERÁ IMPLEMENTAR A ANIMACAO DA EXPLOSAO DO PLAYER E O DANO TOMADO

                    }
                    else if(vidaAtual > 0)
                    {
                        //INIMIGO SOLTA UM ATAQUE
                        Debug.Log("INIMIGO COTRA-ATACOU COM UM HIT");
                    }

                    //INSTANCIANDO PREFABS


                    GameObject danoTemp = Instantiate(danoTxtPrefab, transform.position, transform.localRotation);//mostrando dano tomado
                    danoTemp.GetComponentInChildren<TextMeshPro>().text = forcaDanoPlayer.ToString(); //atualizando o texto do prefab para mostrar o dano naquele momento
                    danoTemp.GetComponentInChildren<MeshRenderer>().sortingLayerName = "HUD";
                    int forcaX = 50;//Fazer o dano sair um pouco para o lado
                    if (playerEsquerda == false)
                    {
                        forcaX *= -1;
                    }
                    danoTemp.GetComponent<Rigidbody2D>().AddForce(new Vector2(forcaX, 230));//jogar o dano para cima
                    Destroy(danoTemp, 0.7f);//DESTROI O DANO CRIADO

                    //EFEITO HIT
                    GameObject fxTemp = Instantiate(fxDano[0], transform.position, transform.localRotation);
                    Destroy(fxTemp, 1);

                }


                break;
        }
    }

    void flip()
    {//USADO PRIMEIRAMENTE NO KNOCKBACK
        olhandoEsquerda = !olhandoEsquerda; // inverte o valor da var bool
        float x = transform.localScale.x;

        x *= -1; // inverte o sinal do scale x

        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);

        //faz com que a barra de vida altere o lado conforme personagem se vira
        barrasVida.transform.localScale = new Vector3(barrasVida.transform.localScale.x * -1 , barrasVida.transform.localScale.y, barrasVida.transform.localScale.z);
        posicaoExplosao.transform.localScale = new Vector3(posicaoExplosao.transform.localScale.x * -1, posicaoExplosao.transform.localScale.y, posicaoExplosao.transform.localScale.z);
    }

    void atualizarTMPVida(TextMeshProUGUI tmpVida, int vidaAtual, int vida)
    {
        if (vidaAtual < 0) { vidaAtual = 0; }

        tmpVida.text = vidaAtual + "/" + vida;
    }

    void verifDirPlayer()
    {
        float xPlayer = _playerController.transform.position.x;

        if (xPlayer < transform.position.x)
        {
            playerEsquerda = true;
        }
        else if (xPlayer > transform.position.x)
        {
            playerEsquerda = false;
        }

        
         if (olhandoEsquerda == false && playerEsquerda == true)
        {
            flip();
        }
         if (olhandoEsquerda == true && playerEsquerda == false)
        {
            flip();
        }
        
    }

    IEnumerator loot()
    {
        yield return new WaitForSeconds(1);

        GameObject fxMorte = Instantiate(this.fxMorte, pontoAnimMorte.position, transform.localRotation);
        yield return new WaitForSeconds(0.5f);//depois de meio segundo desabilita a imagem do inimigo
        sRender.enabled = false;

       /* //Controle de loot
        int qtdMoedas = Random.Range(1, 5);
        for (int l = 0; l < qtdMoedas; l++)
        {
            GameObject lootTemp = Instantiate(loots, transform.position, transform.localRotation);//Instanciando a coin
            lootTemp.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-35, 35), 100));
            yield return new WaitForSeconds(0.1f);

        }*/


        yield return new WaitForSeconds(0.2f);//depois de um segundo destroi a animacao de morte e o inimigo
        Destroy(fxMorte, 1);
        Destroy(this.gameObject);

    }

}