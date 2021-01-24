using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    public AudioSource sMusic; //FONTE DE MUSICA
    public AudioSource sFX; //FONTE DE EFEITOS SONOROS

    [Header("Musicas")]

    public AudioClip musicaTitulo;
    public AudioClip musicaSelecaoFases;
    public AudioClip musicaFases;

    // [Header("Efeitos sonoros")]


    //CONFIGURACOES DOS AUDIOS
    public float volumeMaxMusica;
    private float volumeMaxFX;

    //CONFIGURACOES DA TROCA DE MUSICA
    private AudioClip novaMusica;
    private string novaCena;
    private bool trocarCena;
    void Start()
    {
    }
    private void Awake()
    {

        DontDestroyOnLoad(this.gameObject);

        if(PlayerPrefs.GetInt("valoresIniciais") == 0)
        {
            PlayerPrefs.SetInt("valoresIniciais", 1);
            PlayerPrefs.SetFloat("valorMaxMusica", 1);
            
        }

        //CARREGA AS CONFIGURACOES DE AUDIO
        volumeMaxMusica = PlayerPrefs.GetFloat("valorMaxMusica");
        volumeMaxFX = 1;

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
        
        Debug.Log("Antes de dar o erro");
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


}
