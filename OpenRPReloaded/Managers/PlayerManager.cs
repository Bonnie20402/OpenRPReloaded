
using OpenRPReloaded.ExceptionNamespace;
using OpenRPReloaded.Frontend;
using OpenRPReloaded.Models;
using SampSharp.GameMode.World;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenRPReloaded.Managers
{

    /// <summary>
    /// Classe que possui uma lista dos jogadores atualmente autenticados.
    /// </summary>
    public static class PlayerManager
    {
        private static Dictionary<Player, Account> _playersList = new Dictionary<Player, Account>();

        /// <summary>
        /// Adiciona um jogador AUTENTICADO á lista de jogadores interna do servidor.
        /// </summary>
        /// <param name="player"> Instância do Jogador</param>
        /// <param name="account"> Instância da conta</param>
        public static void AddPlayer(Player player, Account account)
        {

            
            if(!player.IsAuthenticated())
            {
                throw new NotAuthenticatedException($"Username {player.Name} não autenticado!");
            }
            else
            {
                player.SendClientMessage("add");
                _playersList.Add(player, account);
            }
                
        }

        /// <summary>
        /// Retorna a lista de jogadores online.
        /// </summary>
        /// <returns></returns>
        public static int GetPlayerCount()
        {
            return _playersList.Count;
        }



        /// <summary>
        /// Remove um jogador da lista dos jogadores autenticados.
        /// </summary>
        /// <param name="player">O jogador a remover</param>
        public static void RemovePlayer(Player player)
        {
            _playersList.Remove(player);
        }

        /// <summary>
        /// Remove uma conta da lista das contas autenticadas.
        /// </summary>
        /// <param name="account">A conta a remover</param>
        public static void RemovePlayer(Account account)
        {
            RemovePlayer(GetPlayer(account));
        }

        /// <summary>
        /// Pega a instância do jogador associada á conta passada por parametro.
        /// Retorna null se estiver offline.
        /// </summary>
        /// <param name="account">A conta</param>
        public static Player GetPlayer(Account account)
        {
            return _playersList.Keys.FirstOrDefault(x => x.Name == account.Username);
        }

       



        
    }
}
