using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace OpenRPReloaded.Models
{

    /// <summary>
    /// Este modelo representa uma conta no servidor.
    /// No futuro, pretendo adicionar autenticação de 2 fatores via discord.
    /// Ou uma maneira de recuperar a palavra-passe pelo discord.
    /// </summary>
    public class Account
    {

        [Key]
        public Guid AccountID { get; set; }


        [MaxLength(32)]
        [MinLength(4)]
        public string Username { get; set; }

        public string Password { get; set; }

        public DateTime CreationDate { get; set; }


        public Boolean Disabled { get; set; }

     
        public Boolean Banned { get; set; }


        //TODO: No futuro, criar uma lista dos logins da conta.
        [NotMapped]
        public DateTime LastLogin {  get; set; }

        public string? Email { get; set; }


        public Account()
        {

        }

        public Account(Guid guid, string username, string password, DateTime creationDate)
        {
            this.AccountID = guid;
            this.Username = username;
            this.Password = password;
            CreationDate = creationDate;
        }
    }
}
