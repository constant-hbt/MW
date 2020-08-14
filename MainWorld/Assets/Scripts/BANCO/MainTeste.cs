using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MainTeste : MonoBehaviour
{
    private Desempenho_Controller _desempenho;
    private Historico_Controller _historico;
    void Start()
    {
        _desempenho = FindObjectOfType(typeof(Desempenho_Controller)) as Desempenho_Controller;
        _historico = FindObjectOfType(typeof(Historico_Controller)) as Historico_Controller;
        /*
            FaseConcluida objFaseConcluida = new FaseConcluida();


            Desempenho objDesempenho = new Desempenho();
            objDesempenho.IdGame = 1;
            objDesempenho.DescricaoFase = "Fase1";

            objDesempenho.Moedas = 1500;
            objDesempenho.Estrelas = 1500;
            objDesempenho.Vidas = 1500;
            objDesempenho.Ultima_fase_concluida = 1;
            objDesempenho.Id_usuario_ativ_turma = 1;

            //pega a data e a hora do sistema
            DateTime data = DateTime.Now.ToLocalTime();
            string data_inicio_string = data.ToString();//converte para string para passar como json(json nao tem serializacao e desserializacao de DateTime)
            string data_fim_string = data.ToString();//Antes de gravar no banco e ao pegar os dados do banco deve se converter os DateTime para 

            objDesempenho.Fase_concluida = new FaseConcluida(data_inicio_string, data_fim_string, 50, 150, 150, 1) ;

            _desempenho.SendRestPostRegistrarDesemepenho(objDesempenho);
        */

        Historico objHistorico = new Historico();
        objHistorico.Moedas = 1100;
        objHistorico.Estrelas = 1100;
        objHistorico.Vidas = 1100;
        objHistorico.Ultima_fase_concluida = 1;

        DateTime data = DateTime.Now.ToLocalTime();
        string data_hora_local = data.ToString();

        objHistorico.Data_hora = data_hora_local;
        objHistorico.Id_usuario_ativ_turma = 1;

        _historico.SendPostRegistrarHistorico(objHistorico);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
