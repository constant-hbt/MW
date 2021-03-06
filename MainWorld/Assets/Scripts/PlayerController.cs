﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{

    /// <summary>
    /// TEM UM ERRO NA LINHA 513 - TEM HAVER COM A INSTANCIACAO DO INIMIGO
    /// </summary>

    private SpriteRenderer sRender;
    private         Animator        playerAnimator;
    private         Rigidbody2D     playerRB;
    private         GameController  _gameController;
    private         ControllerFase  _controllerFase;
    private Inimigo _inimigo;
    private InimigoArqueiro _inimigoArqueiro;
    private PainelFaseIncompleta _painelFaseIncompleta;

    [Header("Sistema de vida")]
    public int vidaPlayer ;//vida do player dentro da fase
    public Transform pontoAnimMorte;//guarda as coordenadas dos eixos do ponto de origem que a animacao deve iniciar
    private bool estaMorto ;//verifica se o player esta morto
    private bool ativPainelPosMorte; //ativa o painel apos a morte do player
    public bool tomeiHit;//verifica se o player ja tomou um hit do inimigo, limita o collider de ataque do inimigo a acerta-lo somente uma vez com sua animacao de ataque
    

    [Header("Configuração de movimentação")] 
    public          float           speed; // velocidade de movimento do personagem
    public          float           moveX = 0.8f;
    public          bool            atacando; //indica que o personagem esta atacando
    public          int             IdAnimation; //indica o id da animação
    
    public          Collider2D      collisorEmPé;// Collisor em pé 
    public          Collider2D      collisorAbaixado;//Collisor abaixado
    public Collider2D collisorEscudo;
    
    public          bool            Grounded; //indica se o player esta no chao
    public          LayerMask       oqueEhChao; //indica o que é superficie para o teste do grounded
    public          Transform       groundCheck; //objeto responsavel por fazer a detecção para vermos se estamos tocando o chão

    public          float           jumpForceY_puloSimples = 15;
    public          float           jumpForceY_pularFrente = 10; //força aplicada no eixo y para gerar o pulo do personagem
    public          float           jumpForceX_pularFrente = 5.5f;//força aplicada no eixo x para gerar o pulo do personagem
    
    public          bool            lookLeft;//indica se o personagem esta olhando para a esquerda
    private         float           x;//pega o scale.x do player
    private float posX; //pega a posicao no eixo x do personagem

    public GameObject[] tileChao; //utilizado somente para mudar a tag do chao enquanto o personagem estiver andando
    
   [Header("RayCast")]
    private         Vector3         cabecaScan = Vector3.up;
    private         Vector3         ombroScan = new Vector3(0.136f, 0.143f, 0);
    private         Vector3         toraxScan = Vector3.right;
    private         Vector3         cinturaScan = Vector3.right;
    private         Vector3         joelhoScan = Vector3.right;
    private         Vector3         peScan = Vector3.right;

    public          Transform       scanRayCabeca;
    public          Transform       scanRayOmbro;
    public          Transform       scanRayTorax;
    public          Transform       scanRayCintura;
    public          Transform       scanRayJoelho;
    public          Transform       scanRayPe;

    private bool rayCast_ColidindoInimigo = false;
    private bool rayCast_NaoColidindoInimigo = true;
   // private bool colidiComInimigo = false;
    public bool colidiMoveInimigo = false;//teleporteInimigo

    private int groundedTravado = 0;
    public          LayerMask       interacao;//define a Layer em que o RayCast deve verificar para ver se há colisão com inimigo
    public          LayerMask       interacaoTeleporte;//define a Layer que o RayCast deve verificar se há colisão com algum objeto teleporte
    public          GameObject      objetoInteração;//pega o GameObject do objeto que o RayCast estiver interagindo

    [Header("Configuração de Ataque")]
    public          PolygonCollider2D colliderAttack1;//colisor da arma usado na animação de ataque1, que só é ativado em um momento especifico da animação de ataque
    public          PolygonCollider2D colliderAttack3;//colisor da arma usado na animação de ataque3, que só é ativado em um momento específico da animação
    public          int             forcaDano;
    
    [Header("Sistema de Configuração de fase")]
   // public          GameObject      painelFaseIncompleta;//gameObject do obj painelFaseIncompleta
    public GameObject painelConclusaoFase;
    public          int             qtdBlocosUsados = -1; // quantidade de blocos usados na fase
    private         int             qtdBlocosCadaParteFase = 0;// quantidade de blocos usados em cada parte da fase(necessario para fases com mais de uma etapa)
    public         bool            passeiFase ;// verifica se o usuário concluiu a de fase
    public bool passeiParteFase = false; //verifica se o usuario passou de terreno na fase caso haja
    private         bool            validarConclusaoFase = false;//verifica se a fase foi concluida ou não
    public        bool            interpreteAcabou = false;//verifica se o interprete js do blockly terminou
    public int parteFase;//denomina em que parte da fase o personagem está
    
    [Header("Sistema de KnockBack")]
    public GameObject knockForcePrefab; //força de repulsão
    public Transform knockPosition;
    // private float kX;
    // public float knockX;//pega o valor padrao do position x

    [Header("Prefabs")]
    public GameObject[] fxDano; //array responsavel por guardar as animacoes de dano
    public GameObject fxMorte; //responsavel por guardar a animacao pos morte do player

    //TESTE  HA INIMIGO
    [DllImport("__Internal")]
    public static extern void CondicaoHaInimigo(bool temp_situacaoInimigo);
   
    [DllImport("__Internal")]
    public static extern void CondicaoNaoHaInimigo(bool temp_situacaoInimigo);

    
    
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody2D>();
        sRender = GetComponent<SpriteRenderer>();
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        _controllerFase = FindObjectOfType(typeof(ControllerFase)) as ControllerFase;
        _inimigo = FindObjectOfType(typeof(Inimigo)) as Inimigo;
        _inimigoArqueiro = FindObjectOfType(typeof(InimigoArqueiro))as InimigoArqueiro;
        _painelFaseIncompleta = FindObjectOfType(typeof(PainelFaseIncompleta)) as PainelFaseIncompleta;

        x = transform.localScale.x;

        passeiFase = false;

        estaMorto = false;
        ativPainelPosMorte = false;
        tomeiHit = false;
        vidaPlayer = 1;
       
    }

  
    private void FixedUpdate()
    {
        

        Grounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f, oqueEhChao);//esse teste so deve acontecer se houver uma colisao com a layer Ground

        if (!Grounded)
        {
            groundedTravado++;
        }else if (Grounded)
        {
            groundedTravado = 0;                    //ESSES DOIS IFS NECESSITAM DE MAIS TESTES MAIS A FRENTE DO PROJETO
        }
        if(groundedTravado == 50)
        {
            StartCoroutine(DestravarPlayer());
        }
   
        if (validarConclusaoFase)
        {
            //qtdBlocosCadaParte não pode ser igual a 0 pois assim entraria no if abaixo no inicio das fases antes mesmo do usuário selecionar os seus blocos
            if (qtdBlocosCadaParteFase == 0)//garante que nao entrara no if abaixo quando nao tiver blocos na area de trabalho
            {
                qtdBlocosUsados = -1;
            }
            else
            {
                qtdBlocosUsados = qtdBlocosCadaParteFase;//qtdBlocosCadaParteFase a quantidade de blocos utilizados pelo usuário , tudo isso acontece através de uma função que está contida no onclick do botão executar
                validarConclusaoFase = false;//permite que quando qtdBlocosUsados já estiver preenchido não fique realizando esse if a cada frame(pois seria desnecessário), economizando assim desempenho
            }
        }

        //Verifica se a solução utilizado pelo usuário foi suficiente para concluir a fase, caso os blocos tenham se encerrado e mesmo assim o player nao tenha chegado ao final da fase, ou de cada parte da fase, é sinal que ele fracassou , portanto aparece o painelFaseIncompleta
        if (interpreteAcabou  && Grounded && playerRB.velocity.x == 0 && playerRB.velocity.y == 0 && !passeiFase && !passeiParteFase && !_gameController.flagPerdiTentativa|| ativPainelPosMorte)
        {
            _gameController.numTentativasFase--;
            if (_gameController.numTentativasFase < 1)
            {
                this.retirarVida();
                if(_gameController.numVida > 0)
                {
                   
                    painelConclusaoFase.SetActive(true);
                    _gameController.SendMessage("adicionarErro");//quando o painel é ativado é sinal de que falhou na fase, por isso adiciona um erro dentro do array na posicao condizente com a fase

                }
                else
                {
                    _gameController.descricaoFase = "TelaGameOver";
                    SceneManager.LoadScene("TelaCarregamento");
                }

                qtdBlocosUsados = -1;
                
            }
            else
            {
                if(_painelFaseIncompleta == null)
                {
                    _painelFaseIncompleta = FindObjectOfType(typeof(PainelFaseIncompleta)) as PainelFaseIncompleta;
                }

                _painelFaseIncompleta.jogarNovamente(_gameController.idFaseEmExecucao);
                _gameController.flagPerdiTentativa = true;
            }

            
            interpreteAcabou = false;
            ativPainelPosMorte = false;
            passeiParteFase = false;

            //salvar no banco
                  //  _controllerFase.EnviarHistorico("Fase" + _gameController.idFaseEmExecucao + "-Parte" + _gameController.parteFaseAtual + 1,_controllerFase.qtdMoedasColetadas, _controllerFase.estrelas,
                   //                          _gameController.numVida, _gameController.ultima_fase_concluida, _gameController.id_usuario, _gameController.id_atividade);
           
        }
        if(vidaPlayer <= 0 && !estaMorto)
        {
            
            playerAnimator.SetInteger("idAnimation", 3);
            StartCoroutine("mortePlayer");
            estaMorto = true;//vai definir que o player esta morto e portanto assim o painel de fase incompleta sera ativado
            //DEPOIS VAI PARA O INTERPRETE E CHAMAR O PAINEL DE FASE INCOMPLETA
        }
        if (estaMorto)
        {
            playerRB.velocity = new Vector2(0, 0);
            playerAnimator.SetInteger("idAnimation", 3);//fixa o idAnimation do animator em 3 para que ao morrer ele trave na animacao de morte, para que nenhuma outra aconteca
            
        }

        if (tomeiHit)
        {
            StartCoroutine("habilitarNovoHit");//se após tomar dano o player ainda estiver vivo , apos 5 segundos é habilitado novamente a tomada de hit;
        }
        
    }
    void Update()
    {
       

        playerAnimator.SetBool("grounded", Grounded);
        playerAnimator.SetFloat("speedY", playerRB.velocity.y);


        //RAYCAST
        interagir();

        interagirInimigo();
    }
    
    //CONTROLE DE COLISÃO
    private void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "armaInimigo":
                //IRA FAZER UM TESTE COM A VIDA DO INIMIGO SE A FORCA DO ATAQUE FOR IGUAL A VIDA DO INIMIGO O INIMIGO MORRE E O PLAYER NAO SOFRE NADA
                //SE O ATAQUE FOR MENOR QUE A VIDA DO INIMIGO ENTAO O INIMIGO DA UM HIT NO PLAYER E O PLAYER MORRE E TEM QUE REINICIAR A FASE
                //E SE O DANO DO PLAYER FOR MAIOR QUE A VIDA DO INIMIGO O INIMIGO EXPLODE E MORRE , MAIS DA DANO NO PLAYER E O PLAYER TBM MORRE
                if (!tomeiHit && this.gameObject.tag != "invencivel")
                {
                    tomeiHit = true;
                   // print("Tomei um dano no inimigo com força igual a = " + _inimigo.forcaDanoInim);//somente utilizado para testes
                    playerAnimator.SetTrigger("hit");

                    //EFEITO HIT
                    GameObject fxTemp = Instantiate(fxDano[0], transform.position, transform.localRotation);
                    Destroy(fxTemp, 0.5f);

                    col.gameObject.transform.parent.parent.SendMessage("retirarVidaPlayer", SendMessageOptions.DontRequireReceiver);//VER A QUESTAO DE RETIRAR VIDA DO PLAYER -- parent.parent pego dois objetos acima do colliderAtaque de acordo com a hierarquia do objeto
                    //VER A QUESTAO QUE O INIMIGO TA DANDO DOIS HITS AO INVES DE UM -- TRAVAR ISSO

                }
                break;
            
            case "Win":
                this.passeiFase = true;
                qtdBlocosUsados = -1;//resetar a var e nao deixar entrar no if que ativa o painel de fase incompleta
                zerarVelocidadeP();//zero a velocidade do player para ele iniciar a nova etapa da fase sem estar se movimentando
                col.gameObject.SendMessage("ativarPainel", SendMessageOptions.DontRequireReceiver);
                break;
            case "coletavel":
                col.gameObject.SendMessage("coletar","coletavel", SendMessageOptions.DontRequireReceiver);
                
                break;
            case "loot":
                col.gameObject.SendMessage("coletar","loot", SendMessageOptions.DontRequireReceiver);
                break;
            case "vida":
                col.gameObject.SendMessage("acrescentarVida", SendMessageOptions.DontRequireReceiver);
                break;
            case "placaAviso":
                _inimigoArqueiro = FindObjectOfType(typeof(InimigoArqueiro)) as InimigoArqueiro;
                Inimigo[] inim = FindObjectsOfType<Inimigo>();

                if(_inimigoArqueiro != null)
                {
                    _inimigoArqueiro.atacar();
                }else if(inim.Length > 0)
                {
                    inim[inim.Length - 1].StartContraAtaque();
                }

                if(_inimigoArqueiro != null || inim != null)
                {
                    col.gameObject.SendMessage("MudarEstado", SendMessageOptions.DontRequireReceiver);
                    col.gameObject.tag = "Untagged";
                }
                
                break;
            case "placaAvancar":

                col.gameObject.SendMessage("MudarEstado", SendMessageOptions.DontRequireReceiver);
                break;
            case "placaAvisoChefao":
                InimigoChefe inimigoChefe = FindObjectOfType(typeof(InimigoChefe)) as InimigoChefe;
                if(inimigoChefe != null)
                {
                    inimigoChefe.StartContraAtaque();

                    col.gameObject.SendMessage("MudarEstado", SendMessageOptions.DontRequireReceiver);
                    col.gameObject.tag = "Untagged";
                }

                break;
            case "moveInimigo":

                if (!colidiMoveInimigo)//garante que a colisao so acontecerá uma unica vez
                {
                    colidiMoveInimigo = true;
                    InimigoTeleporte ini = FindObjectOfType(typeof(InimigoTeleporte)) as InimigoTeleporte;
                    ini.mudarPosicao();
                    ini.jaPassou(col.gameObject);
                    
                }

                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "chao":
                    playerRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;//marca o freeze position x e o rotation z como true
                
                break; 
            case "inimigo":
                playerAnimator.SetTrigger("hit");
                collision.gameObject.SendMessage("retirarVidaPlayer", SendMessageOptions.DontRequireReceiver);
                break;
            case "objetosQueDaoDano":
                
                playerAnimator.SetTrigger("hit");
                collision.gameObject.SendMessage("retirarVidaPlayer", SendMessageOptions.DontRequireReceiver);
                break;
            case "espinhoLateral":
                playerAnimator.SetTrigger("hit");
                
                collision.gameObject.SendMessage("retirarVidaPlayer", SendMessageOptions.DontRequireReceiver);
                StartCoroutine(desabilitarHitEspinhoLateral());
                break;
            case "teleporte":
                zerarVelocidadeP();//zero a velocidade do player para ele iniciar a nova etapa da fase sem estar se movimentando
               // _controllerFase.EnviarHistorico("Fase" + _gameController.idFaseEmExecucao + "-Parte" + (_gameController.parteFaseAtual + 1), _controllerFase.qtdMoedasColetadasCadaParte, _controllerFase.estrelas,
               //                              _gameController.numVida, _gameController.ultima_fase_concluida, _gameController.id_usuario, _gameController.id_atividade);
                parteFase += 1;
                _gameController.parteFaseAtual += 1;
                passeiParteFase = true; // depois de 1.6s eu reseto ela dentro da func interagindo do script teleporte
                collision.gameObject.SendMessage("interagindo", SendMessageOptions.DontRequireReceiver);
               break;
        }
    }
    


    public void Flip()//será utilizado futuramente
    {
        lookLeft = !lookLeft; // inverte o valor da var bool
        x *= -1; // inverte o sinal do scale x
        moveX *= -1;
        jumpForceX_pularFrente *= -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);

        cabecaScan.x = cabecaScan.x * -1;
        ombroScan.x = ombroScan.x * -1;
        toraxScan.x = toraxScan.x * -1;
        cinturaScan.x = cinturaScan.x * -1;
        joelhoScan.x = joelhoScan.x * -1;
        peScan.x = peScan.x * -1;

    }
    void atack(int atk)
    {
        switch (atk)
        {
            case 0:
                atacando = false;

                break;
            case 1:
                atacando = true;
                break;
        }
    }

    void interagir()
    {

        RaycastHit2D scanTeleporte = Physics2D.Raycast(scanRayCintura.transform.position, cinturaScan, 0.4f, interacaoTeleporte /*faz com que teste a colisao somente em objetos que tiverem essa layer*/);
        Debug.DrawRay(scanRayCintura.transform.position, cinturaScan * 0.4f, Color.blue);

        if (scanTeleporte == true)
        {

            objetoInteração = scanTeleporte.collider.gameObject;
        }


    }

    public void interagirInimigo()
    {
        bool testeColisaoInimigo = false;
        bool testeNaoColidindoInimigo = true;

        RaycastHit2D scanCabeca = Physics2D.Raycast(scanRayCabeca.transform.position, cabecaScan, 0.2f, interacao);
        Debug.DrawRay(scanRayCabeca.transform.position, cabecaScan * 0.17f, Color.red);

        RaycastHit2D scanOmbro = Physics2D.Raycast(scanRayOmbro.transform.position, ombroScan, 1.1f, interacao);
        Debug.DrawRay(scanRayOmbro.transform.position, ombroScan * 1.1f, Color.green);

        RaycastHit2D scanTorax = Physics2D.Raycast(scanRayTorax.transform.position, toraxScan, 0.4f, interacao);
        Debug.DrawRay(scanRayTorax.transform.position, toraxScan * 0.4f, Color.yellow);

        RaycastHit2D scanCintura = Physics2D.Raycast(scanRayCintura.transform.position, cinturaScan, 0.4f, interacao /*faz com que teste a colisao somente em objetos que tiverem essa layer*/);
        Debug.DrawRay(scanRayCintura.transform.position, cinturaScan * 0.4f, Color.blue);//faz com que seja visivel a linha seja visivel para teste

        RaycastHit2D scanJoelho = Physics2D.Raycast(scanRayJoelho.transform.position, joelhoScan, 0.3f, interacao);
        Debug.DrawRay(scanRayJoelho.transform.position, joelhoScan * 0.3f, Color.cyan);

        RaycastHit2D scanPe = Physics2D.Raycast(scanRayPe.transform.position, peScan, 0.38f, interacao);
        Debug.DrawRay(scanRayPe.transform.position, peScan * 0.38f, Color.black);
        
        if (scanCabeca == true || scanOmbro == true || scanTorax == true || scanCintura == true || scanJoelho == true || scanPe == true)
        {
            testeColisaoInimigo = true;
            testeNaoColidindoInimigo = false;
            
        }
        else
        {
            testeColisaoInimigo = false;
            testeNaoColidindoInimigo = true;
            
        }

        if (rayCast_ColidindoInimigo != testeColisaoInimigo)
        {
            rayCast_ColidindoInimigo = testeColisaoInimigo;
           // CondicaoHaInimigo(rayCast_ColidindoInimigo);
        }
        if(rayCast_NaoColidindoInimigo != testeNaoColidindoInimigo)
        {
            rayCast_NaoColidindoInimigo = testeNaoColidindoInimigo;
           // CondicaoNaoHaInimigo(rayCast_NaoColidindoInimigo);   
        }
    }

   /* public void retornoFuncHaInimigo()
    {//quando eu terminar de executar o bloco ha inimigos reinicio a var colidiInimigo para ver se a mais inimigos perto do player
        colidiComInimigo = false;
    }*/

    void habilitarColliderAtak(int tipoAtaque)
    {
        switch (tipoAtaque)
        {
            case 1:
                colliderAttack1.enabled = true;
                break;
            case 3:
                colliderAttack3.enabled = true;
                break;
        }
            
        
    }
    public void habilitarDesabilitarColliderEscudo(int status)
    {
        switch (status)
        {
            case 0:
                collisorEscudo.enabled = true;
                break;
            case 1:
                collisorEscudo.enabled = false;
                break;
        }
    }
    //JUNTAR ESTAS DUAS FUNCOES EM UMA SO DEPOIS
    void desabilitarColliderAtak(int tipoAtaque)
    {
        switch (tipoAtaque)
        {
            case 1:
                colliderAttack1.enabled = false;
                break;
            case 3:
                colliderAttack3.enabled = false;
                break;
        }
    }

    //MOVIMENTAÇÃO DO PLAYER ATRAVÉS DOS BLOCOS DE COMANDO
    public IEnumerator Movimentacao(string tipoMovimentacao)
    {
        
        switch (tipoMovimentacao)
        {
            case "avancar":
                desmarcarFreezyX();
                mudarTagChao("semTagChao", parteFase);
                StartCoroutine("Avancar");
                yield return new WaitForSeconds(0.6f );
                pararMovimentacao();
                break;
            case "puloSimples":
                StartCoroutine("PuloSimples");
                yield return new WaitForSeconds(0.2f);
                break;
            case "puloLateral":
                desmarcarFreezyX();
                StartCoroutine("PuloLateral");
                yield return new WaitForSeconds(1f);
                break;
            case "defender":
                habilitarDesabilitarColliderEscudo(0);
                StartCoroutine("Defender");
                yield return new WaitForSeconds(0.75f); // era 0.9f
                break;
            
        }
    }
    IEnumerator Avancar()
    {//configurações da movimentação de avanço do player 
        habilitaColisorEmPe();
        playerRB.velocity = new Vector2((moveX * speed), playerRB.velocity.y);

        playerAnimator.SetInteger("idAnimation", 1);
        passeiFase = false;
        StartCoroutine("diminuirQTDBlocosU");
        yield return null;
    }
    IEnumerator PuloSimples()
    {
        if (Grounded == true)
        {
            playerRB.AddForce(new Vector2(0, jumpForceY_puloSimples));
        }
        StartCoroutine("diminuirQTDBlocosU");//verificar e apagar depois
        yield return null;
    }
    IEnumerator PuloLateral()//ok , falta ajustar a altura
    {
      
        if (Grounded == true)
        {
            playerRB.AddForce(new Vector2(jumpForceX_pularFrente ,jumpForceY_pularFrente));
        }
        StartCoroutine("diminuirQTDBlocosU"); //verificar e apagar depois
        yield return null;
    }
    IEnumerator Defender()//ok
    {
        if (Grounded == true)
        {

            habilitaColisorAbaixado();
            playerAnimator.SetInteger("idAnimation", 2);
        }
        yield return new WaitForSeconds(1.8f);
        playerAnimator.SetInteger("idAnimation", 0);
       
        habilitaColisorEmPe();
        StartCoroutine("diminuirQTDBlocosU");//verificar e apagar depois
    }
    IEnumerator Ataque(int valor_ataque)//ok
    {
        forcaDano = valor_ataque;
        playerAnimator.SetTrigger("attack1");
        //StartCoroutine("diminuirQTDBlocosU");
        yield return new WaitForSeconds(0.2f);
    }
    IEnumerator Attack3()//ok
    {
        playerAnimator.SetTrigger("attack3");
        StartCoroutine("diminuirQTDBlocosU");//VERIFICAR E APAGAR DEPOIS
        yield return null;
    }
    //---------------------------------------------------------------------------------

    public void explosaoInimigo(int forcaAtaqueInimigo) //chama a coroutina com as configuracoes de hitInimigo --> É ativada pelo inimigo
    {
        pararMovimentacao();
        StartCoroutine("hitInimigo", forcaAtaqueInimigo);
    }

    IEnumerator hitInimigo(int forcaAtaqueInimigo)//funcao responsavel por dar o hit no player caso o ataque dele seja maior doque a vida do inimigo
    {
        if(_inimigo == null)
        {
            _inimigo = FindObjectOfType(typeof(Inimigo)) as Inimigo;
        }

        yield return new WaitForSeconds(0.75f); // era 1.5
        GameObject hitTemp = Instantiate(_inimigo.fxHitPlayer, transform.position, transform.localRotation);//instancia a animacao de hit no player
        Destroy(hitTemp, 0.7f);//destroi o prefab de hit
        playerAnimator.SetTrigger("hit");//inicia a animacao de hit
        yield return new WaitForSeconds(0.2f);
        this.vidaPlayer -= forcaAtaqueInimigo;//retira a vida do Player de acordo com a força do ataque do inimigo

    }
    IEnumerator mortePlayer()//funcao responsavel pela animacao de morte do player caso sua vida seja <= 0
    {
        
        yield return new WaitForSeconds(1.2f);
        sRender.enabled = false;//deixa o player invisivel
        yield return new WaitForSeconds(0.2f);
        GameObject morteTemp = Instantiate(fxMorte, pontoAnimMorte.position, transform.localRotation);//instancia a animacao de morte
        ativPainelPosMorte = true;
        yield return new WaitForSeconds(0.5f);
        Destroy(morteTemp);//destroi a animacao de morte
        this.gameObject.SetActive(false);//desativa o gameObject do player

    }
    IEnumerator habilitarNovoHit()
    {
        yield return new WaitForSeconds(5f);//depois de 5 segundos habilita novamente a possibilidade de se tomar hit's
        tomeiHit = false;
    }
    IEnumerator desabilitarHitEspinhoLateral()
    {
        this.gameObject.layer = 16;
        playerRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;//marca o freeze position x e o rotation z como true
        yield return new WaitForSeconds(10f);
        this.gameObject.layer = 14;
    }
    private void pararMovimentacao()
    {
         playerRB.velocity = new Vector2(0 , playerRB.velocity.y);
        
        playerAnimator.SetInteger("idAnimation", 0);
        mudarTagChao("comTagChao", parteFase);
        marcarFreezyX();
    }
    IEnumerator zerarVelocidadeAposSaltoL()
    {
       
       yield return new WaitForSeconds(0.6f);
        pararMovimentacao();

    }
    private void habilitaColisorAbaixado()//usado no bloco defender
    {
        collisorAbaixado.enabled = true;
        this.gameObject.tag = "invencivel";
        this.gameObject.layer =17;
        collisorEmPé.enabled = false;
    }
    private void habilitaColisorEmPe()//usado no bloco defender
    {
        this.gameObject.tag = "Player";
        this.gameObject.layer = 14;
        collisorAbaixado.enabled = false;
        collisorEmPé.enabled = true;
    }

    public void desmarcarFreezyX()
    {
        playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    public void marcarFreezyX()
    {
        playerRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }
    public void retirarVida()
    {
        _gameController.numVida-= 1;
    }
    
    //Funcao responsavel por destravar o player no momento em que este se encontra travado nas quinas das plataformas
    IEnumerator DestravarPlayer()
    {
        
        desmarcarFreezyX();
        yield return new WaitForFixedUpdate();
        groundedTravado = 0;
        this.GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        this.GetComponent<CircleCollider2D>().enabled = true;
    }
    public void zerarVelocidadeP()
    {
        playerRB.velocity = new Vector2(0, 0);
    }

    public void mudarTagChao(string tipoTag , int parteFase)
    {
        switch (tipoTag)
        {
            case "semTagChao":
                tileChao[parteFase].tag = "Untagged";
                break;
            case "comTagChao":
                tileChao[parteFase].tag = "chao";
                break;
        }
        ;
    }
    IEnumerator diminuirQTDBlocosU() // verificar se esta sendo usada e caso nao for entao apague
    {
        yield return new WaitForSeconds(1.5f);
        qtdBlocosUsados--;
    }

    public void mudarValidar()
    {
        validarConclusaoFase = true;
    }
    public void habilitarPerdaTentativa(string status)
    {
        switch (status)
        {
            case "true":
                _gameController.flagPerdiTentativa = true;
                
                break;
            case "false":
                interpreteAcabou = false;
                _gameController.flagPerdiTentativa=false;
                
                break;
        }
        
    }
    IEnumerator respostaInterprete()
    {
         yield return new WaitForSeconds(1.5f);
        interpreteAcabou = true;
    }

    public void receberBlocos( int qtdBlocoParte)
    {
        qtdBlocosCadaParteFase = qtdBlocoParte;
    }


    //BOTOÕES UTILIZADOS PARA TESTE
    public void startPuloFrente()//somente para testes , apagar depois
    {
        desmarcarFreezyX();
        
        StartCoroutine("PuloLateral");
        StartCoroutine("diminuirQTDBlocosU");
    }
    public void StartPuloSimples()//somente para testes , apagar depois
    {
        desmarcarFreezyX();
        StartCoroutine("PuloSimples");

    }
    public void StartDefender()
    {
        habilitarDesabilitarColliderEscudo(0);
        StartCoroutine("Defender");
    }
    public void StartAvancar()
    {
        if (!tomeiHit)
        {
            StartCoroutine("TESTEAvancar");
        }

    }
    
    public void StartAtaque()
    {
        StartCoroutine(Ataque(forcaDano));
    }
    IEnumerator TESTEAvancar()

    {//configurações da movimentação de avanço do player 
     
        desmarcarFreezyX();
        mudarTagChao("semTagChao", parteFase);
        StartCoroutine("Avancar");
        yield return new WaitForSeconds(0.6f);
        pararMovimentacao();
        

    } 
    public void StartVirar()
    {
        Flip();
    }

    void chamarResetarStatusParaPainelFaseInc()
    {
        StartCoroutine(resetarStatusParaPainelFaseInc());
    }
    IEnumerator resetarStatusParaPainelFaseInc()
    {
        //yield return new WaitForSeconds(1.6f);
        yield return null;
        interpreteAcabou = false;
        passeiParteFase = false;
    }

}
