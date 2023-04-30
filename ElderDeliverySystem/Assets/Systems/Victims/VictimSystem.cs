using SystemBase.Core.GameSystems;
using SystemBase.Utils;
using Systems.Movement;
using Systems.Player;
using UniRx;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Systems.Victims
{
    [GameSystem(typeof(PlayerControlSystem))]
    public class VictimSystem : GameSystem<VictimComponent>
    {
        public override void Register(VictimComponent component)
        {
            component.BodyComponent = component.GetComponent<BodyComponent>();
            
            SystemUpdate(component)
                .Subscribe(_ => CheckForPlayer(component))
                .AddTo(component);
        }

        private void CheckForPlayer(VictimComponent component)
        {
            if (SharedComponentCollection.TryGet<PlayerComponent>(out var player))
            {
                var distance = player.transform.position - component.transform.position;
                var distance2D = distance.XZ();
                if (distance2D.magnitude < component.minDistanceToPlayer)
                {
                    component.BodyComponent.AddForce(-distance.normalized * component.fleeSpeed);
                    return;
                }
            }
            
            var newDirection = Random.insideUnitCircle.normalized;
            var currentDirection = math.normalize(component.BodyComponent.Velocity.xz);
            var direction = math.lerp(currentDirection, newDirection, component.randomRoamingCoefficient);
            component.BodyComponent.AddForce(new Vector3(direction.x, 0, direction.y) * component.roamingSpeed);
        }
    }
}