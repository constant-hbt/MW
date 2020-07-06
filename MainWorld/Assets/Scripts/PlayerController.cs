using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
public class PlayerController : MonoBehaviour
{
    private         Animator        playerAnimator;
    private         Rigidbody2D     playerRB;
    private         GameController  _gameController;
    private         ControllerFase  _controllerFase;
    
    [Header("Configuração de movimentação")] 
    public          float           speed; // velocidade de movimento do personagem
    public          bool            atacando; //indica que o personagem esta atacando
    public          int             IdAnimation; //indica o id da animação
    
    public          Collider2D      collisorEmPé;// Collisor em pé 
    public          Collider2D      collisorAbaixado;//Collisor abaixado
    
    public          bool            Grounded; //indica se o player esta no chao
    public          LayerMask       oqueEhChao; //indica o que é superficie para o teste do grounded
    public          Transform       groundCheck; //objeto responsavel por fazer a detecção para vermos se estamos tocando o chão
    
    public          float           jumpForceY = 15; //força aplicada no eixo y para gerar o pulo do personagem
    public          float           jumpForceX = 6;//força aplicada no eixo x para gerar o pulo do personagem
    
    public          bool            lookLeft;//indica se o personagem esta olhando para a esquerda
    private         float           x;//pega o scale.x do player


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

    public          LayerMask       interacao;//define a Layer em que o RayCast deve verificar para ver se há colisão com inimigo
    public          LayerMask       interacaoTeleporte;//define a Layer que o RayCast deve verificar se há colisão com algum objeto teleporte
    public          GameObject      objetoInteração;//pega o GameObject do objeto que o RayCast estiver interagindo

    [Header("Configuração de Ataque")]
    public          PolygonCollider2D colliderAttack1;//colisor da arma usado na animação de ataque1, que só é ativado em um momento especifico da animação de ataque
    public          PolygonCollider2D colliderAttack3;//colisor da arma usado na animação de ataque3, que só é ativado em um momento específico da animação

    [Header("Sistema de Configuração de fase")]
    public          GameObject      painelFaseIncompleta;//gameObject do obj painelFaseIncompleta
    public          int             qtdBlocosUsados = -1; // quantidade de blocos usados na fase
    private         int             qtdBlocosCadaParteFase = 0;// quantidade de blocos usados em cada parte da fase(necessario para fases com mais de uma etapa)
    private         bool            passeiFase ;// verifica se o usuário concluiu a de fase
    private         bool            validarConclusaoFase = false;//verifica se a fase foi concluida ou não
    private         bool            interpreteAcabou = false;//verifica se o interprete js do blockly terminou
    private         int             parteFase;//denomina em que parte da fase o personagem está


    //TESTE  HA INIMIGO
    [DllImport("__Internal")]
    public static extern void CondicaoHaInimigo(bool temp_situacaoInimigo);
   
    [DllImport("__Internal")]
    public static extern void CondicaoNaoHaInimigo(bool temp_situacaoInimigo);

    [DllImport("__Internal")]
    public static extern void Teste(bool condInim);



    private bool colidiComInimigo = false;
    private bool rayCast_ColidindoInimigo = false;
    private bool rayCast_NaoColidindoInimigo = true;
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody2D>();
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        _controllerFase = FindObjectOfType(typeof(ControllerFase)) as ControllerFase;

        x = transform.localScale.x;

        passeiFase = false;
        parteFase = 0;
    }

  
    private void FixedUpdate()
    {
        Grounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f, oqueEhChao);//esse teste so deve acontecer se houver uma colisao com a layer Ground


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
        if (interpreteAcabou /*&& qtdBlocosUsados == 0*/ && Grounded && playerRB.velocity.x == 0 && playerRB.velocity.y == 0 && !passeiFase)
        {
            //
            painelFaseIncompleta.SetActive(true);
            this.retirarVida();
            qtdBlocosUsados = -1;
            interpreteAcabou = false;
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
            case "inimigo":
                print("Colidi com um inimigo");//somente utilizado para testes
                break;
            case "teleporte":
                zerarVelocidadeP();//zero a velocidade do player para ele iniciar a nova etapa da fase sem estar se movimentando
                col.gameObject.SendMessage("interagindo", SendMessageOptions.DontRequireReceiver);
                parteFase += 1;
                break;
            case "Win":
                this.passeiFase = true;
                qtdBlocosUsados = -1;//resetar a var e nao deixar entrar no if que ativa o painel de fase incompleta
                zerarVelocidadeP();//zero a velocidade do player para ele iniciar a nova etapa da fase sem estar se movimentando
                col.gameObject.SendMessage("ativarPainel", SendMessageOptions.DontRequireReceiver);
                break;
            case "coletavel":
                col.gameObject.SendMessage("coletar", SendMessageOptions.DontRequireReceiver);
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
        }
    }


    void Flip()//será utilizado futuramente
    {
        lookLeft = !lookLeft; // inverte o valor da var bool
        x *= -1; // inverte o sinal do scale x

        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);

        cabecaScan.x = cabecaScan.x * -1;
        ombroScan.x = ombroScan.x * -1;
        toraxScan.x = toraxScan.x * -1;
        cinturaScan.x = cinturaScan.x * -1;
        joelhoScan.x = joelhoScan.x * -1;
        peScan.x = peScan.x * -1;

    }
    void atack(int atk)//não utilizado ainda
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
        /*
        if (scanCabeca == true || scanOmbro == true || scanTorax == true || scanCintura == true)
        {
            playerAnimator.SetTrigger("attack1");

        }
        else if (scanJoelho == true || scanPe == true)
        {
            playerAnimator.SetTrigger("attack3");
        }*/
        if (scanCabeca == true || scanOmbro == true || scanTorax == true || scanCintura == true || scanJoelho == true || scanPe == true)
        {
            testeColisaoInimigo = true;
            testeNaoColidindoInimigo = false;
            //colidiComInimigo = true;
            // Teste(true);
            //  CondicaoInimigo(true);
        }
        else
        {
            testeColisaoInimigo = false;
            testeNaoColidindoInimigo = true;
            // CondicaoInimigo(false);
            // Debug.Log("RayCast nao ta achando o inimigo");
        }

        if (rayCast_ColidindoInimigo != testeColisaoInimigo)
        {
            // Teste(rayCast_ColidindoInimigo);
            Debug.Log("Entrei dentro do interagirInimigo, estou dentro do if, e estou enviado rayCast_ColidindoInimigo = " + rayCast_ColidindoInimigo);
            rayCast_ColidindoInimigo = testeColisaoInimigo;
            CondicaoHaInimigo(rayCast_ColidindoInimigo);
        }
        if(rayCast_NaoColidindoInimigo != testeNaoColidindoInimigo)
        {
            Debug.Log("Entrei dentro do interagirInimigo, estou dentro do if, e estou enviado rayCast_NaoColidindoInimigo = " + rayCast_NaoColidindoInimigo);
            rayCast_NaoColidindoInimigo = testeNaoColidindoInimigo;
            CondicaoNaoHaInimigo(rayCast_NaoColidindoInimigo);   
        }
    }

    public void retornoFuncHaInimigo()
    {//quando eu terminar de executar o bloco ha inimigos reinicio a var colidiInimigo para ver se a mais inimigos perto do player
        colidiComInimigo = false;
    }

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
                yield return new WaitForSeconds(0.6f);
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
                StartCoroutine("Defender");
                yield return new WaitForSeconds(0.2f);
                break;
            /*case "atacar":
                StartCoroutine("Ataque" , valor_ataque);
                yield return new WaitForSeconds(0.2f);
                break;*/
        }
    }
    IEnumerator Avancar()
    {//configurações da movimentação de avanço do player 
     
        playerRB.velocity = new Vector2(playerRB.velocity.x + ( 0.8f * speed), playerRB.velocity.y);
        playerAnimator.SetInteger("idAnimation", 1);
        passeiFase = false;
        StartCoroutine("diminuirQTDBlocosU");
        yield return null;
    }
    IEnumerator PuloSimples()
    {
        if (Grounded == true)
        {
            playerRB.AddForce(new Vector2(0, jumpForceY));
        }
        StartCoroutine("diminuirQTDBlocosU");
        yield return null;
    }
    IEnumerator PuloLateral()//ok , falta ajustar a altura
    {
      
        if (Grounded == true)
        {
            playerRB.AddForce(new Vector2(jumpForceX * x,jumpForceY));
        }
        // StartCoroutine("zerarVelocidadeAposSaltoL");
        StartCoroutine("diminuirQTDBlocosU");
        yield return null;
    }
    IEnumerator Defender()//ok
    {
        if (Grounded == true)
        {

            habilitaColisorAbaixado();
            playerAnimator.SetInteger("idAnimation", 2);
        }
        yield return new WaitForSeconds(2f);
        playerAnimator.SetInteger("idAnimation", 0);
       
        habilitaColisorEmPe();
        StartCoroutine("diminuirQTDBlocosU");
    }
    IEnumerator Ataque(int valor_ataque)//ok
    {
        Debug.Log("Meu ataque tem o valor de = " + valor_ataque);
        playerAnimator.SetTrigger("attack1");
        StartCoroutine("diminuirQTDBlocosU");
        yield return new WaitForSeconds(0.2f);
    }
    IEnumerator Attack3()//ok
    {
        playerAnimator.SetTrigger("attack3");
        StartCoroutine("diminuirQTDBlocosU");
        yield return null;
    }
    //---------------------------------------------------------------------------------
    private void pararMovimentacao()
    {
         playerRB.velocity = new Vector2(0 , playerRB.velocity.y);
        
        playerAnimator.SetInteger("idAnimation", 0);
        mudarTagChao("comTagChao", parteFase);
        
    }
    IEnumerator zerarVelocidadeAposSaltoL()
    {
       
       yield return new WaitForSeconds(0.6f);
        pararMovimentacao();

    }
    private void habilitaColisorAbaixado()//usado no bloco defender
    {
        collisorAbaixado.enabled = true;
        collisorEmPé.enabled = false;
    }
    private void habilitaColisorEmPe()//usado no bloco defender
    {
        collisorAbaixado.enabled = false;
        collisorEmPé.enabled = true;
    }

    public void desmarcarFreezyX()
    {
        playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    public void retirarVida()
    {
        _gameController.numVida-= 1;
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
    IEnumerator diminuirQTDBlocosU()
    {
        yield return new WaitForSeconds(1.5f);
        qtdBlocosUsados--;
    }

    public void mudarValidar()
    {
        validarConclusaoFase = true;
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
    public void startAvancar()//somente para testes , apagar depois
    {
        desmarcarFreezyX();
        StartCoroutine("PuloLateral");
        StartCoroutine("diminuirQTDBlocosU");
    }
    public void StartDefender()//somente para testes , apagar depois
    {
        desmarcarFreezyX();
        mudarTagChao("semTagChao", parteFase);
        StartCoroutine("Avancar");

        StartCoroutine("zerarVelocidadeAposSaltoL");

    }
}
