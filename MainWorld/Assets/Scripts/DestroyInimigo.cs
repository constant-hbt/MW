using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInimigo : MonoBehaviour
{

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void chamarDestruirInimigo(GameObject objInimigo)
    {
        StartCoroutine(destruirObjInimigo(objInimigo));
    }

    IEnumerator destruirObjInimigo(GameObject obj)
    {
        

        yield return new WaitForSeconds(0.5f);
        Debug.Log("Vou destruir o objeto inimigo");
        Destroy(obj);

    }
}
