using GTANetworkAPI;
using System;

namespace RageTrailersSync.Server
{
    public sealed class Server : Script
    {
        private readonly TrailerCreator _traderCreator;

        public Server()
        {
            IdGenerator idGenerator = new();
            TrailerCreator trailerCreator = new(idGenerator);

            _traderCreator = trailerCreator;
        }

        [Command]
        private void CreateTrailer(Player player, string modelName)
        {
            if (string.IsNullOrWhiteSpace(modelName))
            {
                player.SendChatMessage("Trailer model name was empty");
                return;
            }

            Trailer trailer = _traderCreator.CreateTrailer(modelName, player.Position, player.Rotation, player.Dimension);
        }
    }
}
