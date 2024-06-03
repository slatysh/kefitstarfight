#if DEBUG_COLLIDE
using System.Collections.Generic;
using UnityEngine;
#endif

namespace Logic.Common
{
    public interface ICollideService
    {
#if DEBUG_COLLIDE
        List<Vector2> DebugVertices { get; }
#endif
    }
}
