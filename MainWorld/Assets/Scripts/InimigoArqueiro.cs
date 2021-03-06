﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InimigoArqueiro : MonoBehaviour
{
    

    public float velocidadeFlecha;
    public GameObject posicaoFlecha;
    public GameObject prefabFlecha;

    public bool comecarAtacar = false;
    //private bool novoAtaque = true;

    public Animator arqueiroAnimator;

    public Transform posicaoPlayer;
    void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        

      /*  if(comecarAtacar && novoAtaque)
        {

            atacar();
        }*/
    }

    public void atacar()
    {
        
       // novoAtaque = false;
        arqueiroAnimator.SetTrigger("Attack");
       // StartCoroutine(validarNovoAtaque());
    }


    public void instanciarFlecha()
    {
        GameObject fxTemp = Instantiate(prefabFlecha, posicaoFlecha.gameObject.transform.position, posicaoFlecha.gameObject.transform.localRotation);

        StartCoroutine(movimentoFlecha(fxTemp));

    }
    IEnumerator movimentoFlecha(GameObject flecha)
    {
        if(this.gameObject.transform.position.x < posicaoPlayer.position.x)
        {
            flecha.GetComponent<Rigidbody2D>().AddForce(new Vector2(velocidadeFlecha * 110, 0));
        }
        else
        {
            flecha.GetComponent<Rigidbody2D>().AddForce(new Vector2(velocidadeFlecha * -110, 0));
        }
        

        yield return new WaitForSeconds(2f);
        Destroy(flecha, 0.5f);

    }
    /*IEnumerator validarNovoAtaque()
    {
        yield return new WaitForSeconds(2.65f);//era 2.75 e a velocidade da flecha era 1
       // novoAtaque = true;
    }*/

    public void AtivarDesativarCollider(int situacao)
    {
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
       
            this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        
        
    }
    
    
    
}
