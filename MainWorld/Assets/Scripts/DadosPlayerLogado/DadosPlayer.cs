using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DadosPlayer
{
    public int id_usuario;
    public int fase_concluida;
    public int moedas;
    public int vidas;
    public int estrelas;
    public int ultima_fase_concluida;

    public DadosPlayer() { }

    public DadosPlayer(int p_id_usuario, int p_fase_concluida, int p_moedas, int p_vidas, int p_estrelas, int p_ultima_fase_concluida)
    {
        this.id_usuario = p_id_usuario;
        this.fase_concluida = p_fase_concluida;
        this.moedas = p_moedas;
        this.vidas = p_vidas;
        this.estrelas = p_estrelas;
        this.ultima_fase_concluida = p_ultima_fase_concluida;
    }

    public int Id_usuario
    {
        get { return id_usuario; }
        set { id_usuario = value; }
    }

    public int Fase_concluida
    {
        get { return fase_concluida; }
        set { fase_concluida = value; }
    }

    public int Moedas
    {
        get { return moedas; }
        set { moedas = value; }
    }
    
    public int Vidas
    {
        get { return vidas; }
        set { vidas = value; }
    }

    public int Estrelas
    {
        get { return estrelas; }
        set { estrelas = value; }
    }
    public int Ultima_fase_concluida
    {
        get { return ultima_fase_concluida; }
        set { ultima_fase_concluida = value; }
    }

}
