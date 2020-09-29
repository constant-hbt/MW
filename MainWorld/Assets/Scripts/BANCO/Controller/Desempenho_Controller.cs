using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class Desempenho_Controller : MonoBehaviour
{
    private GameController _gameController;
    private ControllerFase _controllerFase;

    string WEB_URL = "http://localhost:3000";//servico ->> 51514 casa ->>>> 49478
    string rota = "/novoDesempenho";
    string rotaAtt = "/atualizaDesempenho";
    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        _controllerFase = FindObjectOfType(typeof(ControllerFase)) as ControllerFase;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EnviarRegistroDesempenho()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        _controllerFase = FindObjectOfType(typeof(ControllerFase)) as ControllerFase;

        Debug.Log("Entrei na funcao enviarRegistroDesempenho()");

        Debug.Log("Erro aqui " + _controllerFase.data_InicioFase.ToString());

        FaseConcluida objFaseConcluida = new FaseConcluida();
        objFaseConcluida.Data_inicio = _controllerFase.data_InicioFase;
        objFaseConcluida.Data_fim = _controllerFase.data_FimFase;
        objFaseConcluida.QtdErros = _gameController.errosFase[_gameController.idFaseEmExecucao - 1];
        objFaseConcluida.QtdEstrelas = _controllerFase.estrelas;
        objFaseConcluida.QtdMoedas = _controllerFase.qtdMoedasColetadas;

        Desempenho objDesempenho = new Desempenho();
        objDesempenho.IdGame = _gameController.idGame;
        objDesempenho.DescricaoFase = _gameController.descricaoFase;
        objDesempenho.Id_Desempenho = _gameController.id_Desempenho;
        objDesempenho.Moedas = _gameController.numGold + _controllerFase.qtdMoedasColetadas;
        objDesempenho.Estrelas = _gameController.numEstrelas + _controllerFase.estrelas;
        objDesempenho.Vidas = _gameController.numVida;
        objDesempenho.Ultima_fase_concluida = _gameController.idFaseEmExecucao;
        objDesempenho.Id_usuario_ativ_turma = _gameController.id_usuario_ativ_turma;
        objDesempenho.Fase_concluida = objFaseConcluida;


        Debug.Log("Enviei o obj desempenho, este é o valor do id_game = " + objDesempenho.idGame);
        SendRestPostRegistrarDesemepenho(objDesempenho);

       
        _gameController.zerarVarBanco();
    }

    public void SendRestPostRegistrarDesemepenho(Desempenho desempenho)
    {

       if(desempenho.Id_Desempenho == 0)
        {
            StartCoroutine(RegistrarDesempenho(WEB_URL, rota, desempenho));
        }
        else
        {
            StartCoroutine(AtualizarDesempenho(WEB_URL, rotaAtt, desempenho));
        }
        
    }

    IEnumerator RegistrarDesempenho(string url, string rota, Desempenho p_desempenho)
    {
        string urlNew = string.Format("{0}{1}", url, rota);//concatena a rota com a url
        Debug.Log("Nova url "+urlNew); 
        string jsonData = JsonUtility.ToJson(p_desempenho);
        Debug.Log("Mandei este jsonData =" + jsonData);

        using (UnityWebRequest www = UnityWebRequest.Post(urlNew, jsonData))
        {

            www.SetRequestHeader("content-type", "application/json");
            www.uploadHandler.contentType = "application/json";
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));//transforma o conteudo json em bytes para realizar o envio

            yield return www.SendWebRequest();//faz o envio da requisição

            //processa e trabalha com o retorno da requisição
            if (www.isNetworkError)
            {
               // Mensagem mensagem_erro = new Mensagem((int)www.responseCode, www.error);//responseCode = numero de status, error = mensagem de erro
                Debug.Log(www.error);
               // callback(mensagem_erro);
            }
            else
            {
                if (www.isDone)
                {
                    string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);//downloadHandler.data manda a informacao vinda do servidor(banco)
                    
                    Desempenho retorno_Desempenho = JsonUtility.FromJson<Desempenho>(jsonResult);//crio uma var do tipo mensagem e preencho com o json retornado do servidor
                    _gameController.id_Desempenho = retorno_Desempenho.Id_Desempenho;
                    
                    // Debug.Log("Retorno = " + retorno_Desempenho.Id_Desempenho);
                    // Mensagem mensagem_rest = JsonUtility.FromJson<Mensagem>(jsonResult);//crio uma var do tipo mensagem e preencho com o json retornado do servidor
                    // callback(mensagem_rest);//preencho a callback com a informação retornada do servidor

                }
            }
        }

        yield return null;
    }

    IEnumerator AtualizarDesempenho(string url, string rota, Desempenho p_desempenho)
    {
        string urlNew = string.Format("{0}{1}", url, rota);//concatena a rota com a url
        Debug.Log("Nova url " + urlNew);
        string jsonData = JsonUtility.ToJson(p_desempenho);
        Debug.Log("Mandei este jsonData =" + jsonData);

        using (UnityWebRequest www = UnityWebRequest.Put(urlNew, jsonData))
        {

            www.SetRequestHeader("content-type", "application/json");
            www.uploadHandler.contentType = "application/json";
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));//transforma o conteudo json em bytes para realizar o envio

            yield return www.SendWebRequest();//faz o envio da requisição

            //processa e trabalha com o retorno da requisição
            if (www.isNetworkError)
            {
                // Mensagem mensagem_erro = new Mensagem((int)www.responseCode, www.error);//responseCode = numero de status, error = mensagem de erro
                Debug.Log(www.error);
                // callback(mensagem_erro);
            }
            else
            {
                if (www.isDone)
                {
                    string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);//downloadHandler.data manda a informacao vinda do servidor(banco)
                    Debug.Log(jsonResult);
                 
                    // Mensagem mensagem_rest = JsonUtility.FromJson<Mensagem>(jsonResult);//crio uma var do tipo mensagem e preencho com o json retornado do servidor
                    // callback(mensagem_rest);//preencho a callback com a informação retornada do servidor

                }
            }
        }

        yield return null;
    }


    //funcao que preenche os objetos Desempenho e faseConcluida , para serem enviados como parametro para a funcao rest , para em seguida serem enviados ao banco
    public Desempenho preencherObjDesempenho(int p_idGame, string p_descricao_fase, int p_desem_moedas, int p_desem_estrelas, int p_desem_vidas, int p_desem_ultima_fase, int p_id_usuario_ativ_turma,
       string p_faseC_data_inicio, string p_faseC_data_fim, int p_faseC_qtdErros, int p_faseC_qtdEstrelas, int p_faseC_qtdMoedas)
    {
        FaseConcluida objFaseConcluida = new FaseConcluida();
        objFaseConcluida.Data_inicio = p_faseC_data_inicio;
        objFaseConcluida.Data_fim = p_faseC_data_fim;
        objFaseConcluida.QtdErros = p_faseC_qtdErros;
        objFaseConcluida.QtdEstrelas = p_faseC_qtdEstrelas;
        objFaseConcluida.QtdMoedas = p_faseC_qtdMoedas;

        Desempenho objDesempenho = new Desempenho();
        objDesempenho.IdGame = p_idGame;
        objDesempenho.DescricaoFase = p_descricao_fase;
        objDesempenho.Moedas = p_desem_moedas;
        objDesempenho.Estrelas = p_desem_estrelas;
        objDesempenho.Vidas = p_desem_vidas;
        objDesempenho.Ultima_fase_concluida = p_desem_ultima_fase;
        objDesempenho.Id_usuario_ativ_turma = p_id_usuario_ativ_turma;
        objDesempenho.Fase_concluida = objFaseConcluida;

        return objDesempenho;
    }
}
