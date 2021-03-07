using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
public class Save_Controller : MonoBehaviour
{
    public void ChamarBuscarSaves(int id_usuario, System.Action<Lista_saves> callback)
    {
        StartCoroutine(BuscarSaves(id_usuario, callback));
    }
    IEnumerator BuscarSaves(int id_usuario, System.Action<Lista_saves> callback)
    {

        //string caminho = "http://jogos.plataformaceos.com.br/mainworld/save_game.php?";
        string caminho = "http://localhost/games/save_game.php?";
        string p_Id_usuario = "id_usuario=" + id_usuario;

        string url = string.Format("{0}{1}", caminho, p_Id_usuario);

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data, 3, www.downloadHandler.data.Length - 3);

                    string[] resultado = jsonResult.Split(';');

                    Lista_saves saves = new Lista_saves();

                    if(resultado[1] != null)
                    {
                       // Debug.Log("Resultado = "+resultado[1].Trim());
                        saves = JsonUtility.FromJson<Lista_saves>(resultado[1].Trim());
                    }

                    callback(saves);
                }
            }
        }
    }

    public void ChamarCriarSave( int id_game, int id_usuario, System.Action<bool,int> callback)
    {
        StartCoroutine(CriarSave( id_game, id_usuario, callback));
    }

    IEnumerator CriarSave(int id_game, int id_usuario, System.Action<bool,int> callback)
    {
        //string caminho = "http://jogos.plataformaceos.com.br/mainworld/criarsave.php?";
        string caminho = "http://localhost/games/criarsave.php?";

        string p_id_game = "id_game=" + id_game + "&";
        string p_id_usuario = "id_usuario=" + id_usuario;

        string url = string.Format("{0}{1}{2}", caminho, p_id_game, p_id_usuario);

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data, 3, www.downloadHandler.data.Length - 3);

                    string[] resultado = jsonResult.Split(';');

                    if(resultado[1] != null)
                    {
                            callback(true, Convert.ToInt32(resultado[1].ToString()));
                             Debug.Log("Registro criado com sucesso");
                    }
                    else
                    {
                        callback(false, Convert.ToInt32(resultado[1].ToString()));
                    }
                    
                }
            }
        }
    }

    public void ChamarDeletarSave(int id_save_game, System.Action<bool> callback)
    {
        StartCoroutine(DeletarSave(id_save_game, callback));
    }

    IEnumerator DeletarSave(int id_save_game, System.Action<bool> callback)
    {
        //string caminho = "http://jogos.plataformaceos.com.br/mainworld/deletarsave.php?";
        string caminho = "http://localhost/games/deletarsave.php?";
        string p_id_save_game = "id_save_game=" + id_save_game;

        string url = string.Format("{0}{1}", caminho, p_id_save_game);

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    Debug.Log("Registro deletado com sucesso");
                    callback(true);
                }
            }
        }

    }

    public void ChamarAtualizarSave(int id_save_game, int moedas, int estrelas, int vidas, int ultima_fase_concluida, int id_game, int id_usuario)
    {
        StartCoroutine(AtualizarSave(id_save_game, moedas, estrelas, vidas, ultima_fase_concluida, id_game, id_usuario));
    }
    IEnumerator AtualizarSave(int id_save_game, int moedas, int estrelas, int vidas, int ultima_fase_concluida, int id_game, int id_usuario)
    {
        //string caminho = "http://jogos.plataformaceos.com.br/mainworld/atualizarsave.php?";
        string caminho = "http://localhost/games/atualizarsave.php?";

        string p_id_save_game = "id_save_game=" + id_save_game + "&";
        string p_moedas = "moedas=" + moedas + "&";
        string p_estrelas = "estrelas=" + estrelas + "&";
        string p_vidas = "vidas=" + vidas + "&";
        string p_ultima_fase_concluida = "ultima_fase_concluida=" + ultima_fase_concluida + "&";
        string p_id_game = "id_game=" + id_game + "&";
        string p_id_usuario = "id_usuario=" + id_usuario + "&";

        string url = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", caminho, p_id_save_game, p_moedas, p_estrelas, p_vidas, p_ultima_fase_concluida, p_id_game, p_id_usuario);

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    Debug.Log("Registro atualizado com sucesso");
                }
            }
        }
    }
}
