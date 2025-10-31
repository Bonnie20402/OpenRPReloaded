using OpenRPReloaded.Enums.Account;
using OpenRPReloaded.Enums.States;
using OpenRPReloaded.Models;
using OpenRPReloaded.Services;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;
using System;
using System.Collections.Generic;

namespace OpenRPReloaded.Frontend
{
    public class UnauthenticatedPlayer : Player
    {
        public PlayerAuthState AuthState { get; private set;}
        /// <summary>
        /// Cria um dialogo de login para a conta
        /// </summary>
        /// <param name="username"> O nome da conta</param>
        /// <param name="hasError"> Se já teve erros a criar antes</param>
        /// <param name="output"> O erro da criação posterior, se for o caso </param>
        /// <returns> O dialogo com o texto prontinho ^^</returns>
        private Dialog CreateLoginDialog(string username, bool hasError = false, AccountLoginResult? output = null)
        {
            string loginMessage;
            if (!hasError)
            {
                loginMessage =
                "{FFFFFF}" +
                $"Bem-vindo ao servidor, {username}\n" +
                "Parece que já tens uma conta!\n" +
                "Insere a tua palavra-passe aqui em baixo:";
            }
            else
            {
                loginMessage =
                "{FFFFFF}" +
                $"Bem-vindo ao servidor, {username}\n" +
                "A tua palavra-passe não está correta\n" +
                "Insere a tua palavra-passe aqui em baixo:";

                //TODO: Listar problemas (quando criar oserviço de mensagens)
            }
            //TODO: Criar serviço de mensagens. Para já, fica assim ^^
            var dialog = new InputDialog(
                caption: "Autenticação de conta",
                message: loginMessage,
                isPassword: true,
                button1: "Logar",
                button2: ""
                );
            dialog.Response += OnLoginDialogResponse;
            return dialog;
        }


        /// <summary>
        /// Cria um dialogo de registro para a conta
        /// </summary>
        /// <param name="username"> O nome da (futura) conta</param>
        /// <param name="hasError"> Se já teve erros a criar antes</param>
        /// <param name="outputs"> A lista de erros da criação posterior, se for o caso </param>
        /// <returns> O dialogo com o texto prontinho ^^</returns>
        private Dialog CreateRegisterDialog(string username, bool hasError = false, List<AccountCreationResult> outputs = null)
        {
            string registerMessage;
            if(!hasError)
            {
                registerMessage =
                "{FFFFFF}" +
                $"Bem-vindo ao servidor, {username}\n" +
                "Parece que não tens uma conta. Vamos criar uma!\n" +
                "Insere a tua melhor palavra-passe aqui em baixo:";
            }
            else
            {
                registerMessage =
                "{FFFFFF}" +
                $"Bem-vindo ao servidor, {username}\n" +
                "Parece existiu um ou mais problemas a criar a tua conta\n" +
                "Insere a tua melhor palavra-passe aqui em baixo:";

                //TODO: Listar problemas (quando criar oserviço de mensagens)
            }
                //TODO: Criar serviço de mensagens. Para já, fica assim ^^
            var dialog = new InputDialog(
                caption: "Autenticação de conta",
                message: registerMessage,
                isPassword: true,
                button1: "Registrar",
                button2: ""
                );
            dialog.Response += OnRegisterDialogResponse;
            return dialog;
        }



        /// <summary>
        /// Lógica ao receber uma respsota do dialogo de autenticação (REGISTER)
        /// </summary>
        /// <param name="invoker">Contexto do objeto que invocou</param>
        /// <param name="args">Argumentos de evento</param>
        private void OnRegisterDialogResponse(Object invoker, DialogResponseEventArgs args)
        {
            //Criar o serviço
            
            //Tentar registrar e guardar o conjunto dos resultados.
            List<AccountCreationResult> result =
                        AccountsService.RegisterAccount(Name, args.InputText);

            //Se a conta nao contem success, entao nao foi criada, ou seja vamos re-abrir o dialogo.
            if (!result.Contains(AccountCreationResult.Success))
            {
                var dialog = CreateRegisterDialog(Name, true, result);
                dialog.Show(this);
            }
            else
            {
                OnAccountCreation();
            }
        }

        /// <summary>
        /// Lógica ao receber uma respsota do dialogo de autenticação (LOGIN)
        /// </summary>
        /// <param name="invoker">Contexto do objeto que invocou</param>
        /// <param name="args">Argumentos de evento</param>
        private void OnLoginDialogResponse(Object invoker, DialogResponseEventArgs args)
        {
            

            AccountLoginResult result =
                        AccountsService.LoginAccount(Name, args.InputText);

           
            if (result != AccountLoginResult.Success)
            {

                var dialog = CreateLoginDialog(Name, true, result);
                dialog.Show(this);
            }
            else
            {
                OnAccountCreation();
            }
        }

        /// <summary>
        /// Lógica de um jogador que acabou de ligar ao servidor (ConnectedPlayer)
        /// </summary>
        /// <param name="e"> argumentos de evento </param>
        public override void OnConnected(EventArgs e)
        {
            AuthState = PlayerAuthState.Unauthenticated;

            for (uint i = 0; i < 32; i++) SendClientMessage("");

            SendClientMessage("Bem-Vindo ao Open RP Reloaded - Feito em C# & .NET");
            SendClientMessage("A carregar os teus dados, espera um pouco...");

            for (uint i = 0; i < 3; i++) SendClientMessage("");


            
            //Se não tiver registrado, pedir para criar uma conta
            if (!AccountsService.IsRegistered(Name))
            {
                var dialog = CreateRegisterDialog(Name);
                dialog.Show(this);
            }
            else
            {
                var dialog = CreateLoginDialog(Name);
                dialog.Show(this);
            }
        }



        /// <summary>
        /// Implementação do método de criação da conta.
        /// Futuramente usado para invocar um tutorial.
        /// </summary>
        public override void OnAccountCreation()
        {

            AccountsService accountsService = new AccountsService();
            Account playerAccount = AccountsService.GetAccountWithoutTracking(Name);
            SendClientMessageToAll($"O jogador {Name} acabou de registrar-se no servidor! Dêm boas-vindas!");
            SendClientMessage($"ID da conta: {playerAccount.AccountID}");
            SendClientMessage($"Hash: {playerAccount.Password}");
            OnAuth();
        
        }


        /// <summary>
        /// Implementação do método de autenticação com sucesso de uma conta
        /// </summary>
        public override void OnAuth()
        {
            AuthState = PlayerAuthState.Authenticated;
            var accountService = new AccountsService();
            var account = accountService.GetAccountWithoutTracking(Name);
            account.LastLogin = DateTime.Now;
            SendClientMessageToAll($"O jogador {account.Username} [ID: {account.AccountID}] autenticou-se");
        
        }

        public bool IsAuthenticated()
        {
            return AuthState == PlayerAuthState.Authenticated;
        }
    }
}
