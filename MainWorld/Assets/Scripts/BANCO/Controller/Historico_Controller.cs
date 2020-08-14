using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class Historico_Controller : MonoBehaviour
{
    string WEB_URL = "localhost:3000";
    string rota = "/novoHistorico";
    void Start()
    {
        
    }

    public void SendPostRegistrarHistorico(Historico historico)
    {
        StartCoroutine(RegistrarHistorico(WEB_URL, rota, historico));
    }

    IEnumerator RegistrarHistorico(string url, string rota, Historico p_historico)
    {
        string urlNew = string.Format("{0}{1}", url, rota);//concatena a rota com a url

        string jsonData = JsonUtility.ToJson(p_historico);
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
                    Debug.Log(jsonResult);
                    // Mensagem mensagem_rest = JsonUtility.FromJson<Mensagem>(jsonResult);//crio uma var do tipo mensagem e preencho com o json retornado do servidor
                    // callback(mensagem_rest);//preencho a callback com a informação retornada do servidor

                }
            }
        }

        yield return null;
    }
}
