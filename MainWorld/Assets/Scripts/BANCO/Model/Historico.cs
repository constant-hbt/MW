using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public class Historico
{
    public int id_historico;
    public string descricao;
    public int moedas;
    public int estrelas;
    public int vidas;
    public int ultima_fase_concluida;
    public DateTime data_hora;
    public string blocos_utilizados;
    public int id_usuario;
    public int id_atividade;

    public Historico() { }

    public Historico(string p_descricao, int p_moedas, int p_estrelas, int p_vidas, int p_ultima_fase_concluida, DateTime p_data_hora,string p_blocos_utilizados, int p_id_usuario, int p_id_atividade)
    {
        this.descricao = p_descricao;
        this.moedas = p_moedas;
        this.estrelas = p_estrelas;
        this.vidas = p_vidas;
        this.ultima_fase_concluida = p_ultima_fase_concluida;
        this.data_hora = p_data_hora;
        this.blocos_utilizados = p_blocos_utilizados;
        this.id_usuario = p_id_usuario;
        this.id_atividade = p_id_atividade;
    }

    public int Id_historico
    {
        get { return id_historico; }
        set { id_historico = value; }
    }
    public string Descricao
    {
        get { return descricao; }
        set { descricao = value; }
    }
    public int Moedas
    {
        get { return moedas; }
        set { moedas = value; }
    }
    public int Estrelas
    {
        get { return estrelas; }
        set { estrelas = value; }
    }
    public int Vidas
    {
        get { return vidas; }
        set { vidas = value; }
    }
    public int Ultima_fase_concluida
    {
        get { return ultima_fase_concluida; }
        set { ultima_fase_concluida = value; }
    }
    public DateTime Data_hora
    {
        get { return data_hora; }
        set { data_hora = value; }
    }
    public string Blocos_utilizados
    {
        get { return blocos_utilizados; }
        set { blocos_utilizados = value; }
    }
    public int Id_usuario
    {
        get { return id_usuario; }
        set { id_usuario = value; }
    }
    public int Id_atividade
    {
        get { return id_atividade; }
        set { id_atividade = value; }
    }
}
