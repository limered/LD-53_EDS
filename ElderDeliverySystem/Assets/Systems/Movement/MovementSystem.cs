using SystemBase.Core.GameSystems;
using UniRx;
using UnityEngine;

namespace Systems.Movement
{
    [GameSystem]
    public class MovementSystem : GameSystem<BodyComponent>
    {
        public override void Register(BodyComponent component)
        {
            SystemFixedUpdate(component)
                .Subscribe(Simulate)
                .AddTo(component);
        }

        private static void Simulate(BodyComponent body)
        {
            body.transform.position += (Vector3) body.Velocity;
            body.Velocity += body.Acceleration;
            body.Velocity *= body.airFriction;
            body.Acceleration = Vector3.zero;
        }
    }
}