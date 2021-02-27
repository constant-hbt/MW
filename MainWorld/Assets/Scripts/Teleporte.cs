using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class Teleporte : MonoBehaviour
{
    /// <summary>
    /// RESPONSAVEL PELOS OBJETOS DE TELEPORTE DO PLAYER DE UMA PARTE DA FASE PARA OUTRA
    /// </summary>

    private         PlayerController            _playerController;// transform do player
    private         ControllerFase              _controllerFase;
    private GameController _gameController;
    private AudioController _audioController;

    public          Transform                   destino;
    public          Camera                      cam;
    public          Transform[]                 transicaoCamera;//posicao que a camera deve se encontrar ao mudar de uma parte da fase para outra
    public RectTransform objHud;
    public GameObject painelIntroChefe;
    //responsaveis por mudar a posicao do hud conforme para passando entre as partes da fase
    public float[] posHudParte ;//para definir essas medidas foi utilizado metodos manuais
    public bool virarPlayer = false; // caso o player for iniciar a fase olhando para o lado contrario , isso possibilita virá-lo para lado certo
   // int parteFase= 0;

   /* [Header("Configuração de Limite de blocos")]
    public          int                         blocosDisponiveis;
    */
    [DllImport("__Internal")]
    public static extern void            SistemaLimiteBloco(int qtdBlocoFase, int toolbox);

    [DllImport("__Internal")]
    public static extern void            SistemaReiniciarWorkspaceBlockly();


    [DllImport("__Internal")]
    public static extern void ReiniciarVarCodeCompleto();

    [DllImport("__Internal")]
    public static extern void SistemaDeEnableDisableBlocos(bool situacao);

    [DllImport("__Internal")]
    public static extern void CentralizarWebGl();
    void Start()
    {
        _playerController = FindObjectOfType(typeof(PlayerController)) as PlayerController;
        _controllerFase = FindObjectOfType(typeof(ControllerFase)) as ControllerFase;
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        _audioController = FindObjectOfType(typeof(AudioController)) as AudioController;
    }
    void Update()
    {
       
    }

    void interagindo()
    {
        if (_gameController.idFaseEmExecucao == 9 && _gameController.parteFaseAtual == 2)
        {
            _audioController.trocarMusica(_audioController.musicaFase9Parte2, _gameController.descricaoFase, false);

            HUD hud = FindObjectOfType(typeof(HUD)) as HUD;
         //     CentralizarWebGl();
         //      SistemaDeEnableDisableBlocos(true); //trava os blocos para poderem ser usados somente apos fechar o painel
            hud.habilitarObjVidaChefao();

            if (painelIntroChefe != null)
            {
                painelIntroChefe.gameObject.SetActive(true);
            }
        }

        int parteFaseAtual = 0;
       //   SistemaReiniciarWorkspaceBlockly();//Ao teleportar para outra etapa da fase reseta o espaco blockly
        for(int i=0; i<= _controllerFase.fases.Length; i++)
        {
            parteFaseAtual = i;
            if (!_controllerFase.fases[i].activeSelf && _playerController.parteFase == parteFaseAtual)
            {
                _controllerFase.fases[i].SetActive(true);
                break;
            }
            else
            {
                _controllerFase.fases[i].SetActive(false);
            }
        }
        _playerController.zerarVelocidadeP();//zera a velocidade do player
        _playerController.transform.position = destino.position;//muda a posição do player para o inicio da proxima parte da fase
        cam.transform.position = new Vector3( transicaoCamera[0].position.x, transicaoCamera[0].position.y, 0 );//muda a posição da câmera para a proxima parte da fase
        objHud.localPosition = new Vector4(posHudParte[0], 0, 0, 0);
      //  SistemaLimiteBloco(_controllerFase.qtdBlocosDisponiveis[_gameController.parteFaseAtual], _gameController.idFaseEmExecucao);//Ao teleportar para outra etapa da fase modifica o limite de blocos para aquela parte da fase

        _controllerFase.DadosFaseMemoria(); //salva os dados ja coletados na fase atual dentro de GameController
        _playerController.qtdBlocosUsados = -1;//necessário para não entrar no if que habilita o painel de fase incompleta , pois ao iniciar a próxima etapa o usuário necessitará de tempo ate dispor os blocos
        if (virarPlayer)
        {
            _playerController.Flip();   
        }
      //  ReiniciarVarCodeCompleto();
        
        
    }

    
}
