using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator playerAnimator;
    private Rigidbody2D playerRB;
    private SpriteRenderer sRender;

    [Header("Configuração de movimentação")]
    private float h;//variaveil de movimento horizontal 
    private float v;//variaveil de movimento vertical
    public Collider2D collisorEmPé;// Collisor em pé 
    public Collider2D collisorAbaixado;//Collisor abaixado
    public bool Grounded; //indica se o player esta no chao
    public LayerMask oqueEhChao; //indica o que é superficie para o teste do grounded
    public Transform groundCheck; //objeto responsavel por fazer a detecção para vermos se estamos tocando o chão
    public float jumpForce; //força aplicada para gerar o pulo do personagem
    public float speed; // velocidade de movimento do personagem
    public bool lookLeft;//indica se o personagem esta olhando para a esquerda
    public bool atacando; //indica que o personagem esta atacando
    public int IdAnimation; //indica o id da animação
    private float x;//pega o scale.x do player
    public bool validarMovimentoX =false;
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody2D>();
        sRender = GetComponent<SpriteRenderer>();
        x = transform.localScale.x;
    }

    private void FixedUpdate()
    {
        Grounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f, oqueEhChao);//esse teste so deve acontecer se houver uma colisao com a layer Ground

        //movimentar o personagem
        if (!validarMovimentoX)
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
        }
        else if (h != 0)
        {
            IdAnimation = 1;
        }
        else
        {
            IdAnimation = 0;
        }


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
        {
            validarMovimentoX = false;
            /*
            if(lookLeft == true && transform.localScale.x > 0)
            {
                Flip();
            }else if(lookLeft == false && transform.localScale.x < 0)
            {
                Flip();
            }*/

            playerRB.AddForce(new Vector2(6 * x,15));
            StartCoroutine("testando");
            
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

    }



    void Flip()
    {
        lookLeft = !lookLeft; // inverte o valor da var bool
        

        x *= -1; // inverte o sinal do scale x

        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);


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
    
    
    IEnumerator testando()
    {
        yield return new WaitForSeconds(0.8f);
        validarMovimentoX = true;
    }
}
