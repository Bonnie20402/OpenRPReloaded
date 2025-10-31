using Microsoft.EntityFrameworkCore;
using OpenRPReloaded.Contexts;
using OpenRPReloaded.Enums.Account;
using OpenRPReloaded.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenRPReloaded.Services
{
    /// <summary>
    /// Responsável pela gestão das contas do servidor.
    /// </summary>
    public class AccountsService 
    {
        private AccountsContext CreateContext() => new AccountsContext();

        public bool IsRegistered(string username)
        {
            using var context = CreateContext();
            return context.Accounts.Any(a => a.Username == username);
        }

        public Account GetAccountWithoutTracking(string username)
        {
            using var context = CreateContext();
            return context.Accounts
                .AsNoTracking()
                .FirstOrDefault(a => a.Username == username);
        }

        public Account GetAccountWithoutTracking(Guid accountId)
        {
            using var context = CreateContext();
            return context.Accounts
                .AsNoTracking()
                .FirstOrDefault(a => a.AccountID == accountId);
        }

        private bool IsValidUsername(string username)
        {
            if (username.Length >= 4 && username.Length <= 31) return true;
            return false;
        }

        private bool IsValidPassword(string password)
        {
            if (password.Length >= 8 && password.Length <= 31) return true;
            return false;
        }

        public List<AccountCreationResult> RegisterAccount(string username, string passwordUnhashed)
        {
            var results = new List<AccountCreationResult>();

            if (!IsValidUsername(username))
                results.Add(AccountCreationResult.Fail_Invalid_Username);
            if (IsRegistered(username))
                results.Add(AccountCreationResult.Fail_Account_Already_Exists);
            if (!IsValidPassword(passwordUnhashed))
                results.Add(AccountCreationResult.Fail_Invalid_Password);

            if (results.Count > 0)
                return results;

            var newAccount = new Account(
                Guid.NewGuid(),
                username,
                BCrypt.Net.BCrypt.EnhancedHashPassword(passwordUnhashed),
                DateTime.Now
            );

            try
            {
                using var context = CreateContext();
                context.Accounts.Add(newAccount);
                context.SaveChanges();
                results.Add(AccountCreationResult.Success);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar conta de {username}: {ex.Message}");
                results.Add(AccountCreationResult.Fail);
            }

            return results;
        }

        public AccountLoginResult LoginAccount(string username, string passwordUnhashed)
        {
            using var context = CreateContext();
            var account = context.Accounts.FirstOrDefault(a => a.Username == username);

            if (account == null)
                return AccountLoginResult.Fail_Due_To_Missing_Account;
            if (account.Disabled)
                return AccountLoginResult.Fail_Due_To_Account_Disabled;
            if (account.Banned)
                return AccountLoginResult.Fail_Due_To_Ban;
            if (!BCrypt.Net.BCrypt.EnhancedVerify(passwordUnhashed, account.Password))
            {
                string hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(passwordUnhashed);
                string hashedAccPassword = account.Password;
                return AccountLoginResult.Fail_Due_To_Wrong_Password;
            }
                

            return AccountLoginResult.Success;
        }

        public AccountPasswordUpdateResult UpdateAccountPassword(Guid accountId, string passwordUnhashed)
        {
            using var context = CreateContext();
            var account = context.Accounts.FirstOrDefault(a => a.AccountID == accountId);

            if (account == null)
                return AccountPasswordUpdateResult.Fail_Due_To_Missing_Account;
            if (account.Disabled)
                return AccountPasswordUpdateResult.Fail_Due_To_Account_Disabled;
            if (BCrypt.Net.BCrypt.Verify(passwordUnhashed, account.Password))
                return AccountPasswordUpdateResult.Fail_Due_To_Same_Password;

            account.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(passwordUnhashed);

            try
            {
                context.SaveChanges();
                return AccountPasswordUpdateResult.Success;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar palavra-passe de {account.Username}: {ex.Message}");
                return AccountPasswordUpdateResult.Fail_Due_To_Database_Error;
            }
        }

        public AccountUsernameUpdateResult UpdateAccountUsername(Guid accountId, string newUsername)
        {
            using var context = CreateContext();

            if (!IsValidUsername(newUsername))
                return AccountUsernameUpdateResult.Fail_Due_To_Invalid_Username;

            if (context.Accounts.Any(a => a.Username == newUsername))
                return AccountUsernameUpdateResult.Fail_Due_To_Username_In_Use;

            var account = context.Accounts.FirstOrDefault(a => a.AccountID == accountId);
            if (account == null)
                return AccountUsernameUpdateResult.Fail_Due_To_Missing_Account;

            account.Username = newUsername;

            try
            {
                context.SaveChanges();
                return AccountUsernameUpdateResult.Success;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao mudar username de {account.AccountID}: {ex.Message}");
                return AccountUsernameUpdateResult.Fail_Due_To_Database_Error;
            }


        }


    }

}
