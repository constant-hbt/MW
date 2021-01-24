using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PainelConfigSom : MonoBehaviour
{
    private AudioController _audioController;

    public Slider volumeMusica;
    // Start is called before the first frame update
    void Start()
    {
        _audioController = FindObjectOfType(typeof(AudioController)) as AudioController;
        volumeMusica.value = _audioController.volumeMaxMusica;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void alterarVolumeMusica()
    {
        float tempVolume = volumeMusica.value;
        _audioController.volumeMaxMusica = tempVolume;

        _audioController.sMusic.volume = tempVolume;

        PlayerPrefs.SetFloat("valorMaxMusica", tempVolume);
    }
}
