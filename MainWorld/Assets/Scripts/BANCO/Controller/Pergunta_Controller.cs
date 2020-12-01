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
        string caminho = "http://localhost/games/salvarresposta.php?";/* "http://jogos.plataformaceos.com.br/mainworld/salvarresposta.php?";*/
        string p_Resposta1 = "resposta1=" + resposta.Resposta_pergunta1 + "&";
        string p_Resposta2 = "resposta2=" + resposta.Resposta_pergunta2 + "&";
        string p_Resposta3 = "resposta3=" + resposta.Resposta_pergunta3 + "&";
        string p_Pergunta_id = "pergunta_id=" + resposta.Pergunta_id;

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
                    Debug.Log("Resposta salva com sucesso");
                }
                
            }
        }

    }

    IEnumerator PegarPergunta(int id_fase)
    {
        string caminho = "http://localhost/games/captarperguntas.php?";/* "http://jogos.plataformaceos.com.br/mainworld/captarperguntas.php?";*/
        string p_id_fase = "id_fase=" + id_fase;

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
                    string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    Pergunta retorno_pergunta = JsonUtility.FromJson<Pergunta>(jsonResult);

                    //VER A QUESTAO DA CALLBACK DEPOIS
                }
            }
        }
    }
}
