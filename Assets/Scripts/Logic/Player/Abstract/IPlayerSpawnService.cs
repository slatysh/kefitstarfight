using System;

namespace Logic.Player
{
    public interface IPlayerSpawnService
    {
        event Action<PlayerModel> OnSpawn;
        void Spawn();
    }
}
