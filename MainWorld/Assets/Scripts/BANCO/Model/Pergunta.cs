using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pergunta
{
    private int id;
    private string descricao_pergunta1;
    private string descricao_pergunta2;
    private string descricao_pergunta3;
    private int id_fase;

    public Pergunta() { }

    public Pergunta(int p_id, string p_pergunta1, string p_pergunta2, string p_pergunta3, int p_id_fase)
    {
        this.id = p_id;
        this.descricao_pergunta1 = p_pergunta1;
        this.descricao_pergunta2 = p_pergunta2;
        this.descricao_pergunta3 = p_pergunta3;
        this.id_fase = p_id_fase;
    }

    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    public string Descricao_pergunta1
    {
        get { return descricao_pergunta1; }
        set { descricao_pergunta1 = value; }
    }

    public string Descricao_pergunta2
    {
        get { return descricao_pergunta2; }
        set { descricao_pergunta2 = value; }
    }

    public string Descricao_pergunta3
    {
        get { return descricao_pergunta3; }
        set { descricao_pergunta3 = value; }
    }

    public int Id_fase
    {
        get { return id_fase; }
        set { id_fase = value; }
    }
}
