using SystemBase.Core.Components;
using UnityEngine;

namespace Systems.Weapons
{
    public class WeaponComponent : GameComponent
    {
        public float attackInterval;
        public float damage;
        public float range;
        public Animator weaponAnimator;
        public float LastAttackTime { get; set; }
    }
}