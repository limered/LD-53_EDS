using SystemBase.Core.GameSystems;
using Systems.Souls;
using UniRx;

namespace Systems.UI
{
    [GameSystem]
    public class PauseScreenSystem : GameSystem<PauseScreenComponent>
    {
        public override void Register(PauseScreenComponent component)
        {
            SharedComponentCollection
                .Subscribe<SoulContainerComponent>(souls => UpdatePauseScreenFromSouls(souls, component))
                .AddTo(component);
        }

        private void UpdatePauseScreenFromSouls(SoulContainerComponent souls, PauseScreenComponent component)
        {
            souls.managementMessage
                .Subscribe(m => component.managementMessage.text = m)
                .AddTo(component);
        }
    }
}