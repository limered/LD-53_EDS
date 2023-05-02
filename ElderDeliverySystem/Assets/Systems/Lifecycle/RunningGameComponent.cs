using SystemBase.Core.Components;
using UniRx;

namespace Systems.Lifecycle
{
    [SingletonComponent]
    public class RunningGameComponent : GameComponent
    {
        public FloatReactiveProperty timer = new(0);
        public float gameDuration = 30;
    }
}