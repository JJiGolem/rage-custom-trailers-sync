using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RageTrailersSync.Client.Helpers
{
    internal static class VehicleHelper
    {
        public static bool IsDriver(RAGE.Elements.Vehicle vehicle, RAGE.Elements.Player player)
        {
            return GetDriver(vehicle) == player;
        }

        public static RAGE.Elements.Player GetDriver(RAGE.Elements.Vehicle vehicle)
        {
            int driverHandle = vehicle.GetPedInSeat(-1, 0);
            return RAGE.Elements.Entities.Players.All.FirstOrDefault(p => p.Handle == driverHandle);
        }

        public static RAGE.Elements.Vehicle GetTrailer(RAGE.Elements.Vehicle vehicle)
        {
            return GetVehicleAtHandle(GetTrailerHandle(vehicle));
        }

        public static int GetTrailerHandle(RAGE.Elements.Vehicle vehicle)
        {
            int trailerHandle = 0;
            vehicle.GetTrailerVehicle(ref trailerHandle);
            return trailerHandle;
        }

        public static RAGE.Elements.Vehicle GetVehicleAtHandle(int handle)
        {
            return RAGE.Elements.Entities.Vehicles.All.FirstOrDefault(v => v.Handle == handle);
        }
    }
}
