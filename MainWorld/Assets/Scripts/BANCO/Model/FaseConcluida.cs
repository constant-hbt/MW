using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class FaseConcluida 
{

	public string data_inicio;
	public string data_fim;
	public int	qtdErros;
	public int	qtdEstrelas;
	public int	qtdMoedas;
	//private int	id_Desempenho_game ja tenho o id dentro de desempenho , se todo correr certo sem a variavel pode excluir
	public int id_Fase_Game;

	public FaseConcluida() { }

	public FaseConcluida(string p_data_inicio, string p_data_fim, int p_qtdErros, int p_qtdEstrelas, int p_qtdMoedas/*, int p_id_Desempenho_game*/, int p_id_Fase_Game)
	{
		this.data_inicio = p_data_inicio;
		this.data_fim = p_data_fim;
		this.qtdErros = p_qtdErros;
		this.qtdEstrelas = p_qtdEstrelas;
		this.qtdMoedas = p_qtdMoedas;
		//this.id_Desempenho_game = p_id_Desempenho_game;
		this.id_Fase_Game = p_id_Fase_Game;
	}

	public string Data_inicio
	{
		get { return data_inicio; }
		set { data_inicio = value; }
	}

	public string Data_fim
	{
		get { return data_fim; }
		set { data_fim = value; }
	}

	public int QtdErros
	{
		get { return qtdErros; }
		set { qtdErros = value; }
	}
	public int QtdEstrelas
	{
		get { return qtdEstrelas; }
		set { qtdEstrelas = value; }
	}
	public int QtdMoedas
	{
		get { return qtdMoedas; }
		set { qtdMoedas = value; }
	}

	/*public int ID_Desempenho_game
	{
		get { return id_Desempenho_game; }
		set { id_Desempenho_game = value; }
	}*/
	public int ID_Fase_Game
	{
		get { return id_Fase_Game; }
		set { id_Fase_Game = value; }
	}

}
