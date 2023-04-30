using SystemBase.Core.Components;
using Unity.Mathematics;

namespace Systems.Camera
{
    [SingletonComponent]
    public class CameraComponent : GameComponent
    {
        public float2 extents;
    }
}