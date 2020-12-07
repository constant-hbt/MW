using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Pergunta
{
    public int id_pergunta; //EDITAR NO BANCO A TABELA PERGUNTA E COLOCAR ID_PERGUNTA
    public string descricao;
    //public string pergunta2;
    //public string pergunta3;
    public string alternativas;
    public string descritiva;
    public int id_fase;

    public Pergunta() { }

    public Pergunta(int id, string pergunta1, string pergunta2, string pergunta3, int id_fase)
    {
        this.id_pergunta = id;
       // this.pergunta1 = pergunta1;
       // this.pergunta2 = pergunta2;
       // this.pergunta3 = pergunta3;
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
