using SystemBase.Core.Components;
using Systems.Movement;
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
        
        public BodyComponent BodyComponent { get;set; }
    }
}