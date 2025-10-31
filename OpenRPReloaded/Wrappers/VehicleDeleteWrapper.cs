using SampSharp.Core.Natives.NativeObjects;
using SampSharp.GameMode.World;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenRPReloaded.Wrappers
{
    /// <summary>
    /// Isto vai ser removido no futuro se eu simplesmente for um cego.
    /// Eu não achei nenhuma maneira de apagar um veiculo pela framework GameMode, apenas a das Entities...
    /// </summary>

    public class VehicleDeleteWrapper
    {
        [NativeMethod]
        public virtual bool DestroyVehicle(int vehicleid)
        {
            throw new NativeNotImplementedException();
        }

        public virtual void DestroyVehicle(BaseVehicle vehicle)
        {
            DestroyVehicle(vehicle.Id);
        }

    }
}
