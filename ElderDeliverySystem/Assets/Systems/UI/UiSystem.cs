using SystemBase.Core.GameSystems;
using Systems.Souls;
using UniRx;

namespace Systems.UI
{
    [GameSystem]
    public class UiSystem : GameSystem<UiComponent>
    {
        public override void Register(UiComponent component)
        {
            SharedComponentCollection
                .Subscribe<SoulContainerComponent>(soulContainer => RegisterToSoulContainerInfo(soulContainer, component))
                .AddTo(component);
        }

        private static void RegisterToSoulContainerInfo(SoulContainerComponent soulContainer, UiComponent component)
        {
            soulContainer.soulCount
                .Subscribe(c => component.soulsCounter.text = $"Souls: {c}/{soulContainer.soulsTargetCount.Value} (Management Target)")
                .AddTo(component);
        }
    }
}