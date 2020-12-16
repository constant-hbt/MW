using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class UsuarioModel
{

    public int id_usuario;
    public string nome;
    public string usuario;
    public string senha;
    public string email;

    public UsuarioModel() { }

    public UsuarioModel(int p_id_usuario, string p_nome, string p_usuario, string p_senha, string p_email)
    {
        this.id_usuario = p_id_usuario;
        this.nome = p_nome;
        this.usuario = p_usuario;
        this.senha = p_senha;
        this.email = p_email;
    }

    public int Id_usuario
    {
        get { return id_usuario; }
        set { id_usuario = value; }
    }

    public string Nome
    {
        get { return nome; }
        set { nome = value; }
    }

    public string Usuario
    {
        get { return usuario; }
        set { usuario = value; }
    }
    
    public string Senha
    {
        get { return senha; }
        set { senha = value; }
    }

    public string Email
    {
        get { return email; }
        set { email = value; }
    }
}
