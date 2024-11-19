﻿using MeuSiteMVC.Enums;
using System.ComponentModel.DataAnnotations;

namespace MeuSiteMVC.Models
{
    public class UsuarioModel
    {

        public int id { get; set; }

        [Required(ErrorMessage = "Digite o nome do usuario")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o login do usuario")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite o e-mail do usuario")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é valido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite a senha do usuario")]
        public string Senha { get; set; }

        public PerfilEnum Perfil { get; set; }

        public DateTime DataCadastro { get; set; }
        
        public DateTime? DataAtualizacao { get; set; }

        

    }
}
