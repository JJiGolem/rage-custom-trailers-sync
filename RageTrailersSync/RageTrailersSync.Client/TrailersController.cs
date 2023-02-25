using RAGE;
using RAGE.Elements;
using RageTrailersSync.Client.Helpers;
using System.Collections.Generic;

namespace RageTrailersSync.Client
{
    internal class TrailersController
    {
        private Vehicle _myAttachedTrailer;

        public TrailersController()
        {
            Events.Tick += Tick;
        }

        private void Tick(List<Events.TickNametagData> _)
        {
            Vehicle truck = Client.LocalPlayer.Vehicle;
            if (truck == null || truck.DoesExist() == false)
                return;

            if (VehicleHelper.IsDriver(truck, Client.LocalPlayer) == false)
                return;

            CheckAttachTrailerToTruck(truck);
        }

        public void CheckAttachTrailerToTruck(Vehicle truck)
        {
            var trailerVehicle = VehicleHelper.GetTrailer(truck);

            if (trailerVehicle == null || trailerVehicle.IsLocal)
                return;

            if (_myAttachedTrailer != trailerVehicle)
            {
                _myAttachedTrailer = trailerVehicle;
                Events.CallRemote("Server:Trailers:AttachedTrailer", truck, trailerVehicle);
            }
        }
    }
}
