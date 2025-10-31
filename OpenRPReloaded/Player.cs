using OpenRPReloaded.Enums;
using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Factories;
using SampSharp.GameMode.Pools;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;
using System;
namespace OpenRPReloaded
{
    [PooledType]
    public partial class Player : BasePlayer
    {
        


        public override void OnConnected(EventArgs e)
        {


            SetSpawnInfo(NoTeam, (int) SkinID.Andre, new Vector3(0.0,0.0,0.0), 0f);
            SendClientMessage("Open RP Reloaded - Inicio do desenvolvimento: 30/10/2025");


  


        }


        public override void OnSpawned(SpawnEventArgs e)
        {

            Position = new Vector3(1152.9374, -1770.217, 16.59375);

            var msgDlg = new MessageDialog("Teste", "Mensagem", "Sim", "nao");


            msgDlg.Response += (object sender, DialogResponseEventArgs e) =>
            { 
                if (e.DialogButton == DialogButton.Left)
                {
                    SendClientMessage("Sim");
                }
                else SendClientMessage("Nao");
            };

            msgDlg.Show(this);

        }


        public override void OnClickMap(PositionEventArgs e)
        {

            this.Position = e.Position;
            
        }
        public override void OnExitVehicle(PlayerVehicleEventArgs e)
        {
            GameMode.VehicleDestroyer.DestroyVehicle(e.Vehicle);
         
        }



        [Command("pos",IgnoreCase = true)]
        public void CommandTeste()
        {
            string loc = $"Localizacao: new vector3({Position.X},{Position.Y},{Position.Z};";
            SendClientMessage(loc);
            Console.WriteLine(loc);
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