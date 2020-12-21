using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using MySql.Data.MySqlClient;
public class Historico_Controller : MonoBehaviour
{
   
    public void ChamarRegistrarHistorico(Historico historico)
    {
        StartCoroutine(RegistrarHistorico(historico));
    }
    IEnumerator RegistrarHistorico(Historico p_historico)
    {

        string caminho =  "http://jogos.plataformaceos.com.br/mainworld/salvarhistorico.php?";// "http://localhost/games/salvarhistorico.php?";
        string p_Descricao ="descricao="+p_historico.Descricao+"&";
        string p_Moedas = "moedas=" + p_historico.Moedas + "&";
        string p_Vidas = "vidas=" + p_historico.Vidas + "&";
        string p_Estrelas = "estrelas=" + p_historico.Estrelas + "&";
        string p_UltimaFaseConcluida = "ultimafaseconcluida=" + p_historico.Ultima_fase_concluida + "&";
        string p_Blocos = "blocos="+p_historico.Blocos_utilizados+"&"; 
        string p_IdAtividade = "idatividade=" + p_historico.Id_atividade + "&";
        string p_IdUsuario = "idusuario=" + p_historico.Id_usuario;

        string url = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}", caminho,p_Descricao,p_Moedas,p_Vidas,p_Estrelas,p_UltimaFaseConcluida,p_Blocos,p_IdAtividade,p_IdUsuario);
        Debug.Log("Url montada =" + url);

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

    
}
