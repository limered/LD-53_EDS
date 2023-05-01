using SystemBase.Core.Components;
using UniRx;

namespace Systems.Lifecycle
{
    [SingletonComponent]
    public class RunningGameComponent : GameComponent
    {
        public float timeInSeconds = 120;
        public FloatReactiveProperty timer = new(0);
    }
}