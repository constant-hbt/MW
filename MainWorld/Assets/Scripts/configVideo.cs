using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class configVideo : MonoBehaviour
{

    public VideoPlayer videoPlay;
    public string videoClipName;
    private string url;

    private void Awake()
    {
        url = System.IO.Path.Combine(Application.streamingAssetsPath, videoClipName);
        videoPlay.url = url;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!videoPlay.isPlaying)
        {
            videoPlay.Play();
        }
        else if (videoPlay.isPlaying)
        {
            return;
        }
    }
}
