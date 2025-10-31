using System;
using System.ComponentModel.DataAnnotations;
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

        public string Username { get; set; }

        public string Password { get; set; }

        public DateTime CreationDate { get; set; }

        public string? Email { get; set; }


        public Account(Guid guid, string username, string password)
        {
            this.AccountID = guid;
            this.Username = username;
            this.Password = password;
        }
    }
}
