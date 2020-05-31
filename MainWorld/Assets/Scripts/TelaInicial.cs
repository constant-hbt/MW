using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Runtime.InteropServices;

public class TelaInicial : MonoBehaviour
{   
   

    private bool statusBotao = false; //true -> botão ja foi pressionado, false -> nao foi pressionado --- CONFIGURAÇÃO DO BOTAO CONFIG
    

    [Header("GameObject dos Botões de Configuração")]
    public GameObject botaoConfig;
    public GameObject botaoSom;
    public GameObject botaoControl;

    [Header("Paineis")]
    public GameObject painelSom;
    public GameObject painelControles;

    [Header("Painel Controles / Botões de Controle")]

    public GameObject[] btnsControles;//usada para ativar e desativar os botoes que contem as descrições do s blocos
    //usado para redimensionar o tamanho de cada obj que contem os botões, para assim adequar o scrollBar
    public RectTransform contentPersonagem;
    public RectTransform contentEstruturas;
    public RectTransform contentCond;
    //usada para ativar e ativar os objetos contendo cada grupo de botões
    public GameObject[] paineisDeControles;
    public ScrollRect scrollRect;

    [Header("Botões de Configuração")]
    
    public Button closePainelControle;
    public Button closePainelSom;
    public Button btnSom;
    public Button btnControle;
    public Button btnConfig;


    //Integração com js da página
    [DllImport("__Internal")]
    private static extern void SistemaDeEnableDisableBlocos(bool situacao);
    void Start()
    {
        SistemaDeEnableDisableBlocos(true);//quando o jogo estiver na tela inicial os blocos estarão desabilitados e não mostrar a mensagem com o restante dos blocos
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    

    public void startCoroutines(int id)
    {
        switch (id)
        {
            case 1:
                StartCoroutine("MudarCena");
                break;
            case 2:
                StartCoroutine("mostrarConfig");
                break;
        }
        
    }

    IEnumerator mostrarConfig()
    {
        if (statusBotao == false  )
        {

            statusBotao = true;

           // botaoConfig.transform.localScale += new Vector3(-0.2f, -0.2f, 0);
            yield return new WaitForSeconds(0.2f);

            botaoSom.SetActive(true);
            botaoControl.SetActive(true);
           
            
            botaoConfig.GetComponent<Animator>().SetBool("botaoAtivado", true);//deixa impossibilitado gerar a animacao ao passar o mouse no botao config, para nao conflitar com os outros botoes que foram ativados
            btnSom.Select();//quando os botoes de Controle e som aparece o som estará com o foco de seleção
        }
        else if (statusBotao == true)
        {
            statusBotao = false;
            //botaoConfig.transform.localScale += new Vector3(0.5f, 0.5f, 0);
            yield return new WaitForSeconds(0.2f);
            botaoSom.SetActive(false);
            botaoControl.SetActive(false);
            

            botaoConfig.GetComponent<Animator>().SetBool("botaoAtivado", false);//possibilita gerar a animacao ao passar o mouse no botao
            btnConfig.Select();
        }

    }

    IEnumerator MudarCena()
    {
        yield return new WaitForSeconds(0.42f);
        SceneManager.LoadScene("SelecaoFase");
    }
    
    //ativar e desativar botão de configurações de som
    public void ativarEdesativarPainel(string nomePainel)
    {//ativa e desativa os paineis de configuração

        switch (nomePainel)
        {
            case "Som":
                bool painelSomStatus = painelSom.activeSelf;
                painelSomStatus = !painelSomStatus;

                painelSom.SetActive(painelSomStatus);
                if (painelSomStatus == true)
                {
                    closePainelSom.Select();
                }
                else
                {
                   btnSom.Select();
                }

                break;
            case "Controle":
               
                bool painelControleStatus = painelControles.activeSelf;
                painelControleStatus = !painelControleStatus;

                painelControles.SetActive(painelControleStatus);
                if(painelControleStatus == true)
                {
                    closePainelControle.Select();
                }
                else
                {
                    btnControle.Select();
                }
               
                break;
        }
              
    }

    public void botoesControle(string nomeBotão)
    {//Painel de botões de movimentação do personagem (contentPersonagem) deve começar ativo
        switch (nomeBotão)
        {
            case "Personagem":
                                   
                                    //somente os botões referentes a movimentação do personagem estarão ativos
                                    foreach(GameObject i in btnsControles)
                                    {
                                        if(i.gameObject.tag == "UI.Control.Personagem")
                                         {
                                                i.SetActive(true);
                                        }
                                         else
                                             {
                                               i.SetActive(false);
                                            }
                                    }

               scrollRect.content = contentPersonagem;//modifica o content dentro do ScrollRect para redimensionar dinamicamente a barra de rolagem
                foreach (GameObject paineis in paineisDeControles)
                {
                    if(paineis.gameObject.tag == "UI.Painel.Personagem")
                    {
                        paineis.SetActive(true);
                    }
                    else
                    {
                        paineis.SetActive(false);
                    }
                }
                break;
            case "Estruturas":

                //somente os botões referentes as estruturas de comando estarão ativos
                foreach (GameObject i in btnsControles)
                {
                    if (i.gameObject.tag == "UI.Control.Estrutura")
                    {
                        i.SetActive(true);
                    }
                    else
                    {
                        i.SetActive(false);
                    }
                }
                scrollRect.content = contentEstruturas;//modifica o content dentro do ScrollRect para redimensionar dinamicamente a barra de rolagem

                foreach (GameObject paineis in paineisDeControles)
                {
                    if (paineis.gameObject.tag == "UI.Painel.Estrutura")
                    {
                        paineis.SetActive(true);
                    }
                    else
                    {
                        paineis.SetActive(false);
                    }
                }
                break;
            case "Condições":
                //somente os botões referentes aos blocos de condições estarão ativos
                foreach (GameObject i in btnsControles)
                {
                    if (i.gameObject.tag == "UI.Control.Cond")
                    {
                        i.SetActive(true);
                    }
                    else
                    {
                        i.SetActive(false);
                    }
                }

               scrollRect.content = contentCond;//modifica o content dentro do ScrollRect para redimensionar dinamicamente a barra de rolagem
                foreach (GameObject paineis in paineisDeControles)
                {
                    if (paineis.gameObject.tag == "UI.Painel.Cond")
                    {
                        paineis.SetActive(true);
                    }
                    else
                    {
                        paineis.SetActive(false);
                    }
                }
                break;
        }
    }

   
}
