using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PainelEntrarFase : MonoBehaviour
{
    // Start is called before the first frame update

    public TextMeshProUGUI tmpTitulo;
    public TextMeshProUGUI tmpDescricao;
    public TextMeshProUGUI tmpObjetivo;

    public int idBotaoFaseSelecionado;
    private void Awake()
    {
       
       
    }
    void Start()
    {
        
        
    }
    private void OnEnable()
    {

        StartCoroutine(chamar());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator chamar()
    {
        yield return null;//new WaitForSeconds(0.1f);
        Debug.Log("idBotaoFaseSelecionado = " + idBotaoFaseSelecionado);
        switch (idBotaoFaseSelecionado)
        {
            case 1:
                Debug.Log("Iniciando script do painelEntrarFase para fase1");
                tmpTitulo.text = "Kington";
                tmpDescricao.text = "Mudei descrição para kington";
                tmpObjetivo.text = "Mudei o objetivo para kington";

                break;
            case 2:
                Debug.Log("Iniciando script do painelEntrarFase para fase2");
                tmpTitulo.text = "Wessex";
                tmpDescricao.text = "Mudei descrição para wessex";
                tmpObjetivo.text = "Mudei o objetivo para wessex";
                break;
            case 3:
                
                break;
            case 4:
                
                break;
            case 5:
                
                break;
            case 6:
                
                break;
            case 7:
                
                break;
            case 8:
                
                break;
            case 9:
                
                break;
        }
    }

    public void btnClose()
    {
        if(this.gameObject.activeSelf == true)
        {
            this.gameObject.SetActive(false);
        }
    }
}
