using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoTeleporte : MonoBehaviour
{

    public GameObject[] posicoes;
    public GameObject[] inimigos;

    public List<int> posicaoJaOcupada = new List<int>();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void mudarPosicao()
    {
        posicaoJaOcupada.Clear();
       
        for(int i=0; i< inimigos.Length; i++)
        {
            bool contidoNoArray = true;



            while (contidoNoArray)
            {
                int numPosicao = Random.Range(1, posicoes.Length+1);

                Debug.Log("Valor do numPosicao = " + numPosicao);
                if (posicaoJaOcupada.Contains(numPosicao))
                {
                    contidoNoArray = true;
                }
                else
                {
                    contidoNoArray = false;
                    posicaoJaOcupada.Add(numPosicao);
                    inimigos[i].transform.position = new Vector3(posicoes[numPosicao - 1].transform.position.x, posicoes[numPosicao - 1].transform.position.y, posicoes[numPosicao - 1].transform.position.z);
                }

            } ;

            

        }
        
       
    }
}
