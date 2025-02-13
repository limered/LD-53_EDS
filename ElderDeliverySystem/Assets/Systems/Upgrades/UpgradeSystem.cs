using System;
using SystemBase.Core.GameSystems;
using Systems.Lifecycle;
using Systems.Player;
using Systems.Souls;
using Systems.Weapons;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Systems.Upgrades
{
    public enum UpgradeType
    {
        ScytheSpeed,
        PlayerSpeed,
        PlayTime,
        SoulSpeed,
        CollectionRadius
    }

    [GameSystem]
    public class UpgradeSystem : GameSystem<UpgradeComponent>
    {
        private readonly Action[] _upgradeActions =
        {
            () => Object.FindObjectOfType<WeaponComponent>().attackInterval /= 2f,
            () => Object.FindObjectOfType<PlayerComponent>().speed += 1,
            () => Object.FindObjectOfType<RunningGameComponent>().gameDuration *= 2,
            () =>
            {
                var souls = Object.FindObjectsByType<SoulComponent>(FindObjectsInactive.Include,
                    FindObjectsSortMode.InstanceID);
                foreach (var soul in souls) soul.vacuumForce += 1;
                Object.FindObjectOfType<SoulContainerComponent>()
                    .soulPrefab.GetComponent<SoulComponent>()
                    .vacuumForce += 1;
            },
            () =>
            {
                var souls = Object.FindObjectsByType<SoulComponent>(FindObjectsInactive.Include,
                    FindObjectsSortMode.InstanceID);
                foreach (var soul in souls) soul.vacuumDistance *= 2;
                Object.FindObjectOfType<SoulContainerComponent>()
                    .soulPrefab.GetComponent<SoulComponent>()
                    .vacuumDistance *= 2;
            }
        };

        public override void Register(UpgradeComponent component)
        {
            MessageBroker.Default.Receive<UpgradeMessage>()
                .Subscribe(msg => Upgrade(msg.upgrade))
                .AddTo(component);
        }

        private void Upgrade(UpgradeSO objUpgrade)
        {
            SharedComponentCollection.Get<SoulContainerComponent>().soulCount.Value -= objUpgrade.prize;
            objUpgrade.Buy();
            _upgradeActions[(int)objUpgrade.upgradeType]();
        }
    }
}