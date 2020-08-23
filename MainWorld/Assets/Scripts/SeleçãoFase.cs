using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class SeleçãoFase : MonoBehaviour
{

    /// <summary>
    /// RESPONSAVEL PELAS CONFIGURACOES DA CENA DE SELECAO DE FASE
    /// </summary>
    private         ControleDeFases         _controleDeFases;
    private         GameController          _gameController;
    private PainelEntrarFase _painelEntrarFase;
    public GameObject painelEntrarFase;

    [Header("Controle HUD")]

    //Barra de progresso de conclusão de fase
    public          GameObject          barraDeProgresso;
    public          TextMeshProUGUI     textoProgresso;
    public          TextMeshProUGUI     tmpFaseSelecionada;
    public          float               maxProgresso;//no caso como tem 9 fases esse é o maior progresso
    public          float               progressoAtual;

    //coins coletadas
    public          TextMeshProUGUI     goldText;
    public          TextMeshProUGUI     vidaText;
    public          TextMeshProUGUI     estrelaText;

    //TESTE PAINEL
    public Vector4 posicaoPainelEF;
    public RectTransform rectCanvasEntrarFase;

    //Integração com js da página
    [DllImport("__Internal")]
    private static extern void          SistemaDeEnableDisableBlocos(bool situacao);

    void Start()
    {
       SistemaDeEnableDisableBlocos(true);//quando o jogo estiver na tela inicial os blocos estarão desabilitados e não mostrar a mensagem com o restante dos blocos

        _controleDeFases = FindObjectOfType(typeof(ControleDeFases)) as ControleDeFases;
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        _painelEntrarFase = FindObjectOfType(typeof(PainelEntrarFase)) as PainelEntrarFase;
        //zera estas variaveis pois no momento que esta cena estiver ativa não haverá nenhuma fase em execucao
        _gameController.descricaoFase = "";
        _gameController.idFaseEmExecucao = 0;
    }

    
    void Update()
    {
        progressoAtual = _gameController.fasesConcluidas;
        barraDeProgresso.transform.localScale = new Vector3(pegarTamanhoBarra(progressoAtual, maxProgresso), barraDeProgresso.transform.localScale.y, barraDeProgresso.transform.localScale.z);
        textoProgresso.text = progressoAtual+"/"+maxProgresso;
        /*
        if(progressoAtual < maxProgresso)
        {
            progressoAtual += Time.deltaTime * 5;
        }
        else                                                            PARA FAZER A BARRA DE PROGRESSO NA TRANSIÇÃO ENTRE FASES
        {
            textoProgresso.text = "100% completo";
        }*/

        //Mostra a quantidade de gold coletada no HUD
        string s = _gameController.numGold.ToString("N0");
        goldText.text = s.Replace(",", ".");
        vidaText.text = _gameController.numVida.ToString();
        estrelaText.text = _gameController.numEstrelas.ToString();
    }

    public void nomeFase(int id)
    {//responsavel por chamar as fases

        if (_painelEntrarFase == null)
        {
            _painelEntrarFase = FindObjectOfType(typeof(PainelEntrarFase)) as PainelEntrarFase;
        }

        switch (id)
            {
                case 1:
                //SceneManager.LoadScene("Fase1");
                rectCanvasEntrarFase.localPosition = new Vector4(-309f, -111, 3, 4);//posX = 1, posY = 2, Height = 3 , Width = 4;
                _painelEntrarFase.idBotaoFaseSelecionado = id;
                    _gameController.idFaseEmExecucao = id;
                    _gameController.descricaoFase = "Fase1";

                _painelEntrarFase.chamar();
                    break;
                case 2:
                //    SceneManager.LoadScene("Fase2");
                rectCanvasEntrarFase.localPosition = new Vector4(-289, 19, 3, 4);
                _painelEntrarFase.idBotaoFaseSelecionado = id;
                     _gameController.idFaseEmExecucao = id;
                    _gameController.descricaoFase = "Fase2";
                _painelEntrarFase.chamar();
                break;
                case 3:
                rectCanvasEntrarFase.localPosition = new Vector4(-178, -47, 3, 4);
                _painelEntrarFase.idBotaoFaseSelecionado = id;
                    _gameController.idFaseEmExecucao = id;
                    _gameController.descricaoFase = "Fase3";
                _painelEntrarFase.chamar();
                break;
                case 4:
                rectCanvasEntrarFase.localPosition = new Vector4(-110, 206, 3, 4);
                _painelEntrarFase.idBotaoFaseSelecionado = id;
                     _gameController.idFaseEmExecucao = id;
                    _gameController.descricaoFase = "Fase4";
                _painelEntrarFase.chamar();
                break;
                case 5:
                rectCanvasEntrarFase.localPosition = new Vector4(-82, 95, 3, 4);
                _painelEntrarFase.idBotaoFaseSelecionado = id;
                    _gameController.idFaseEmExecucao = id;
                    _gameController.descricaoFase = "Fase5";
                _painelEntrarFase.chamar();
                break;
                case 6:
                rectCanvasEntrarFase.localPosition = new Vector4(155, 201, 3, 4);
                _painelEntrarFase.idBotaoFaseSelecionado = id;
                    _gameController.idFaseEmExecucao = id;
                    _gameController.descricaoFase = "Fase6";
                _painelEntrarFase.chamar();
                break;
                case 7:
                rectCanvasEntrarFase.localPosition = new Vector4(358, 97, 3, 4);
                _painelEntrarFase.idBotaoFaseSelecionado = id;
                    _gameController.idFaseEmExecucao = id;
                    _gameController.descricaoFase = "Fase7";
                _painelEntrarFase.chamar();
                break;
                case 8:
                rectCanvasEntrarFase.localPosition = new Vector4(-38, -64, 3, 4);
                _painelEntrarFase.idBotaoFaseSelecionado = id;
                    _gameController.idFaseEmExecucao = id;
                    _gameController.descricaoFase = "Fase8";
                _painelEntrarFase.chamar();
                break;
                case 9:
                rectCanvasEntrarFase.localPosition = new Vector4(348, -148, 3, 4);
                _painelEntrarFase.idBotaoFaseSelecionado = id;
                    _gameController.idFaseEmExecucao = id;
                    _gameController.descricaoFase = "Fase9";
                _painelEntrarFase.chamar();
                break;
            }

    }

    public void ativarPainelEntrarFase(int idFase)
    {
        

        if (painelEntrarFase.activeSelf == false || painelEntrarFase.activeSelf == true && _gameController.idFaseEmExecucao != idFase)
        {
            StartCoroutine(abrirPainel(idFase));

        }
        else if(_gameController.idFaseEmExecucao == idFase)
        {
            painelEntrarFase.SetActive(false);
        }
        
        
    }
    IEnumerator abrirPainel(int idFase)
    {
        yield return new WaitForSeconds(0.3f);
        painelEntrarFase.SetActive(true);
        nomeFase(idFase);
    }


    public float pegarTamanhoBarra(float minValor , float maxValor)
    {
        return minValor / maxValor;
    }

    public int pegarPorcentagemBarra(float minValor , float maxValor , int fator)
    {
        return Mathf.RoundToInt( pegarTamanhoBarra(minValor, maxValor) * fator);
    }

    public void nomeFaseSelecionada(string nomeFase)
    {
        tmpFaseSelecionada.text = nomeFase;
    }



}
