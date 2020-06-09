using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
public class PlayerController : MonoBehaviour
{
    private         Animator        playerAnimator;
    private         Rigidbody2D     playerRB;
    private GameController _gameController;
    private ControllerFase _controllerFase;
    /*
    [Header("Configuração de vida(HUD)")]
    public int vidaAtual = 3;
    */
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
    
    [Header("Configuração de Interação com inimigos")]
   
    private Vector3 cabecaScan  = Vector3.up;
    private Vector3 ombroScan = new Vector3(0.136f, 0.143f, 0);
    private Vector3 toraxScan = Vector3.right;
    private Vector3 cinturaScan = Vector3.right;
    private Vector3 joelhoScan = Vector3.right;
    private Vector3 peScan = Vector3.right;

    public Transform scanRayCabeca;
    public Transform scanRayOmbro;
    public Transform scanRayTorax;
    public Transform scanRayCintura;
    public Transform scanRayJoelho;
    public Transform scanRayPe;
    
    public LayerMask interacao;
    public LayerMask interacaoTeleporte;
    public GameObject objetoInteração;

    [Header("Configuração de Ataque")]
    public PolygonCollider2D colliderAttack1;
    public PolygonCollider2D colliderAttack3;

    //teste
    public Transform playerTransform;


    // testando
    [DllImport("__Internal")]
    public static extern void Win();

    [DllImport("__Internal")]
    public static extern void Teste(int qtdB, float veloX , float veloY , bool grounded);


    //teste para verificar se o player concluiu a fase ou não
    public int qtdBlocosUsados = -1; // quantidade de blocos usados na fase
    private int qtdBlocosCadaParteFase = 0;
    private bool passeiFase ;
    public GameObject painelFaseIncompleta;
    private bool validarConclusaoFase = false;
    private bool interpreteAcabou = false;

    private int parteFase;

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
            if (qtdBlocosCadaParteFase == 0)//garante que nao entrara no if abaixo quando nao tiver blocos na area de trabalho
            {
                qtdBlocosUsados = -1;
            }
            else
            {
                qtdBlocosUsados = qtdBlocosCadaParteFase;
                validarConclusaoFase = false;
            }
        }

        if (interpreteAcabou && qtdBlocosUsados == 0 && Grounded && playerRB.velocity.x == 0 && playerRB.velocity.y == 0 && !passeiFase)
        {
            Teste(_controllerFase.qtdBlocosUsados, playerRB.velocity.x, playerRB.velocity.y, Grounded);
            print("entrei");
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
        
    }
    
    //CONTROLE DE COLISÃO
    private void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "inimigo":
                print("Colidi com um inimigo");
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
                print("Estou colidindo com o chao");
                playerRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                break;
        }
    }


    void Flip()
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

        RaycastHit2D scanTeleporte = Physics2D.Raycast(scanRayCintura.transform.position, cinturaScan, 0.4f, interacaoTeleporte /*faz com que teste a colisao somente em objetos que tiverem essa layer*/);
        Debug.DrawRay(scanRayCintura.transform.position, cinturaScan * 0.4f, Color.blue);

        if(scanTeleporte == true)
        {
           
            objetoInteração = scanTeleporte.collider.gameObject;
        }

        if (scanCabeca == true || scanOmbro == true || scanTorax == true || scanCintura == true)
        {
                playerAnimator.SetTrigger("attack1");

        }else if(scanJoelho == true || scanPe == true)
        {
            playerAnimator.SetTrigger("attack3");
        }
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
    

    public void startAvancar()//somente para testes , apagar depois
    {
        desmarcarFreezyX();
        StartCoroutine("PuloLateral");
        StartCoroutine("diminuirQTD");
    }
    public void StartDefender()
    {
        desmarcarFreezyX();
        mudarTagChao("semTagChao" , parteFase);
        StartCoroutine("Avancar");
        
        StartCoroutine("zerarVelocidadeAposSaltoL");
        
    }
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
            case "atacar":
                StartCoroutine("Attack1");
                yield return new WaitForSeconds(0.2f);
                break;
        }
    }
    IEnumerator Avancar()
    {//configurações da movimentação de avanço do player -- OK , falta ajustar a distancia
     
        playerRB.velocity = new Vector2(playerRB.velocity.x + ( 0.8f * speed), playerRB.velocity.y);
        playerAnimator.SetInteger("idAnimation", 1);
        passeiFase = false;
        StartCoroutine("diminuirQTD");
        yield return null;
    }
    IEnumerator PuloSimples()//Ok , falta ajustar a altura do salto e a distancia
    {
        if (Grounded == true)
        {
            playerRB.AddForce(new Vector2(0, jumpForceY));
        }
        StartCoroutine("diminuirQTD");
        yield return null;
    }
    IEnumerator PuloLateral()//ok , falta ajustar a altura
    {
      
        if (Grounded == true)
        {
            playerRB.AddForce(new Vector2(jumpForceX * x,jumpForceY));
        }
        // StartCoroutine("zerarVelocidadeAposSaltoL");
        StartCoroutine("diminuirQTD");
        yield return null;
    }
    IEnumerator Defender()//ok
    {
        if (Grounded == true)
        {

            habilitaColisorAbaixado();
            playerAnimator.SetInteger("idAnimation", 2);
        }
        yield return new WaitForSeconds(0.6f);
        playerAnimator.SetInteger("idAnimation", 0);
       
        habilitaColisorEmPe();
        StartCoroutine("diminuirQTD");
    }
    IEnumerator Attack1()//ok
    {
        playerAnimator.SetTrigger("attack1");
        StartCoroutine("diminuirQTD");
        yield return null;
    }
    IEnumerator Attack3()//ok
    {
        playerAnimator.SetTrigger("attack3");
        StartCoroutine("diminuirQTD");
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
    private void habilitaColisorAbaixado()
    {
        collisorAbaixado.enabled = true;
        collisorEmPé.enabled = false;
    }
    private void habilitaColisorEmPe()
    {
        collisorAbaixado.enabled = false;
        collisorEmPé.enabled = true;
    }

    //Zerar velocidade do player
    public void marcarFreezyX()
    {
        playerRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
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
    IEnumerator diminuirQTD()
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
}
