using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alavanca : MonoBehaviour
{
    public Animator gradeAnimator;
    public GameObject alavancaDestravada;
    public GameObject alavancaTravada;
    void Start()
    {
        alavancaTravada.SetActive(true);
        alavancaDestravada.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Player":
                alavancaTravada.SetActive(false);
                alavancaDestravada.SetActive(true);
                gradeAnimator.SetTrigger("subirGrade");
                break;
        }
    }
}
