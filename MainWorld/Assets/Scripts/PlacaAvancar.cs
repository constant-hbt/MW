using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacaAvancar : MonoBehaviour
{
    public GameObject objSetaFrente;
    public GameObject objVerificado;
    public bool estadoAlterado = false;
    public void MudarEstado()
    {
        if (!estadoAlterado)
        {
            objSetaFrente.SetActive(false);
            objVerificado.SetActive(true);
        }
    }
}
