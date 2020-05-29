using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{

    /// <summary>
    /// RESPONSAVEL PELAS CONFIGURACOES DO HUD COM DADOS DO PLAYER
    /// </summary>
    private PlayerController _player;
    private SeleçãoFase _selecaoFase;
    private GameController _gameController;
    private ControllerFase _controllerFase;
    [Header("Controle de vida - player")]
    public Image[] hpBar;//barras de vida

    [Header("Controle HUD")]
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI vidaText;
    void Start()
    {
        _player = FindObjectOfType(typeof(PlayerController)) as PlayerController;
        _selecaoFase = FindObjectOfType(typeof(SeleçãoFase)) as SeleçãoFase;
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        _controllerFase = FindObjectOfType(typeof(ControllerFase)) as ControllerFase;
        foreach(Image img in hpBar)
        {
            img.enabled = true;
        }
    }
    void Update()
    {

        controleBarraVida();

        goldText.text = _controllerFase.qtdMoedasColetadas.ToString();
        
    }

    void controleBarraVida()
    {
        switch (_player.vidaAtual)
        {
            case 3:
                for(int i=0; i < hpBar.Length; i++)
                {
                    hpBar[i].enabled = true;
                }
                break;
            case 2:
                for (int i = 0; i < hpBar.Length; i++)
                {
                    if(i >=0 && i <= 1)
                    {
                        hpBar[i].enabled = true;
                    }
                    else
                    {
                        hpBar[i].enabled = false;

                    }
                }
                break;
            case 1:
                for (int i = 0; i < hpBar.Length; i++)
                {
                    if (i == 0)
                    {
                        hpBar[i].enabled = true;
                    }
                    else
                    {
                        hpBar[i].enabled = false;

                    }
                }
                break;
            case 0:
                for (int i = 0; i < hpBar.Length; i++)
                {
                        hpBar[i].enabled = false;
                    
                }
                break;
        }


    }
}
