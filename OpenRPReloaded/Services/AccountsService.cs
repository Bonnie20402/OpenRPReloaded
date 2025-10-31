using OpenRPReloaded.Contexts;
using OpenRPReloaded.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenRPReloaded.Services
{
    /// <summary>
    /// Responsavel pela gestão das contas do servidor.
    /// </summary>
    public class LoginService
    {

        private AccountsContext _accountsContext;

        public LoginService(AccountsContext accountsContext)
        {
            _accountsContext = accountsContext;
        }


        /// <summary>
        /// Retorna se um jogador tem uma conta no servidor ou não.
        /// </summary>
        /// <param name="username"> O username do jogador. </param>
        /// <returns></returns>
        public static bool IsRegistered(string username)
        {
            Account account = _ac
        }
    }
}
