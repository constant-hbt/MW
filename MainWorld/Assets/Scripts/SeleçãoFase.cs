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

    [DllImport("__Internal")]
    public static extern void AlterarToolboxFases(int idFase);

    void Start()
    {
      //  SistemaDeEnableDisableBlocos(true);//quando o jogo estiver na tela inicial os blocos estarão desabilitados e não mostrar a mensagem com o restante dos blocos

        _controleDeFases = FindObjectOfType(typeof(ControleDeFases)) as ControleDeFases;
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        //zera estas variaveis pois no momento que esta cena estiver ativa não haverá nenhuma fase em execucao
        _gameController.descricaoFase = "";
        _gameController.idFaseEmExecucao = 0;
        _gameController.tentativaFaseAlter = false;
        _gameController.numTentativasFixo = 0;
        _gameController.numTentativasFase = 0;
        _gameController.ZerarVarBancoTentativasFase();
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
                
                   _gameController.idFaseEmExecucao = id;
                   _gameController.descricaoFase = "Fase1";
                SceneManager.LoadScene("TelaCarregamento");
                
                    break;
                case 2:
                
                _gameController.idFaseEmExecucao = id;
                    _gameController.descricaoFase = "Fase2";
                SceneManager.LoadScene("TelaCarregamento");
                
                break;
                case 3:
                
                _gameController.idFaseEmExecucao = id;
                _gameController.descricaoFase = "Fase3";
                SceneManager.LoadScene("TelaCarregamento");
                break;
                case 4:
                
                _gameController.idFaseEmExecucao = id;
                _gameController.descricaoFase = "Fase4";
                SceneManager.LoadScene("TelaCarregamento");
                
                break;
                case 5:
               
                _gameController.idFaseEmExecucao = id;
                _gameController.descricaoFase = "Fase5";
               SceneManager.LoadScene("TelaCarregamento");
                
                break;
                case 6:
               
                _gameController.idFaseEmExecucao = id;
                _gameController.descricaoFase = "Fase6";
                SceneManager.LoadScene("TelaCarregamento");
                
                break;
                case 7:
                
                _gameController.idFaseEmExecucao = id;
                _gameController.descricaoFase = "Fase7";
                SceneManager.LoadScene("TelaCarregamento");
                
                break;
                case 8:
                
                _gameController.idFaseEmExecucao = id;
                _gameController.descricaoFase = "Fase8";
                SceneManager.LoadScene("TelaCarregamento");
                
                break;
                case 9:
               
                _gameController.idFaseEmExecucao = id;
                _gameController.descricaoFase = "Fase9";
                SceneManager.LoadScene("TelaCarregamento");
                
                break;
            }
       // AlterarToolboxFases(id);
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
