using GTANetworkAPI;

namespace RageTrailersSync.Server
{
    class Trailer
    {
        public uint Id { get; set; }
        public string ModelName { get; set; }
        public Vehicle ServerVehicle { get; set; }
        public Vehicle Truck { get; set; }

        public bool IsAttached => Truck is not null;
    }
}
