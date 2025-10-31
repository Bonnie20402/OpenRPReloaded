using System;
using OpenRPReloaded.Wrappers;
using SampSharp.Core.Natives.NativeObjects;
using SampSharp.GameMode;
using SampSharp.GameMode.Factories;

namespace OpenRPReloaded
{
    public class GameMode : BaseMode
        
    {
        public static BaseVehicleFactory VehicleFactory { get; set; }

        public static VehicleDeleteWrapper VehicleDestroyer { get; set; }

        protected override void OnInitialized(EventArgs e)
        {


            VehicleFactory = new BaseVehicleFactory(this);
            VehicleDestroyer = NativeObjectProxyFactory.CreateInstance<VehicleDeleteWrapper>();
            base.OnInitialized(e);

            Console.WriteLine("\n----------------------------------");
            Console.WriteLine(" Open RP Reloaded - Feito em C#     ");
            Console.WriteLine("----------------------------------\n");

            SetGameModeText("Open RP Reloaded - C# & .NET <3");
           
        }
    }
}