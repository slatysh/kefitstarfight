using UnityEngine;

namespace Logic.Common
{
    public interface IMoveable
    {
        Vector2 Position { get; set; }
        float Rotation { get; set; }
        float Speed { get; set; }
    }
}
