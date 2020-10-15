using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoTeleporte : MonoBehaviour
{
    private PlayerController _playerController;

    public GameObject[] posicoes;
    public GameObject[] inimigos;
    public List<int> posicaoJaOcupada = new List<int>();

    void Start()
    {
        _playerController = FindObjectOfType(typeof(PlayerController)) as PlayerController;
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
    public void jaPassou(GameObject pilarAtual)
    {//modifica o pilar de mudar o inimigo de posicao para ja passou

        pilarAtual.transform.GetChild(0).gameObject.SetActive(false);
        pilarAtual.transform.GetChild(1).gameObject.SetActive(true);
        StartCoroutine(habilitarColisaoPilar());
        
    }

    IEnumerator habilitarColisaoPilar()
    {
        yield return new WaitForSeconds(3f);
        _playerController.colidiMoveInimigo = false;
    }
}
