using System;
using SampSharp.GameMode.Pools;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;

namespace OpenRPReloaded
{
    [PooledType]
    public class Player : BasePlayer
    {
        public override void OnConnected(EventArgs e)
        {
            base.OnConnected(e);

            SendClientMessage("Welcome to blank game mode by your name here!");
        }

        [Command("teste",IgnoreCase = true)]
        public void CommandTeste()
        {
            SendClientMessage("ola");
        }
    }

}