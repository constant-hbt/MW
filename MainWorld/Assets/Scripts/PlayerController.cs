using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private         Animator        playerAnimator;
    private         Rigidbody2D     playerRB;
    private GameController _gameController;

    [Header("Configuração de vida(HUD)")]
    public int vidaAtual;

    [Header("Configuração de movimentação")]
    private         float           h;//variavel de movimento horizontal 
    public          float           speed; // velocidade de movimento do personagem
    private         float           v;//variavel de movimento vertical
    public          bool            atacando; //indica que o personagem esta atacando
    public          int             IdAnimation; //indica o id da animação
    private         bool            habilitarMovimentacaoPlayer =true;
    
    public          Collider2D      collisorEmPé;// Collisor em pé 
    public          Collider2D      collisorAbaixado;//Collisor abaixado
    
    public          bool            Grounded; //indica se o player esta no chao
    public          LayerMask       oqueEhChao; //indica o que é superficie para o teste do grounded
    public          Transform       groundCheck; //objeto responsavel por fazer a detecção para vermos se estamos tocando o chão
    
    public          float           jumpForceY = 15; //força aplicada no eixo y para gerar o pulo do personagem
    public          float           jumpForceX = 6;//força aplicada no eixo x para gerar o pulo do personagem
    
    public          bool            lookLeft;//indica se o personagem esta olhando para a esquerda
    private         float           x;//pega o scale.x do player

    
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
    public GameObject objetoInteração;

    [Header("Configuração de Ataque")]
    public PolygonCollider2D colliderAttack1;
    public PolygonCollider2D colliderAttack3;
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody2D>();
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;

        x = transform.localScale.x;

        //inicializa a vidaAtual do player com o limite maximo de vida ao iniciar a fase
        vidaAtual = _gameController.vidaMax;
    }

  
    private void FixedUpdate()
    {
        Grounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f, oqueEhChao);//esse teste so deve acontecer se houver uma colisao com a layer Ground

        //movimentar o personagem
        if (!habilitarMovimentacaoPlayer)
            return;
        playerRB.velocity = new Vector3(h * speed, playerRB.velocity.y);
        h = Input.GetAxisRaw("Horizontal");//capta a entra dos cursores seta direita e seta esquerda
    }
    void Update()
    {
       
        v = Input.GetAxis("Vertical");//capta a entra dos cursores seta cima e seta baixo

        if (h > 0 && lookLeft == true && atacando == false)
        {
            Flip();
        }
        else if (h < 0 && lookLeft == false && atacando == false)
        {
            Flip();
        }

        if (v < 0)
        {
            IdAnimation = 2;
            if (Grounded == true)
            {
                h = 0; //quando o personagem estiver em posição de defesa, ele não poderá se movimentar para frente
            }
        }else if (h != 0)
        {
            IdAnimation = 1;
        }
        else
        {
            IdAnimation = 0;
        }

        //inputs para movimentação
        if (Input.GetButtonDown("Fire1") && v >= 0 && atacando == false)
        {
            playerAnimator.SetTrigger("attack1");
        }
        if (Input.GetButtonDown("Fire2") && v >= 0 && atacando == false)
        {
            playerAnimator.SetTrigger("attack2");
        }
        if (Input.GetButtonDown("Fire3") && v >= 0 && atacando == false)
        {
            playerAnimator.SetTrigger("attack3");
        }
        if (Input.GetButtonDown("Jump") && Grounded == true)
        {//salto lateral
            habilitarMovimentacaoPlayer= false;
            playerRB.AddForce(new Vector2(jumpForceX * x,jumpForceY));
            StartCoroutine("ValidarMovimentoPlayer");
            
        }
        if(Input.GetKeyDown(KeyCode.V) && Grounded == true)
        {//pulo simples 
                
                playerRB.AddForce(new Vector2(0, jumpForceY));
              
            
        }
        

        if (atacando == true && Grounded == true)
        {
            h = 0;//personagem não poderá se mover enquanto estiver atacando
        }
        if (v < 0 && Grounded == true)
        {//habilita o collisor quando o personagem estiver abaixado
            collisorAbaixado.enabled = true;
            collisorEmPé.enabled = false;
        }
        else if (v >= 0 && Grounded == true)
        {//habilita o collisor quando o personagem estiver em pé
            collisorAbaixado.enabled = false;
            collisorEmPé.enabled = true;
        }
        else if (v != 0 && Grounded == false)
        {//arruma o collisor quando o personagem salta
            collisorAbaixado.enabled = false;
            collisorEmPé.enabled = true;
        }

        playerAnimator.SetBool("grounded", Grounded);
        playerAnimator.SetInteger("idAnimation", IdAnimation);
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
    
    
    IEnumerator ValidarMovimentoPlayer()
    {
        yield return new WaitForSeconds(0.8f);
        habilitarMovimentacaoPlayer = true;
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

    
}
