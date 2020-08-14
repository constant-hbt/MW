using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Historico
{
    public int id_historico;
    public int moedas;
    public int estrelas;
    public int vidas;
    public int ultima_fase_concluida;
    public string data_hora;
    public int id_usuario_ativ_turma;

    public Historico() { }

    public Historico(int p_moedas, int p_estrelas, int p_vidas, int p_ultima_fase_concluida, string p_data_hora, int p_id_usuario_ativ_turma)
    {
        this.moedas = p_moedas;
        this.estrelas = p_estrelas;
        this.vidas = p_vidas;
        this.ultima_fase_concluida = p_ultima_fase_concluida;
        this.data_hora = p_data_hora;
        this.id_usuario_ativ_turma = p_id_usuario_ativ_turma;
    }

    public int Id_historico
    {
        get { return id_historico; }
        set { id_historico = value; }
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
    public string Data_hora
    {
        get { return data_hora; }
        set { data_hora = value; }
    }
    public int Id_usuario_ativ_turma
    {
        get { return id_usuario_ativ_turma; }
        set { id_usuario_ativ_turma = value; }
    }
}
