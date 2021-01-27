using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    public GameObject[] audioControllers;

    public AudioSource sMusic; //FONTE DE MUSICA
    public AudioSource sFX; //FONTE DE EFEITOS SONOROS

    [Header("Musicas")]

    public AudioClip musicaTitulo;
    public AudioClip musicaSelecaoFases;
    public AudioClip musicaFase1a3;
    public AudioClip musicaFase4a5;
    public AudioClip musicaFase6a7;
    public AudioClip musicaFase9Parte0e1;
    public AudioClip musicaFase9Parte2;
    public AudioClip musicaTelaGameOver;
    public AudioClip musicaTelaGameWin;

    // [Header("Efeitos sonoros")]


    //CONFIGURACOES DOS AUDIOS
    public float volumeMaxMusica;
  //  private float volumeMaxFX;

    //CONFIGURACOES DA TROCA DE MUSICA
    private AudioClip novaMusica;
    private string novaCena;
    private bool trocarCena;
    void Start()
    {
    }
    private void Awake()
    {
        VerificarQtdObjAudioC();
        DontDestroyOnLoad(this.gameObject);

        if(PlayerPrefs.GetInt("valoresIniciais") == 0)
        {
            PlayerPrefs.SetInt("valoresIniciais", 1);
            PlayerPrefs.SetFloat("valorMaxMusica", 1);
            
        }

        //CARREGA AS CONFIGURACOES DE AUDIO
        volumeMaxMusica = PlayerPrefs.GetFloat("valorMaxMusica");
      //  volumeMaxFX = 1;

       // trocarMusica(musicaTitulo, "TelaInicio", true);
    }

    public void trocarMusica(AudioClip clip, string nomeCena, bool mudarCena)
    {
        novaMusica = clip;
        novaCena = nomeCena;
        trocarCena = mudarCena;

        StartCoroutine(trocarMusica());
    }

    IEnumerator trocarMusica()
    {
        for(float volume = volumeMaxMusica; volume >= 0; volume -= 0.1f)
        {
            yield return new WaitForSeconds(0.1f);
            sMusic.volume = volume;
        }
        sMusic.volume = 0;
        sMusic.Pause();
        sMusic.clip = novaMusica;
        
        sMusic.Play();

        for (float volume = 0; volume < volumeMaxMusica; volume += 0.1f)
        {
            yield return new WaitForSeconds(0.1f);
            sMusic.volume = volume;
        }
        sMusic.volume = volumeMaxMusica;

        if(trocarCena == true)
        {
            SceneManager.LoadScene(novaCena);
        }
    }

    public void VerificarQtdObjAudioC()
    {
        audioControllers = GameObject.FindGameObjectsWithTag("AudioController");
        if (audioControllers.Length >= 2)
        {//Ao mudar de cena caso tenha 2 scripts audioController ele deleta um e deixa somente o script vinculado a primeira fase que fica transitando entre as fases
            Destroy(audioControllers[1]);
        }
    }
}
