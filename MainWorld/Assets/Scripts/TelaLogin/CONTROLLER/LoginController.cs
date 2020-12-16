using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LoginController : MonoBehaviour
{

    public void ChamarValidarUsuario(string usuario, string senha)
    {
        StartCoroutine(ValidarUsuario(usuario, senha));
    }
    IEnumerator ValidarUsuario(string p_usuario, string p_senha)
    {
        string caminho = "http://localhost/games/validarlogin.php?";// "http://jogos.plataformaceos.com.br/mainworld/salvarresposta.php?";
        string usuario = "usuario=" + p_usuario + "&";
        string senha = "senha=" + p_senha;

        string url = string.Format("{0}{1}{2}", caminho, usuario, senha);
        Debug.Log("Url montada = " + url);

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log("Erro ao validar usuario");
            }
            else
            {
                if (www.isDone)
                {
                    string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data, 3, www.downloadHandler.data.Length - 3);
                    
                    
                    string[] resultado = jsonResult.Split(';');

                    if(resultado[1].Trim() == "")
                    {
                        Debug.Log("Usuario não possui cadastro");
                    }
                    else
                    {
                        UsuarioModel novoUsuario = JsonUtility.FromJson<UsuarioModel>(resultado[1].Trim());
                        Debug.Log("Id = " + novoUsuario.Id_usuario);
                        Debug.Log("Nome = " + novoUsuario.Nome);
                        Debug.Log("Usuario = " + novoUsuario.Usuario);
                        Debug.Log("Senha = " + novoUsuario.Senha);
                        Debug.Log("Email = " + novoUsuario.Email);
                    }

                    

                }
            }

        }

    }
}
