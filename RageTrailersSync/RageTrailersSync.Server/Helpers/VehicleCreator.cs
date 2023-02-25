using GTANetworkAPI;
using System.Threading;
using System.Threading.Tasks;

namespace RageTrailersSync.Server.Helpers
{
    internal static class VehicleCreator
    {
        public static Vehicle CreateWithNumberPlate(VehicleHash hash, Vector3 position, Vector3 rotation, int color1, int color2, string numberplate, uint dimension)
        {
            return CreateWithNumberPlate(hash, position, rotation.Z, color1, color2, numberplate, dimension);
        }

        public static Vehicle CreateWithNumberPlate(uint model, Vector3 position, Vector3 rotation, int color1, int color2, string numberplate, uint dimension)
        {
            return CreateWithNumberPlate(model, position, rotation.Z, color1, color2, numberplate, dimension);
        }

        public static Vehicle CreateWithNumberPlate(VehicleHash hash, Vector3 position, float rotation, int color1, int color2, string numberplate, uint dimension)
        {
            return CreateWithNumberPlate((uint)hash, position, rotation, color1, color2, numberplate, dimension);
        }

        public static Vehicle CreateWithNumberPlate(uint model, Vector3 position, float rotation, int color1, int color2, string numberPlate, uint dimension)
        {
            if (string.IsNullOrWhiteSpace(numberPlate))
            {
                NAPI.Util.ConsoleOutput($"Vehicle with model {model} was not been created ({position})");
                return null;
            }

            return CreateInternal(model, position, rotation, color1, color2, numberPlate, dimension);
        }


        public static Vehicle CreateWithoutNumberPlate(VehicleHash hash, Vector3 position, Vector3 rotation, int color1, int color2, uint dimension)
        {
            return CreateWithoutNumberPlate(hash, position, rotation.Z, color1, color2, dimension);
        }

        public static Vehicle CreateWithoutNumberPlate(uint model, Vector3 position, Vector3 rotation, int color1, int color2, uint dimension)
        {
            return CreateWithoutNumberPlate(model, position, rotation.Z, color1, color2, dimension);
        }

        public static Vehicle CreateWithoutNumberPlate(VehicleHash hash, Vector3 position, float rotation, int color1, int color2, uint dimension)
        {
            return CreateWithoutNumberPlate((uint)hash, position, rotation, color1, color2, dimension);
        }

        public static Vehicle CreateWithoutNumberPlate(uint model, Vector3 position, float rotation, int color1, int color2, uint dimension)
        {
            return CreateInternal(model, position, rotation, color1, color2, "", dimension);
        }

        private static Vehicle CreateInternal(uint model, Vector3 position, float rotation, int color1, int color2, string numberPlate, uint dimension)
        {
            if (Thread.CurrentThread.ManagedThreadId == NAPI.MainThreadId)
            {
                return NAPI.Vehicle.CreateVehicle(model, position, rotation, color1, color2, numberPlate, 255, false, false, dimension);
            }

            TaskCompletionSource<Vehicle> tsc = new TaskCompletionSource<Vehicle>();

            NAPI.Task.Run(() =>
            {
                Vehicle veh = NAPI.Vehicle.CreateVehicle(model, position, rotation, color1, color2, numberPlate, 255, false, false, dimension);
                tsc.SetResult(veh);
            });

            return tsc.Task.GetAwaiter().GetResult();
        }
    }
}
