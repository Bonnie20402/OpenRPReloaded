using OpenRPReloaded.Contexts;
using OpenRPReloaded.Enums;
using OpenRPReloaded.Models;
using SampSharp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenRPReloaded.Services
{
    /// <summary>
    /// Responsavel pela gestão das contas do servidor.
    /// </summary>
    public class AccountsService
    {

        private AccountsContext _accountsContext;

        public AccountsService(AccountsContext accountsContext)
        {
            _accountsContext = accountsContext;
        }


        /// <summary>
        /// Retorna se um jogador tem uma conta no servidor ou não.
        /// </summary>
        /// <param name="username"> O username do jogador. </param>
        /// <returns></returns>
        public bool IsRegistered(string username)
        {
            return _accountsContext.Accounts.Any(a => a.Username == username);
        }


        /// <summary>
        /// Registra uma conta, e guarda-a na BD.
        /// </summary>
        /// <param name="username">O username</param>
        /// <param name="passwordUnhashed">A password (unhashed)</param>
        /// <returns></returns>
        public List<AccountCreationResult> RegisterAccount(string username, string passwordUnhashed)
        {
            List<AccountCreationResult> results = new List<AccountCreationResult>();

            // O username só pode ter até 31 letras.
            if (username.Length > 31)
            {
                results.Add(AccountCreationResult.Fail_Username_Too_Long);
            }
            //O username deve ter mais que 3 letras
            if(username.Length < 4)
            {
                results.Add(AccountCreationResult.Fail_Username_Too_Short);
            }
            //O username não deve estar registrado
            if(IsRegistered(username))
            {
                results.Add(AccountCreationResult.Fail_Account_Already_Exists);
            }
            // A password deve conter mais de 7 letras
            if(passwordUnhashed.Length < 8)
            {
                results.Add(AccountCreationResult.Fail_Password_Too_Short);
            }
            //A password deve conter menos que 33 letras
            if(passwordUnhashed.Length > 32)
            {
                results.Add(AccountCreationResult.Fail_Password_Too_Long);
            }
            //Se existir erros até aqui, retornar
            if (results.Count > 0) return results;


            //Criar a conta
            string hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(passwordUnhashed);
            Guid accountID = Guid.NewGuid();
            DateTime currentTime = DateTime.Now;
            Account newAccount = new Account(accountID, username, hashedPassword, currentTime);

            //Guardar a conta na BD
            _accountsContext.Add(newAccount);
            try
            {
                _accountsContext.SaveChanges();
                results.Add(AccountCreationResult.Success);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar a conta de {username}: {ex.Message} " );
                results.Add(AccountCreationResult.Fail);
            }

            return results;
           
        }


        public
    }
}
