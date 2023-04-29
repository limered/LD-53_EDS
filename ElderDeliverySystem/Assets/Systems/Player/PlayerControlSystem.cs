using SystemBase.Core.GameSystems;
using Systems.Movement;
using UniRx;
using UnityEngine;

namespace Systems.Player
{
    [GameSystem]
    public class PlayerControlSystem : GameSystem<PlayerComponent>
    {
        
        public override void Register(PlayerComponent component)
        {
            component.BodyComponent = component.GetComponent<BodyComponent>();
            
            SystemUpdate(component)
                .Subscribe(ControlPlayer)
                .AddTo(component);
        }

        private static void ControlPlayer(PlayerComponent player)
        {
            var direction = Vector3.zero;
            if (Input.GetKey("w"))
            {
                direction += Vector3.forward;
            }
            if (Input.GetKey("s"))
            {
                direction += Vector3.back;
            }
            if (Input.GetKey("a"))
            {
                direction += Vector3.left;
            }
            if (Input.GetKey("d"))
            {
                direction += Vector3.right;
            }
            direction.Normalize();
            player.movementDirection.Value = direction;
            
            player.BodyComponent.AddForce(player.movementDirection.Value * player.speed);
        }
    }
}