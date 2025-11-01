using OpenRPReloaded.Enums.Account;
using OpenRPReloaded.Enums.States;
using OpenRPReloaded.Managers;
using OpenRPReloaded.Models;
using OpenRPReloaded.Services;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.World;
using System;
using System.Collections.Generic;

namespace OpenRPReloaded.Frontend
{

    /// <summary>
    /// Representa um jogaodr que nao se autenticou.
    /// </summary>
    public class PlayerAuthSession
    {
        private Player _player;
        private AccountsService _accountsService;

        public event EventHandler Finished;

        public PlayerAuthSession(Player player, AccountsService accountsService)
        {
            _player = player;
            _accountsService = accountsService;
        }
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
                "Parece que ja tens uma conta!\n" +
                "Insere a tua palavra-passe aqui em baixo:";
            }
            else
            {
                loginMessage =
                "{FFFFFF}" +
                $"Bem-vindo ao servidor, {username}\n" +
                "A tua palavra-passe nao está correta\n" +
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

            }
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
            
            //Tentar registrar e guardar o conjunto dos resultados.
            List<AccountCreationResult> result =
                        _accountsService.RegisterAccount(_player.Name, args.InputText);

            //Se a conta nao contem success, entao nao foi criada, ou seja vamos re-abrir o dialogo.
            if (!result.Contains(AccountCreationResult.Success))
            {
                var dialog = CreateRegisterDialog(_player.Name, true, result);
                dialog.Show(_player);
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
                        _accountsService.LoginAccount(_player.Name, args.InputText);

           
            if (result != AccountLoginResult.Success)
            {

                var dialog = CreateLoginDialog(_player.Name, true, result);
                dialog.Show(_player);
            }
            else
            {
                OnAccountCreation();
            }
        }


        public void SendWelcomeMessage()
        {
            for (uint i = 0; i < 32; i++) _player.SendClientMessage("");

            _player.SendClientMessage("Bem-Vindo ao Open RP Reloaded - Feito em C# & .NET");
            _player.SendClientMessage("A carregar os teus dados, espera um pouco...");

            for (uint i = 0; i < 3; i++) _player.SendClientMessage("");
        }


        /// <summary>
        /// Lógica de um jogador que acabou de ligar ao servidor (ConnectedPlayer)
        /// </summary>
        /// <param name="e"> argumentos de evento </param>
        public void Start()
        {
            _player.AuthState = PlayerAuthState.Unauthenticated;

            if (!_accountsService.IsRegistered(_player.Name))
            {
                var dialog = CreateRegisterDialog(_player.Name);
                dialog.Show(_player);
            }
            else
            {
                var dialog = CreateLoginDialog(_player.Name);
                dialog.Show(_player);
            }
        }
        /// <summary>
        /// Implementação do método de criação da conta.
        /// Futuramente usado para invocar um tutorial.
        /// </summary>
        public void OnAccountCreation()
        {
            Account playerAccount = _accountsService.GetAccountWithoutTracking(_player.Name);
            _player.SendPlayerMessageToAll($"O jogador {_player.Name} acabou de registrar-se no servidor! Dêm boas-vindas!");
            _player.SendClientMessage($"ID da conta: {playerAccount.AccountID}");
            _player.SendClientMessage($"Hash: {playerAccount.Password}");
            OnFinish();
        
        }
        /// <summary>
        /// Implementação do método de autenticação com sucesso de uma conta
        /// </summary>
        public void OnFinish()
        {
            _player.AuthState = PlayerAuthState.Authenticated;
            var account = _accountsService.GetAccountWithoutTracking(_player.Name);
            account.LastLogin = DateTime.Now;
            PlayerManager.AddPlayer(_player,account);
            Finished?.Invoke(this,EventArgs.Empty);
        }

    }
}
