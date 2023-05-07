using SystemBase.Core.GameSystems;
using Systems.Movement;
using Systems.Upgrades;
using UniRx;
using Unity.Mathematics;
using UnityEngine;

namespace Systems.World
{
    [GameSystem]
    public class WorldSystem : GameSystem<BodyComponent, UpgradeComponent>
    {
        public override void Register(BodyComponent component)
        {
            SharedComponentCollection
                .Subscribe<WorldComponent>(wc => StartWorldBoundCheck(wc, component))
                .AddTo(component);
        }

        private void StartWorldBoundCheck(WorldComponent world, Component body)
        {
            SystemLateUpdate()
                .Subscribe(_ => CheckWorldBounds(world, body))
                .AddTo(body);
        }

        private static void CheckWorldBounds(WorldComponent world, Component body)
        {
            var position = body.transform.position;
            var extents = world.extents;
            position.x = math.clamp(position.x, -extents.x, extents.x);
            position.z = math.clamp(position.z, -extents.y, extents.y);
            body.transform.position = position;
        }

        public override void Register(UpgradeComponent component)
        {
            foreach (var upgrade in component.upgrades) upgrade.isBought = false;
        }
    }
}