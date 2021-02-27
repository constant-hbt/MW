using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Save_game
{
    public int id_save_game;
    public int moedas;
    public int estrelas;
    public int vidas;
    public int ultima_fase_concluida;
    public DateTime data_hora;
    public int id_game;
    public int id_usuario;

    public Save_game() { }

    public Save_game(int p_id_save_game, int p_moedas, int p_estrelas, int p_vidas, int p_ultima_fase_concluida, DateTime p_data_hora, int p_id_game, int p_id_usuario)
    {
        this.id_save_game = p_id_save_game;
        this.moedas = p_moedas;
        this.estrelas = p_estrelas;
        this.vidas = p_vidas;
        this.ultima_fase_concluida = p_ultima_fase_concluida;
        this.data_hora = p_data_hora;
        this.id_game = p_id_game;
        this.id_usuario = p_id_usuario;
    }

    public int Id_save_game
    {
        get { return id_save_game; }
        set { id_save_game = value; }
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

    public DateTime Data_Hora
    {
        get { return data_hora; }
        set { data_hora = value; }
    }

    public int Id_game
    {
        get { return id_game; }
        set { id_game = value; }
    }

    public int Id_usuario
    {
        get { return id_usuario; }
        set { id_usuario = value; }
    }
}
