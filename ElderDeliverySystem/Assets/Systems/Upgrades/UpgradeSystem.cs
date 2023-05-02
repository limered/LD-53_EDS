using System;
using SystemBase.Core.GameSystems;
using Systems.Lifecycle;
using Systems.Player;
using Systems.Souls;
using Systems.Weapons;
using UniRx;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Systems.Upgrades
{
    public enum UpgradeType
    {
        ScytheSpeedX2,
        ScytheSpeedX4,
        ScytheSpeedX8,
        SpeedX2,
        SpeedX4,
        SpeedX8,
        TimeX2,
        TimeX4,
    }
    
    [GameSystem]
    public class UpgradeSystem : GameSystem<UpgradeComponent>
    {
        public override void Register(UpgradeComponent component)
        {
            MessageBroker.Default.Receive<UpgradeMessage>()
                .Subscribe(msg => Upgrade(msg.upgrade))
                .AddTo(component);
        }

        private void Upgrade(UpgradeSO objUpgrade)
        {
            SharedComponentCollection.Get<SoulContainerComponent>().soulCount.Value -= objUpgrade.prize;
            switch (objUpgrade.upgradeType)
            {
                case UpgradeType.ScytheSpeedX2:
                    Object.FindObjectOfType<WeaponComponent>().attackInterval /= 2f;
                    break;
                case UpgradeType.ScytheSpeedX4:
                    Object.FindObjectOfType<WeaponComponent>().attackInterval /= 2f;
                    break;
                case UpgradeType.ScytheSpeedX8:
                    Object.FindObjectOfType<WeaponComponent>().attackInterval /= 2f;
                    break;
                case UpgradeType.SpeedX2:
                    Object.FindObjectOfType<PlayerComponent>().speed += 1;
                    break;
                case UpgradeType.SpeedX4:
                    Object.FindObjectOfType<PlayerComponent>().speed += 1;
                    break;
                case UpgradeType.SpeedX8:
                    Object.FindObjectOfType<PlayerComponent>().speed += 1;
                    break;
                case UpgradeType.TimeX2:
                    Object.FindObjectOfType<RunningGameComponent>().gameDuration *= 2;
                    break;
                case UpgradeType.TimeX4:
                    Object.FindObjectOfType<RunningGameComponent>().gameDuration *= 2;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}