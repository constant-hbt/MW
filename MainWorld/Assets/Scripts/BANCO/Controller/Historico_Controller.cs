using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
public class Historico_Controller : MonoBehaviour
{
    private GameController _gameController;
    private ControllerFase _controllerFase;

    string WEB_URL = "http://localhost:3000";
    string rota = "/novoHistorico";
    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        _controllerFase = FindObjectOfType(typeof(ControllerFase)) as ControllerFase;
    }

    public void EnviarRegistroHistorico()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        _controllerFase = FindObjectOfType(typeof(ControllerFase)) as ControllerFase;

        Historico objHistorico = new Historico();
        objHistorico.Moedas = _gameController.numGold;
        objHistorico.Estrelas = _gameController.numEstrelas;
        objHistorico.Vidas = _gameController.numVida;
        objHistorico.Ultima_fase_concluida = _gameController.fasesConcluidas;
        objHistorico.Data_hora = DateTime.Now.ToLocalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
        objHistorico.Id_usuario_ativ_turma = _gameController.id_usuario_ativ_turma;

        SendRestPostRegistrarHistorico(objHistorico);
    }

    public void SendRestPostRegistrarHistorico(Historico historico)
    {
        StartCoroutine(RegistrarHistorico(WEB_URL, rota, historico));
    }

    IEnumerator RegistrarHistorico(string url, string rota, Historico p_historico)
    {
        string urlNew = string.Format("{0}{1}", url, rota);//concatena a rota com a url

        string jsonData = JsonUtility.ToJson(p_historico);
        Debug.Log("Mandei este jsonData =" + jsonData);

       /* using (UnityWebRequest www = UnityWebRequest.Post(urlNew, jsonData))
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
        }*/

        yield return null;
    }
}
