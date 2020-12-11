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

    public Pergunta(string retornoPHP)
    {
        string[] arrayRetornoPHP = retornoPHP.Split(';');
       // this.id = Convert.ToInt32(arrayRetornoPHP[1]);
      //  this.pergunta1 = arrayRetornoPHP[2];
       // this.pergunta2 = arrayRetornoPHP[3];
       // this.pergunta3 = arrayRetornoPHP[4];
        this.id_fase = Convert.ToInt32(arrayRetornoPHP[5]);

        
    }
   /*public int Id
    {
        get { return id; }
        set { id = value; }
    }

    
   public string Pergunta1
    {
        get { return pergunta1; }
        set { pergunta1 = value; }
    }

    public string Pergunta2
    {
        get { return pergunta2; }
        set { pergunta2 = value; }
    }

    public string Pergunta3
    {
        get { return pergunta3; }
        set { pergunta3 = value; }
    }

    public int Id_fase
    {
        get { return id_fase; }
        set { id_fase = value; }
    }*/
}
