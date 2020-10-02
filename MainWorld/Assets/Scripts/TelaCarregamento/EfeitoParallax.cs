using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EfeitoParallax : MonoBehaviour
{
    public RawImage imagemCeu;
    public RawImage imagemNuvem;

    public float speedCeu, speedNuvem;


    private void FixedUpdate()
    {
        imagemCeu.uvRect = new Rect(imagemCeu.uvRect.x + speedCeu * Time.deltaTime, imagemCeu.uvRect.y, imagemCeu.uvRect.width, imagemCeu.uvRect.height);
        imagemNuvem.uvRect = new Rect(imagemNuvem.uvRect.x + speedNuvem * Time.deltaTime, imagemNuvem.uvRect.y, imagemNuvem.uvRect.width, imagemNuvem.uvRect.height);
    }
}
