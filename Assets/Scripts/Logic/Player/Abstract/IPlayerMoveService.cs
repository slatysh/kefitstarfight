using UnityEngine;

namespace Logic.Player
{
    public interface IPlayerMoveService
    {
        void Move(Vector2 moveVector);
    }
}
