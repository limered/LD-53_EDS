using SystemBase.Core.GameSystems;
using SystemBase.GameState.States;
using SystemBase.Utils;
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
                .Where(_ => IoC.Game.gameStateContext.CurrentState.Value is Running)
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