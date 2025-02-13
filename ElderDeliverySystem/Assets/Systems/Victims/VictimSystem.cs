using System.Collections;
using SystemBase.Core.GameSystems;
using SystemBase.GameState.States;
using SystemBase.Utils;
using Systems.Movement;
using Systems.Player;
using Systems.Weapons;
using Systems.World;
using UniRx;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Systems.Victims
{
    [GameSystem(typeof(PlayerControlSystem))]
    public class VictimSystem : GameSystem<VictimComponent, WorldComponent>
    {
        private const float FifteenFps = 1f / 15f;

        public override void Register(VictimComponent component)
        {
            component.BodyComponent = component.GetComponent<BodyComponent>();

            SystemUpdate(component)
                .Subscribe(_ => Move(component))
                .AddTo(component);

            MessageBroker.Default.Receive<AttackEvent>()
                .Where(e => CanBeHit(component, e))
                .Subscribe(e => component.ReceiveDamage(e.Damage))
                .AddTo(component);

            component.health.Subscribe(_ => CheckLife(component)).AddTo(component);
        }

        private static bool CanBeHit(VictimComponent component, AttackEvent attackEvent)
        {
            if (component.health.Value <= 0) return false;
            var distanceToAttack = (float2)component.transform.position.XZ() - attackEvent.Position.xy;
            return math.length(distanceToAttack) <= attackEvent.Range;
        }

        private static void CheckLife(VictimComponent component)
        {
            if (!(component.health.Value <= 0)) return;
            
            Object.Destroy(component.gameObject);
            MessageBroker.Default.Publish(new VictimDiedMessage
            {
                soulDropCount = 1,
                position = component.transform.position.XZ()
            });
        }

        private void Move(VictimComponent component)
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

        public override void Register(WorldComponent component)
        {
            SystemUpdate(component)
                .Where(_ => IoC.Game.gameStateContext.CurrentState.Value is Running)
                .Subscribe(SpawnVictim)
                .AddTo(component);
        }

        private void SpawnVictim(WorldComponent world)
        {
            if (Time.deltaTime >= FifteenFps) return;
            if (world.LastSpawnTime + world.spawnInterval > Time.time) return;
            world.LastSpawnTime = Time.time;

            var randomSpawnCenter = new Vector2(
                Random.Range(-world.extents.x, world.extents.x),
                Random.Range(-world.extents.y, world.extents.y));

            Observable.FromCoroutine(_ => SpawnVictims(world.victimPrefab, world, randomSpawnCenter))
                .Subscribe();
        }

        private IEnumerator SpawnVictims(
            GameObject prefab,
            WorldComponent world,
            Vector2 randomSpawnCenter)
        {
            for (var i = 0; i < world.spawnCount; i++)
            {
                var spawnPosition = randomSpawnCenter + Random.insideUnitCircle * world.spawnAreaRadius;

                Object.Instantiate(prefab, new Vector3(spawnPosition.x, 0, spawnPosition.y), Quaternion.identity,
                    world.transform);

                yield return null;
            }
        }
    }
}