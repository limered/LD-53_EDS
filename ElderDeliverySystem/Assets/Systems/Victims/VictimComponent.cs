using SystemBase.Core.Components;
using Systems.Movement;
using UnityEngine;

namespace Systems.Victims
{
    [RequireComponent(typeof(BodyComponent))]
    public class VictimComponent  : GameComponent
    {
        public float minDistanceToPlayer;
        
        public BodyComponent BodyComponent { get;set; }
    }
}