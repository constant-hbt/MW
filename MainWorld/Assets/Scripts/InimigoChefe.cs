using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InimigoChefe : MonoBehaviour
{
    
    private PlayerController _playerController;
    private ControllerFase _controllerFase;
    private SpriteRenderer sRender;
    private Animator _animator;


    [Header("Sistema de vida")]
    public int numMortes;
    public int vida;
    public int vidaAtual;
    private float percVida;
    public GameObject barrasVida;
    public Transform vidaCheia;
    public TextMeshProUGUI tmpVida;
    public GameObject objTmpVida;
    public Transform pontoAnimMorte; //ponto referencia da origem da animacao de morte
    //public Transform pontoSaidaLoot; //ponto de referencia da origem dos loots
    //mostrar o dano
    public float ladoEfeitoDano = 0.122f; //posicao em que a animacao de efeito do dano irá aparecer
    public GameObject danoTxtPrefab;
    public GameObject efeitoExplosaoPrefab;

    [Header("Sistema de ataque")]
    public int forcaDanoInim;
    public PolygonCollider2D colliderAttack;

    [Header("Flip")]
    public bool olhandoEsquerda;//INDICA SE O inimigo ESTA olhando A ESQUERDA OU DIREITA
    public bool playerEsquerda; //INDICA SE O PLAYER ESTÁ A ESQUERDA OU DIREITA DO PERSONAGEM

    [Header("Sistema de invunerabilidade")]
   // private bool getHit =false;

    [Header("Prefabs")]
    public GameObject[] fxDano; //array responsavel por guardar as animacoes de dano
    public GameObject fxMorte; //guarda o prefab com a animacao de morte  
    public GameObject fxHitPlayer;//guarda o prefab da animação de quando ele explode e da um hit no player

    [Header("Sistema de posicionamento")]
    public GameObject[] posicaoX;
    public GameObject[] posicaoY;
    public int posicaoAtual;
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
    void FixedUpdate()
    {
        //FAZ COM QUE O INIMIGO SEMPRE ESTEJA VIRADO NA DIRECAO DO PLAYER
        verifDirPlayer();

        //Personagem no chao
        _animator.SetBool("Grounded", true);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "arma":
                //IRA FAZER UM TESTE COM A VIDA DO INIMIGO SE A FORCA DO ATAQUE FOR IGUAL A VIDA DO INIMIGO O INIMIGO MORRE E O PLAYER NAO SOFRE NADA
                //SE O ATAQUE FOR MENOR QUE A VIDA DO INIMIGO ENTAO O INIMIGO DA UM HIT NO PLAYER E O PLAYER MORRE E TEM QUE REINICIAR A FASE
                //E SE O DANO DO PLAYER FOR MAIOR QUE A VIDA DO INIMIGO O INIMIGO EXPLODE E MORRE , MAIS DA DANO NO PLAYER E O PLAYER TBM MORRE

                // getHit = true;
                _animator.SetTrigger("hit");

                int forcaDanoPlayer = _playerController.forcaDano;
                vidaAtual -= Mathf.RoundToInt(forcaDanoPlayer); //diminuo a vida do inimigo apartir da forcaDano do player
                calcularVida();

                if (vidaAtual == 0)
                {
                    numMortes--;
                    if(posicaoAtual < 2)
                    {
                        posicaoAtual++;
                    }
                    else
                    {
                        posicaoAtual = 0;
                    }
                    //INIMIGO SOMENTE MORRE
                    if (numMortes <= 0)
                    {
                        this.gameObject.tag = "Untagged";//muda a tag para o player nao collider com o inimigo depois que ele estiver morto
                        this.gameObject.layer = 9;// muda a layer do inimigo para que o player Principal nao possa arrasta-lo quando o mesmo estiver morto
                        _animator.SetInteger("idAnimation", 1);
                        this.StartCoroutine("morteInim");
                    }
                    else
                    {
                        vidaAtual = vida;
                        StartCoroutine(Reposicionar());
                        //calcularVida();
                        //this.gameObject.transform.position = new Vector3(posicaoX[posicaoAtual-1].transform.position.x, posicaoY[posicaoAtual-1].transform.position.y, this.gameObject.transform.position.z);
                    }

                }
                else if (vidaAtual < 0)
                {
                    //QUER DIZER QUE PLAYER DEU UM DANO MAIS FORTE QUE O NUMERO DE VIDA DO INIMIGO, PORTANTO O INIMIGO MORRE , MAIS SOLTA UMA EXPLOSAO QUE ATINGE O PLAYER
                    this.gameObject.tag = "Untagged";//muda a tag para o player nao collider com o inimigo depois que ele estiver morto
                    this.gameObject.layer = 9;
                    _animator.SetInteger("idAnimation", 1);

                    StartCoroutine("morteInim");

                    _playerController.SendMessage("explosaoInimigo", forcaDanoInim, SendMessageOptions.DontRequireReceiver);
                }
                else if (vidaAtual > 0)
                {
                    StartCoroutine(contraAtaqueRapido());
                }

                //INSTANCIANDO PREFABS
                GameObject danoTemp = Instantiate(danoTxtPrefab, transform.position, transform.localRotation);//mostrando dano tomado
                danoTemp.GetComponentInChildren<TextMeshPro>().text = forcaDanoPlayer.ToString(); //atualizando o texto do prefab para mostrar o dano naquele momento
                danoTemp.GetComponentInChildren<MeshRenderer>().sortingLayerName = "HUD";
                int forcaX = 20;//Fazer o dano sair um pouco para o lado
                if (playerEsquerda == false)
                {
                    forcaX *= -1;
                }
                danoTemp.GetComponent<Rigidbody2D>().AddForce(new Vector2(forcaX, 150));//jogar o dano para cima
                Destroy(danoTemp, 0.7f);//DESTROI O DANO CRIADO

                //EFEITO HIT
                GameObject fxTemp = Instantiate(fxDano[0], transform.position, transform.localRotation);
                Destroy(fxTemp, 1);

                break;

            case "escudoPlayer":
                Debug.Log("Entrei dentro da case escudoPlayer");
                GameObject efeitoTemp = Instantiate(efeitoExplosaoPrefab, new Vector3(col.gameObject.transform.position.x + ladoEfeitoDano, col.gameObject.transform.position.y, col.gameObject.transform.position.z), this.gameObject.transform.localRotation);
                Destroy(efeitoTemp, 0.2f);
                _playerController.habilitarDesabilitarColliderEscudo(1);
                break;
        }
    }

    void flip()
    {//USADO PRIMEIRAMENTE NO KNOCKBACK
        olhandoEsquerda = !olhandoEsquerda; // inverte o valor da var bool
        float x = transform.localScale.x;

        if (olhandoEsquerda && playerEsquerda == true && objTmpVida.transform.localScale.x > -1) //garante que o tmpVida vai estar sempre virado corretamente para frente
        {
            objTmpVida.transform.localScale = new Vector3(objTmpVida.transform.localScale.x * -1, objTmpVida.transform.localScale.y, objTmpVida.transform.localScale.z);

        }
        if (!olhandoEsquerda && playerEsquerda == false && objTmpVida.transform.localScale.x < 1)
        {
            objTmpVida.transform.localScale = new Vector3(objTmpVida.transform.localScale.x * -1, objTmpVida.transform.localScale.y, objTmpVida.transform.localScale.z);

        }



        x *= -1; // inverte o sinal do scale x

        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
        ladoEfeitoDano *= x;
        //faz com que a barra de vida altere o lado conforme personagem se vira
        //barrasVida.transform.localScale = new Vector3(barrasVida.transform.localScale.x * -1 , barrasVida.transform.localScale.y, barrasVida.transform.localScale.z);
    }

    IEnumerator Reposicionar()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
        barrasVida.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        this.GetComponent<SpriteRenderer>().enabled = true;
        barrasVida.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        this.GetComponent<SpriteRenderer>().enabled = false;
        barrasVida.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        this.GetComponent<SpriteRenderer>().enabled = true;
        barrasVida.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        this.GetComponent<SpriteRenderer>().enabled = false;
        barrasVida.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        this.GetComponent<SpriteRenderer>().enabled = true;
        barrasVida.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        this.GetComponent<SpriteRenderer>().enabled = false;
        barrasVida.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        this.GetComponent<SpriteRenderer>().enabled = true;
        barrasVida.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        this.GetComponent<SpriteRenderer>().enabled = false;
        barrasVida.SetActive(false);
        this.gameObject.transform.position = new Vector3(posicaoX[posicaoAtual - 1].transform.position.x, posicaoY[posicaoAtual - 1].transform.position.y, this.gameObject.transform.position.z);
        calcularVida();
        yield return new WaitForSeconds(0.2f);
        this.GetComponent<SpriteRenderer>().enabled = true;
        barrasVida.SetActive(true);
    }

    void atualizarTMPVida(TextMeshProUGUI tmpVida, int vidaAtual, int vida)
    {
        if (vidaAtual < 0) { vidaAtual = 0; }

        tmpVida.text = vidaAtual + "/" + vida;
    }
    void calcularVida()
    {
        percVida = (float)vidaAtual / (float)vida;//recalculo o percentual de vida do inimigo

        if (percVida < 0) { percVida = 0; }


        vidaCheia.localScale = new Vector3(percVida * 0.3f, 0.7f, 1);//modifico o tamanho da barra representativa de acordo com o dano tomado, apos o ataque do player
        atualizarTMPVida(tmpVida, vidaAtual, vida);
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

    IEnumerator morteInim()
    {

        yield return new WaitForSeconds(1f);

        barrasVida.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        GameObject fxMorte = Instantiate(this.fxMorte, pontoAnimMorte.position, transform.localRotation);

        yield return new WaitForSeconds(0.25f);//depois de meio segundo desabilita a imagem do inimigo

        sRender.enabled = false;

        yield return new WaitForSeconds(0.4f);//depois de um segundo destroi a animacao de morte e o inimigo
        Destroy(fxMorte, 0.5f);

        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);

    }

    public void StartContraAtaque()
    {
        StartCoroutine(contraAtaque());
    }

    IEnumerator contraAtaqueRapido()
    {//Contra-ataque que não permite o player se defender
        yield return new WaitForSeconds(0.5f);
        _animator.SetTrigger("attack");
    }
    IEnumerator contraAtaque()
    {//Contra-ataque que da a possibilidade do player se defender
        yield return new WaitForSeconds(0.8f);
        _animator.SetTrigger("attack");
    }

    public void habilitarColliderAttack(int x)//Habilita e desabilita o collider de ataque 
    {
        switch (x)
        {
            case 0:
                colliderAttack.enabled = true;
                break;

            case 1:
                colliderAttack.enabled = false;
                break;
        }
    }

    public void retirarVidaPlayer()
    {
        _playerController.vidaPlayer -= this.forcaDanoInim;
        GameObject danoTemp = Instantiate(danoTxtPrefab, _playerController.transform.position, _playerController.transform.localRotation);//mostrando dano tomado
        danoTemp.GetComponentInChildren<TextMeshPro>().text = this.forcaDanoInim.ToString(); //atualizando o texto do prefab para mostrar o dano naquele momento
        danoTemp.GetComponentInChildren<MeshRenderer>().sortingLayerName = "HUD";
        int forcaX = 20;//Fazer o dano sair um pouco para o lado
        if (playerEsquerda == true)
        {
            forcaX *= -1;
        }
        danoTemp.GetComponent<Rigidbody2D>().AddForce(new Vector2(forcaX, 150));//jogar o dano para cima
        Destroy(danoTemp, 0.7f);//DESTROI O DANO CRIADO
    }
}
