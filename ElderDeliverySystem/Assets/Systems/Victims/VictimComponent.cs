using SystemBase.Core.Components;
using Systems.Movement;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace Systems.Victims
{
    [RequireComponent(typeof(BodyComponent))]
    public class VictimComponent  : GameComponent
    {
        public float minDistanceToPlayer;
        public float fleeSpeed;
        public float roamingSpeed;
        public float randomRoamingCoefficient;

        public FloatReactiveProperty health = new();
        
        public BodyComponent BodyComponent { get;set; }

        public void ReceiveDamage(float damage)
        {
            health.Value -= damage;
        }
    }
}