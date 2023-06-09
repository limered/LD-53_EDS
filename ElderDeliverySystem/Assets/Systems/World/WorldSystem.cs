﻿using SystemBase.Core.GameSystems;
using Systems.Movement;
using Systems.Upgrades;
using UniRx;

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

        private void StartWorldBoundCheck(WorldComponent world, BodyComponent body)
        {
            SystemLateUpdate()
                .Subscribe(_ => CheckWorldBounds(world, body))
                .AddTo(body);
        }

        private static void CheckWorldBounds(WorldComponent world, BodyComponent body)
        {
            var position = body.transform.position;
            var extents = world.extents;
            if (position.x < -extents.x) position.x = -extents.x;
            if (position.x > extents.x) position.x = extents.x;
            if (position.z < -extents.y) position.z = -extents.y;
            if (position.z > extents.y) position.z = extents.y;
            body.transform.position = position;
        }

        public override void Register(UpgradeComponent component)
        {
            foreach (var upgrade in component.upgrades)
            {
                upgrade.isBought = false;
            }
        }
    }
}