using SystemBase.Core.GameSystems;
using Systems.Souls;
using Systems.Upgrades;
using UniRx;
using UnityEngine;

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

        private void GenerateUpgrades(
            UpgradeComponent upgrades, 
            PauseScreenComponent component,
            SoulContainerComponent souls)
        {
            foreach (var upgrade in upgrades.upgrades)
            {
                var upgradeUi = Object.Instantiate(component.upgradePrefab, component.upgradeContainer.transform)
                    .GetComponent<UpgradeUiComponent>();
                upgradeUi.SetUpgrade(upgrade, SharedComponentCollection);
            }
        }

        private void UpdatePauseScreenFromSouls(SoulContainerComponent souls, PauseScreenComponent component)
        {
            souls.managementMessage
                .Subscribe(m => component.managementMessage.text = m)
                .AddTo(component);
            
            SharedComponentCollection
                .Subscribe<UpgradeComponent>(upgrades => GenerateUpgrades(upgrades, component, souls))
                .AddTo(component);
        }
    }
}