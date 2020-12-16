using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Pergunta
{
    public int id_pergunta; //EDITAR NO BANCO A TABELA PERGUNTA E COLOCAR ID_PERGUNTA
    public string descricao;
    public string[] alternativas; //vai trocar para public Alternativas[] alternativas;
    public int id_fase;

    public Pergunta() { }

    public Pergunta(int id, string descricao, string[] alternativas, int id_fase)
    {
        this.id_pergunta = id;
        this.descricao = descricao;
        this.alternativas = alternativas;
        this.id_fase = id_fase;
    }

    
   public int Id_pergunta
    {
        get { return id_pergunta; }
        set { id_pergunta = value; }
    }

    
   public string Descricao
    {
        get { return descricao; }
        set { descricao = value; }
    }


    public int Id_fase
    {
        get { return id_fase; }
        set { id_fase = value; }
    }
}
