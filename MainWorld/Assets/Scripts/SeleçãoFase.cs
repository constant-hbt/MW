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

    //Integração com js da página
    [DllImport("__Internal")]
    private static extern void          SistemaDeEnableDisableBlocos(bool situacao);

    void Start()
    {
        SistemaDeEnableDisableBlocos(true);//quando o jogo estiver na tela inicial os blocos estarão desabilitados e não mostrar a mensagem com o restante dos blocos

        _controleDeFases = FindObjectOfType(typeof(ControleDeFases)) as ControleDeFases;
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;

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
        switch (id)
        {
            case 1:
                SceneManager.LoadScene("Fase1");
                _gameController.idFaseEmExecucao = id;
                _gameController.descricaoFase = "Fase1";
                break;
            case 2:
                SceneManager.LoadScene("Fase2");
                _gameController.idFaseEmExecucao = id;
                _gameController.descricaoFase = "Fase2";
                break;
            case 3:
                print("York");
                _gameController.idFaseEmExecucao = id;
                _gameController.descricaoFase = "Fase3";
                break;
            case 4:
                print("Wareham");
                _gameController.idFaseEmExecucao = id;
                _gameController.descricaoFase = "Fase4";
                break;
            case 5:
                print("Edington");
                _gameController.idFaseEmExecucao = id;
                _gameController.descricaoFase = "Fase5";
                break;
            case 6:
                print("Chippenham");
                _gameController.idFaseEmExecucao = id;
                _gameController.descricaoFase = "Fase6";
                break;
            case 7:
                print("Wantage");
                _gameController.idFaseEmExecucao = id;
                _gameController.descricaoFase = "Fase7";
                break;
            case 8:
                print(" Exeter");
                _gameController.idFaseEmExecucao = id;
                _gameController.descricaoFase = "Fase8";
                break;
            case 9:
                print("Wessex");
                _gameController.idFaseEmExecucao = id;
                _gameController.descricaoFase = "Fase9";
                break;
        }
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
