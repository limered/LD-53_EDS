using SystemBase.Core.Components;

namespace Systems.Upgrades
{
    [SingletonComponent]
    public class UpgradeComponent : GameComponent
    {
        public UpgradeSO[] upgrades;
    }
}