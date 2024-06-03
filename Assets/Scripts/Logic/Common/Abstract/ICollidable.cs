
using UnityEngine;

namespace Logic.Common
{
    public interface ICollidable
    {
        Vector2 CollideBox { get; }
        CollideLayerType CollideLayer { get; }
        bool IsCollidableBy(CollideLayerType sourceLayerType);
    }
}
