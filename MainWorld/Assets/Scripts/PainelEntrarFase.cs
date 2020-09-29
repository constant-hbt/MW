using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;
public class PainelEntrarFase : MonoBehaviour
{
    // Start is called before the first frame update
    private GameController _gameController;

    public TextMeshProUGUI tmpTitulo;
    public TextMeshProUGUI tmpDescricao;
    public TextMeshProUGUI tmpObjetivo;

    

    public int idBotaoFaseSelecionado;
    private string faseHaExecutar;


    
    private void Awake()
    {
       
       
    }
    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        
    }
    private void OnEnable()
    {

        chamar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void chamar()
    {
      
        Debug.Log("idBotaoFaseSelecionado = " + idBotaoFaseSelecionado);
        switch (idBotaoFaseSelecionado)
        {
            case 1:
                Debug.Log("Iniciando script do painelEntrarFase para fase1");
                tmpTitulo.text = "Kington";
                tmpDescricao.text = "Mudei descrição para kington";
                tmpObjetivo.text = "Mudei o objetivo para kington";
                faseHaExecutar = "Fase1";

                break;
            case 2:
                Debug.Log("Iniciando script do painelEntrarFase para fase2");
                tmpTitulo.text = "Devon";
                tmpDescricao.text = "Mudei descrição para devon";
                tmpObjetivo.text = "Mudei o objetivo para devon";
                faseHaExecutar = "Fase2";
                break;
            case 3:
                Debug.Log("Iniciando script do painelEntrarFase para fase3");
                tmpTitulo.text = "York";
                tmpDescricao.text = "Mudei descrição para york";
                tmpObjetivo.text = "Mudei o objetivo para york";
                faseHaExecutar = "Fase3";
                break;
            case 4:
                Debug.Log("Iniciando script do painelEntrarFase para fase4");
                tmpTitulo.text = "Wareham";
                tmpDescricao.text = "Mudei descrição para wareham";
                tmpObjetivo.text = "Mudei o objetivo para wareham";
                faseHaExecutar = "Fase4";
                break;
            case 5:
                Debug.Log("Iniciando script do painelEntrarFase para fase5");
                tmpTitulo.text = "Edington";
                tmpDescricao.text = "Mudei descrição para edington";
                tmpObjetivo.text = "Mudei o objetivo para edington";
                faseHaExecutar = "Fase5";
                break;
            case 6:
                Debug.Log("Iniciando script do painelEntrarFase para fase6");
                tmpTitulo.text = "Chippenham";
                tmpDescricao.text = "Mudei descrição para chippenham";
                tmpObjetivo.text = "Mudei o objetivo para chippenham";
                faseHaExecutar = "Fase6";
                break;
            case 7:
                Debug.Log("Iniciando script do painelEntrarFase para fase7");
                tmpTitulo.text = "Wantage";
                tmpDescricao.text = "Mudei descrição para wantage";
                tmpObjetivo.text = "Mudei o objetivo para wantage";
                faseHaExecutar = "Fase7";
                break;
            case 8:
                Debug.Log("Iniciando script do painelEntrarFase para fase8");
                tmpTitulo.text = "Exeter";
                tmpDescricao.text = "Mudei descrição para exeter";
                tmpObjetivo.text = "Mudei o objetivo para exeter";
                faseHaExecutar = "Fase8";
                break;
            case 9:
                Debug.Log("Iniciando script do painelEntrarFase para fase9");
                tmpTitulo.text = "Wessex";
                tmpDescricao.text = "Mudei descrição para wessex";
                tmpObjetivo.text = "Mudei o objetivo para wessex";
                faseHaExecutar = "Fase9";
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
    public void btnJogar()
    {
        SceneManager.LoadScene(faseHaExecutar);
        
    }
}
