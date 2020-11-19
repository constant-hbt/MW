using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;

public class Conexao : MonoBehaviour
{

    private string _linhaConexao = "Server=localhost;" +
                                   "Database=mw;" +
                                   "User ID=root;" +
                                   "Password=;" +
                                   "Pooling=false;" +
                                   "Allow User Variables=True;";

   public MySqlConnection ConexaoBanco()
    {
        MySqlConnection conexao = new MySqlConnection(_linhaConexao);
        return conexao;
    }


}
