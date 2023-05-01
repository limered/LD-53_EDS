using SystemBase.Core.Components;
using Systems.Movement;
using UnityEngine;

namespace Systems.Souls
{
    [RequireComponent(typeof(BodyComponent))]
    public class SoulComponent : GameComponent
    {
        public float collectionDistance;
        public float vacuumDistance;
        public float vacuumForce;
        public BodyComponent Body { get; set; }
    }
}