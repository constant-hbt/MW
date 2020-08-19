using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PainelEntrarFase : MonoBehaviour
{
    // Start is called before the first frame update

    public RectTransform rectTransform;

    private void Awake()
    {
        
    }
    void Start()
    {
        
    }
    private void OnEnable()
    {
        Debug.Log("Iniciando script do painelEntrarFase");
        rectTransform.localPosition = new Vector4(1, 2,3,4);//posX = 1, posY = 2, Height = 3 , Width = 4;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
