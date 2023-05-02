using SystemBase.Core.GameSystems;
using SystemBase.Utils;
using Systems.Movement;
using Systems.Player;
using Systems.Victims;
using UniRx;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Systems.Souls
{
    [GameSystem]
    public class SoulSystem : GameSystem<SoulContainerComponent, SoulComponent>
    {
        public override void Register(SoulContainerComponent component)
        {
            MessageBroker.Default.Receive<VictimDiedMessage>()
                .Subscribe(msg => SpawnSouls(msg, component))
                .AddTo(component);
        }

        private void SpawnSouls(VictimDiedMessage msg, SoulContainerComponent soulsContainer)
        {
            for (var i = 0; i < msg.soulDropCount; i++)
            {
                var randomPosition = (float2)(Random.insideUnitCircle * 2) + msg.position;
                Object.Instantiate(
                    soulsContainer.soulPrefab,
                    new Vector3(randomPosition.x, 0, randomPosition.y),
                    Quaternion.identity,
                    soulsContainer.transform);
            }
        }

        public override void Register(SoulComponent component)
        {
            component.Body = component.GetComponent<BodyComponent>();

            SystemUpdate(component)
                .Subscribe(_ => CollectIfPlayerNear(component))
                .AddTo(component);
        }

        private void CollectIfPlayerNear(SoulComponent component)
        {
            if (!SharedComponentCollection.TryGet<PlayerComponent>(out var player)) return;

            var distance = player.transform.position - component.transform.position;
            var distance2D = distance.XZ().magnitude;
            if (distance2D < component.vacuumDistance)
                component.Body.AddForce(distance.normalized * component.vacuumForce);

            if (distance2D >= component.collectionDistance) return;
            Object.Destroy(component.gameObject);
            var playerSoulCount = SharedComponentCollection.Get<SoulContainerComponent>();
            playerSoulCount.soulCount.Value++;
            playerSoulCount.allSoulsCount.Value++; 
        }
    }
}