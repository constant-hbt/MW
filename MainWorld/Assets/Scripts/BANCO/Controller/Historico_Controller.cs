using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using MySql.Data.MySqlClient;
public class Historico_Controller : MonoBehaviour
{
    public void RegistrarHistorico(Historico objHistorico)
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
    }
}
