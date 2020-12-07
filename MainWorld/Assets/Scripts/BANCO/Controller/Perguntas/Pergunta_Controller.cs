using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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
        string caminho = "http://localhost/games/salvarresposta.php?";// "http://jogos.plataformaceos.com.br/mainworld/salvarresposta.php?";
        string p_Resposta1 = "resposta1=" + resposta.Resposta_pergunta1 + "&";
        string p_Resposta2 = "resposta2=" + resposta.Resposta_pergunta2 + "&";
        string p_Resposta3 = "resposta3=" + resposta.Resposta_pergunta3 + "&";
        string p_Pergunta_id = "id_pergunta=" + resposta.Id_pergunta;

        Debug.Log(p_Pergunta_id);
        string url = string.Format("{0}{1}{2}{3}{4}", caminho, p_Resposta1, p_Resposta2, p_Resposta3, p_Pergunta_id);
        Debug.Log("Url montada = "+url);

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
        string caminho = "http://localhost/games/captarperguntas.php?";/* "http://jogos.plataformaceos.com.br/mainworld/captarperguntas.php?";*/
        string p_id_fase = "idfase=" + id_fase;

        string url = string.Format("{0}{1}", caminho, p_id_fase);
        Debug.Log("Url montada = " + url);


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
                   // Debug.Log("JsonResult = " + jsonResult);

                    string[] resultado = jsonResult.Split(';');
                    //Debug.Log("Resultado[1] = " + resultado[1].Trim());
                    Perguntas objPerguntas = JsonUtility.FromJson<Perguntas>(resultado[1].Trim());
                    
                    Debug.Log(objPerguntas.perguntas.Length);
                    Debug.Log("Id_pergunta[0] = " + objPerguntas.perguntas[0].id_pergunta);
                    Debug.Log("Descricao[0] = " +System.Text.Encoding.UTF8.GetBytes(objPerguntas.perguntas[0].descricao));
                    Debug.Log("Alternativas[0]= " + objPerguntas.perguntas[0].alternativas);
                    Debug.Log("Id_pergunta[1] = " + objPerguntas.perguntas[1].id_pergunta);
                    Debug.Log("Descricao[1] = " + objPerguntas.perguntas[1].descricao);
                    Debug.Log("Alternativas[1]= " + objPerguntas.perguntas[1].alternativas);
                    Debug.Log("Id_pergunta[2] = " + objPerguntas.perguntas[2].id_pergunta);
                    Debug.Log("Descricao[2] = " + objPerguntas.perguntas[2].descricao);
                    Debug.Log("Alternativas[2]= " + objPerguntas.perguntas[2].alternativas);
                    callback(objPerguntas);
                }
            }
        }
    }
}
