using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using MySql.Data.MySqlClient;
public class Historico_Controller : MonoBehaviour
{
   /* public void RegistrarHistorico(Historico objHistorico)
    {
        Conexao refConexao = FindObjectOfType(typeof(Conexao)) as Conexao; //COLOCAR O SCRIPT CONEXAO VINCULADO AO OBJ "GAMECONTROLLER"
        using (MySqlConnection conexao = refConexao.ConexaoBanco())
        {
            conexao.Open();
            MySqlTransaction transaction;
            transaction = conexao.BeginTransaction();
            try
            {
                MySqlCommand query = new MySqlCommand("INSERT INTO historico_game(descricao, moedas, estrelas, vidas, ultima_fase_concluida, data_hora, blocos_utilizados" +
                    "id_usu_ativ_turma) VALUES(?,?,?,?,?,?,?,?)", conexao);

                query.Parameters.AddWithValue("@descricao", objHistorico.Descricao);
                query.Parameters.AddWithValue("@moedas", objHistorico.Moedas);
                query.Parameters.AddWithValue("@estrelas", objHistorico.Estrelas);
                query.Parameters.AddWithValue("@vidas", objHistorico.Vidas);
                query.Parameters.AddWithValue("@ultima_fase_concluida", objHistorico.Ultima_fase_concluida);
                query.Parameters.AddWithValue("@data_hora", objHistorico.Data_hora);
                query.Parameters.AddWithValue("@blocos_utilizados", objHistorico.Blocos_utilizados);
                query.Parameters.AddWithValue("@id_usu_ativ_turma", objHistorico.Id_usuario_ativ_turma);

                query.Transaction = transaction;
                if (query.ExecuteNonQuery() == 1)
                {
                    transaction.Commit();
                }
                else
                {
                    transaction.Rollback();
                }
            }
            catch (MySqlException e)
            {
                transaction.Rollback();
                throw new System.Exception(e.Message);
            }
            finally
            {
                conexao.Close();
            }
        }
    }*/
    public void ChamarRegistrarHistorico(Historico historico)
    {
        StartCoroutine(RegistrarHistorico(historico));
    }
    IEnumerator RegistrarHistorico(Historico p_historico)
    {
        
        string caminho = "http://localhost/games/salvarhistorico.php?";
        string p_Descricao ="descricao="+p_historico.Descricao+"&";//se der certo preciso alterar o valor desta variavel
        string p_Moedas = "moedas=" + p_historico.Moedas + "&";
        string p_Vidas = "vidas=" + p_historico.Vidas + "&";
        string p_Estrelas = "estrelas=" + p_historico.Estrelas + "&";
        string p_UltimaFaseConcluida = "ultimafaseconcluida=" + p_historico.Ultima_fase_concluida + "&";
        string p_Blocos = "blocos="+p_historico.Blocos_utilizados+"&"; //se der certo preciso alterar o valor desta variavel
        string p_IdAtividade = "idatividade=" + p_historico.Id_usuario_ativ_turma + "&";
        string p_IdUsuario = "idusuario=" + p_historico.Id_usuario_ativ_turma + "&";
        string p_Tempo = "tempo=" + 10;


        string url = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}", caminho,p_Descricao,p_Moedas,p_Vidas,p_Estrelas,p_UltimaFaseConcluida,p_Blocos,p_IdAtividade,p_IdUsuario,p_Tempo);
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
                Debug.Log("Histórico salvo com sucesso");
            }
        }
    }
}
