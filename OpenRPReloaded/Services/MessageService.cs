using OpenRPReloaded.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace OpenRPReloaded.Services
{
    /// <summary>
    /// Serviço responsável por gerir mensagens do jogo.
    /// </summary>
    public class MessageService
    {
        /// <summary>
        /// Envia uma mensagem um serviço para o jogador.
        /// </summary>
        /// <param name="player">O jogador que receberá a mensagem.</param>
        /// <param name="message">A mensagem a ser enviada.</param>
        public void SendServiceMessage(Player player, string message)
        {
            player.SendClientMessage(ColorGTA.Red + $"[Sistema] {ColorGTA.White}" + message);
        }


        /// <summary>
        /// Envia uma mensagem a todos os jogadores no racio especificado.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="message"></param>
        public void SendChatMessage(Player player, string message)
        {
            
        }




    }
}
