using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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
                        saves = JsonUtility.FromJson<Lista_saves>(resultado[1].Trim());
                    }

                    callback(saves);
                }
            }
        }
    }
}
