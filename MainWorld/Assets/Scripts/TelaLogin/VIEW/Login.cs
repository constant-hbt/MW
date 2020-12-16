using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Login : MonoBehaviour
{
    private LoginController _loginController;

    public TMP_InputField inpUsuario;
    public TMP_InputField inpSenha;

    private void Start()
    {
        _loginController = FindObjectOfType(typeof(LoginController)) as LoginController;
    }

    public void btnEntrar()
    {
        string usuario = inpUsuario.text;
        string senha = inpSenha.text;

        if(usuario != "" && senha != "")
        {
            _loginController.ChamarValidarUsuario(usuario, senha);
        }
    }
}
