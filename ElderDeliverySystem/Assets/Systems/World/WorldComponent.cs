using SystemBase.Core.Components;
using Unity.Mathematics;

namespace Systems.World
{
    [SingletonComponent]
    public class WorldComponent : GameComponent
    {
        public float2 extents;
    }
}