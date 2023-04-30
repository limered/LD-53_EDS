using SystemBase.Core.GameSystems;
using SystemBase.Utils;
using UniRx;
using UnityEngine;

namespace Systems.Weapons
{
    [GameSystem]
    public class WeaponSystem : GameSystem<WeaponComponent>
    {
        public override void Register(WeaponComponent component)
        {
            SystemUpdate(component)
                .Subscribe(Attack)
                .AddTo(component);
        }

        private void Attack(WeaponComponent weapon)
        {
            if (weapon.LastAttackTime + weapon.attackInterval > Time.time) return;
            weapon.LastAttackTime = Time.time;
            MessageBroker.Default.Publish(new AttackEvent
            {
                Damage = weapon.damage,
                Range = weapon.range,
                Position = weapon.transform.position.XZ()
            });
        }
    }
}