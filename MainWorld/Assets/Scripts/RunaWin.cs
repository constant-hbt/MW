using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunaWin : MonoBehaviour
{
    public GameObject painelFaseConcluida;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Player":
                painelFaseConcluida.SetActive(true);

                break;
        }
    }
}
