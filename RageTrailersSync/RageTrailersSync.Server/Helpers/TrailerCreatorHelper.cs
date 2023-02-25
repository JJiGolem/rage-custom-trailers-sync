using GTANetworkAPI;

namespace RageTrailersSync.Server.Helpers
{
    internal static class TrailerCreatorHelper
    {
        public static VehicleHash GetHashByName(string modelName)
        {
            return (VehicleHash)NAPI.Util.GetHashKey(modelName);
        }
    }
}
