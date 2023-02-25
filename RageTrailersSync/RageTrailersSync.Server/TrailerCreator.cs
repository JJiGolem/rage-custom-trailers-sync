using GTANetworkAPI;
using RageTrailersSync.Server.Helpers;

namespace RageTrailersSync.Server
{
    class TrailerCreator
    {
        private readonly IdGenerator _idGenerator;

        public TrailerCreator(IdGenerator idGenerator)
        {
            _idGenerator = idGenerator;
        }

        public Trailer CreateTrailer(string modelName, Vector3 position, Vector3 rotation, uint dimension)
        {
            VehicleHash modelHash = TrailerCreatorHelper.GetHashByName(modelName);
            Vehicle trailerVehicle = VehicleCreator.CreateWithoutNumberPlate(modelHash, position, rotation, 0, 0, dimension);
            Trailer trailer = new()
            {
                Id = _idGenerator.Generate(),
                ModelName = modelName,
                ServerVehicle = trailerVehicle
            };

            return trailer;
        }
    }
}
