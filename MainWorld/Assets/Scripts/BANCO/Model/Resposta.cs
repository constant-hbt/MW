using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Resposta
{
    private int id;
    private string resposta_pergunta1;
    private string resposta_pergunta2;
    private string resposta_pergunta3;
    private int pergunta_id;

    public Resposta() { }

    public Resposta(int p_id, string p_resposta_pergunta1, string p_resposta_pergunta2, string p_resposta_pergunta3, int p_pergunta_id)
    {
        this.id = p_id;
        this.resposta_pergunta1 = p_resposta_pergunta1;
        this.resposta_pergunta2 = p_resposta_pergunta2;
        this.resposta_pergunta3 = p_resposta_pergunta3;
        this.pergunta_id = p_pergunta_id;
    }

    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    public string Resposta_pergunta1
    {
        get { return resposta_pergunta1; }
        set { resposta_pergunta1 = value; }
    }

    public string Resposta_pergunta2
    {
        get { return resposta_pergunta2; }
        set { resposta_pergunta2 = value; }
    }

    public string Resposta_pergunta3
    {
        get { return resposta_pergunta3; }
        set { resposta_pergunta3 = value; }
    }

    public int Pergunta_id
    {
        get { return pergunta_id; }
        set { pergunta_id = value; }
    }
}
