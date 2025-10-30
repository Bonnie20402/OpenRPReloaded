using System;
using SampSharp.GameMode.Pools;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;
using SampSharp.GameMode;
using OpenRPReloaded.Enums;
using SampSharp.GameMode.Factories;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Events;
namespace OpenRPReloaded
{
    [PooledType]
    public class Player : BasePlayer
    {
        public override void OnConnected(EventArgs e)
        {


            SetSpawnInfo(NoTeam, (int) SkinID.DJ, new Vector3(0.0, 0.0, 0.0), 0.0f);
            SendClientMessage("Open RP Reloaded - Inicio do desenvolvimento: 30/10/2025");

        }



        public override void OnExitVehicle(PlayerVehicleEventArgs e)
        {
            GameMode.VehicleDestroyer.DestroyVehicle(e.Vehicle.Id);
            SendClientMessage("Carro partido");

         
        }



        [Command("teste",IgnoreCase = true)]
        public void CommandTeste()
        {
            SendClientMessage("ola");
        }



        [Command("aveh")]
        public void CommandAVeh()
        {
            var veh = GameMode.VehicleFactory.Create(VehicleModelType.Taxi, Position + new Vector3(0.0,8.0) ,0f,1,1);
            SendClientMessage("Desfruta do carro novo");

            PutInVehicle(veh);
        }


        

    }

}