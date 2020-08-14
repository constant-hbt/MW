using System;

[System.Serializable]
public class Desempenho
{
    public int idGame;
	public string descricaoFase;

    public int id_Desempenho;
    public int moedas;
    public int estrelas;
    public int vidas;
    public int ultima_fase_concluida;
    public int id_usuario_ativ_turma;
    public FaseConcluida fase_concluida;


    public Desempenho() { }

    public Desempenho(int p_idGame, string p_descricaoFase, int p_id_Desempenho, int p_moedas, int p_estrelas, int p_vidas, int p_ultima_fase_concluida, int p_id_usuario_ativ_turma, FaseConcluida p_faseConcluida)
    {
        this.idGame = p_idGame;
        this.descricaoFase = p_descricaoFase;
        this.id_Desempenho = p_id_Desempenho;
        this.moedas = p_moedas;
        this.estrelas = p_estrelas;
        this.vidas = p_vidas;
        this.ultima_fase_concluida = p_ultima_fase_concluida;
        this.id_usuario_ativ_turma = p_id_usuario_ativ_turma;
        this.fase_concluida = p_faseConcluida;

    }

    public int IdGame
    {
        get { return idGame; }
        set { idGame = value; }
        
    }
    public string DescricaoFase
    {
        get { return descricaoFase; }
        set { descricaoFase = value; }
    }
    public int Id_Desempenho
    {
        get { return id_Desempenho; }
        set { id_Desempenho = value; }
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
    public int Id_usuario_ativ_turma
    {
        get { return id_usuario_ativ_turma; }
        set { id_usuario_ativ_turma = value; }
    }
     public FaseConcluida Fase_concluida
    {
        get { return fase_concluida; }
        set { fase_concluida = value; }
    }
}
