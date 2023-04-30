using SystemBase.Core.Components;

namespace Systems.Weapons
{
    public class WeaponComponent : GameComponent
    {
        public float attackInterval;
        public float damage;
        public float range;
        public float LastAttackTime { get; set; }
    }
}