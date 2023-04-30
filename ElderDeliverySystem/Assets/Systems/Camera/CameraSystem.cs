using SystemBase.Core.GameSystems;
using Systems.Player;
using UniRx;
using Unity.Mathematics;
using UnityEngine;

namespace Systems.Camera
{
    [GameSystem]
    public class CameraSystem : GameSystem<CameraComponent>
    {
        public override void Register(CameraComponent component)
        {
            SharedComponentCollection
                .Subscribe<PlayerComponent>(p => StartFollowing(p, component))
                .AddTo(component);
        }

        private void StartFollowing(PlayerComponent player, CameraComponent camera)
        {
            SystemUpdate()
                .Subscribe(_ => UpdateCameraPosition(camera, player))
                .AddTo(camera);
        }

        private void UpdateCameraPosition(CameraComponent camera, PlayerComponent player)
        {
            var playerPos = player.transform.position;
            var cameraPos = camera.transform.position;
            var delta = playerPos - cameraPos;
            var delta2D = new Vector2(delta.x, delta.z);
            if (math.abs(delta2D.x) > camera.extents.x)
                cameraPos += Vector3.right * (delta2D.x - math.sign(delta2D.x) * camera.extents.x);
            if (math.abs(delta2D.y) > camera.extents.y)
                cameraPos += Vector3.forward * (delta2D.y - math.sign(delta2D.y) * camera.extents.y);

            camera.transform.position = cameraPos;
        }
    }
}