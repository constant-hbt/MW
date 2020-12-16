using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Resposta 
{
    public int id_usuario;
    public int id_pergunta1;
    public int id_pergunta2;
    public int id_pergunta3;
    public string resposta1;
    public string resposta2;
    public string resposta3;

    public Resposta() { }

    public Resposta(int p_id_usuario, int p_id_pergunta1, int p_id_pergunta2, int p_id_pergunta3, string p_resposta1, string p_resposta2, string p_resposta3)
    {
        this.id_usuario = p_id_usuario;
        this.id_pergunta1 = p_id_pergunta1;
        this.id_pergunta2 = p_id_pergunta2;
        this.id_pergunta3 = p_id_pergunta3;
        this.resposta1 = p_resposta1;
        this.resposta2 = p_resposta2;
        this.resposta3 = p_resposta3;
    }

    public int Id_usuario
    {
        get { return id_usuario; }
        set { id_usuario = value; }
    }
    public int Id_pergunta1
    {
        get { return id_pergunta1; }
        set { id_pergunta1 = value; }
    }

    public int Id_pergunta2
    {
        get { return id_pergunta2; }
        set { id_pergunta2 = value; }
    }

    public int Id_pergunta3
    {
        get { return id_pergunta3; }
        set { id_pergunta3 = value; }
    }
    public string Resposta1
    {
        get { return resposta1; }
        set { resposta1 = value; }
    }

    public string Resposta2
    {
        get { return resposta2; }
        set { resposta2 = value; }
    }

    public string Resposta3
    {
        get { return resposta3; }
        set { resposta3 = value; }
    }
    
}
