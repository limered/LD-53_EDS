using SystemBase.Core.Components;
using Systems.Movement;
using UniRx;
using UnityEngine;

namespace Systems.Player
{
    [SingletonComponent, RequireComponent(typeof(BodyComponent))]
    public class PlayerComponent : GameComponent
    {
        public Vector3ReactiveProperty movementDirection = new();
        public float speed;
        public BodyComponent BodyComponent { get;set; }
    }
}