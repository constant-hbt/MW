using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
public class Pergunta_Controller : MonoBehaviour
{
    
    void Start()
    {
        
    }
    
   public void ChamarRegistrarResposta(Resposta p_resposta)
    {
        StartCoroutine(RegistrarResposta(p_resposta));
    }
    IEnumerator RegistrarResposta(Resposta resposta)
    {
       // string caminho = "http://jogos.plataformaceos.com.br/mainworld/salvarresposta.php?";
        string caminho ="http://localhost/games/salvarresposta.php?";
        string p_id_usuario = "id_usuario=" + resposta.Id_usuario + "&";
        string p_IdPergunta1 = "id_pergunta1=" + resposta.Id_pergunta1 + "&";
        string p_Resposta1 = "resposta1=" + resposta.Resposta1 + "&";
        string p_IdPergunta2 = "id_pergunta2=" + resposta.Id_pergunta2 + "&";
        string p_Resposta2 = "resposta2=" + resposta.Resposta2 + "&";
        string p_IdPergunta3 = "id_pergunta3=" + resposta.Id_pergunta3 + "&";
        string p_Resposta3 = "resposta3=" + resposta.Resposta3+"&";
        string p_IdPergunta4 = "id_pergunta4=" + resposta.Id_pergunta4 + "&";
        string p_Resposta4 = "resposta4=" + resposta.Resposta4;

        string url = "";

        if (p_id_usuario!= "" && p_IdPergunta1 != "" && p_Resposta1 != "" && p_IdPergunta2 == "" && p_Resposta2 == "" && p_IdPergunta3 == "" && p_Resposta3 == "")
        {

           url = string.Format("{0}{1}{2}{3}", caminho,p_id_usuario, p_IdPergunta1, p_Resposta1);

        }else if(p_id_usuario != "" && p_IdPergunta1 != "" && p_Resposta1 != "" && p_IdPergunta2 != "" && p_Resposta2 != "" && p_IdPergunta3 == "" && p_Resposta3 == "")
        {

            url = string.Format("{0}{1}{2}{3}{4}{5}", caminho, p_id_usuario, p_IdPergunta1, p_Resposta1, p_IdPergunta2, p_Resposta2);

        }else if(p_id_usuario != "" && p_IdPergunta1 != "" && p_Resposta1 != "" && p_IdPergunta2 != "" && p_Resposta2 != "" && p_IdPergunta3 != "" && p_Resposta3 != ""
            && p_IdPergunta3 == "" && p_Resposta3 == "")
        {

            url = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", caminho, p_id_usuario, p_IdPergunta1, p_Resposta1, p_IdPergunta2, p_Resposta2, p_IdPergunta3, p_Resposta3);

        }
        else if (p_id_usuario != "" && p_IdPergunta1 != "" && p_Resposta1 != "" && p_IdPergunta2 != "" && p_Resposta2 != "" && p_IdPergunta3 != "" && p_Resposta3 != ""
            && p_IdPergunta4 != "" && p_Resposta4 != "")
        {

            url = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}", caminho, p_id_usuario, p_IdPergunta1, p_Resposta1, p_IdPergunta2, p_Resposta2, p_IdPergunta3, p_Resposta3, p_IdPergunta4, p_Resposta4);

        }

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();//faz o envio da requisição

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);


                    Debug.Log(jsonResult);
                }
                
            }
        }

    }

    //chama a coroutine que faz a requisição no banco
    public void ChamarPegarPergunta(int id_fase, System.Action<Perguntas> callback)
    {
        StartCoroutine(PegarPergunta(id_fase, callback));
    }

    //faz a requisição ao banco e traz o retorno
    IEnumerator PegarPergunta(int id_fase, System.Action<Perguntas> callback)
    {
       // string caminho = "http://jogos.plataformaceos.com.br/mainworld/captarperguntas.php?";
       string caminho = "http://localhost/games/captarperguntas.php?";
        string p_id_fase = "idfase=" + id_fase;

        string url = string.Format("{0}{1}", caminho, p_id_fase);
        
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();//faz o envio da requisição

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                  string jsonResult =  System.Text.Encoding.UTF8.GetString(www.downloadHandler.data,3,www.downloadHandler.data.Length - 3);

                  string[] resultado = jsonResult.Split(';');

                    Perguntas objPerguntas = new Perguntas();

                    if (resultado[1] != null)
                    {
                        objPerguntas = JsonUtility.FromJson<Perguntas>(resultado[1].Trim());
                    }

                    callback(objPerguntas);
                }
            }
        }
    }

    //sera utilizado somente para a realização dos testes

    

}
