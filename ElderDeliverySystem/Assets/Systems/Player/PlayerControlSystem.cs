using SystemBase.CommonSystems.control;
using SystemBase.Core.GameSystems;
using Systems.Movement;
using Systems.World;
using UniRx;
using Unity.Mathematics;
using UnityEngine;

namespace Systems.Player
{
    [GameSystem(typeof(WorldSystem))]
    public class PlayerControlSystem : GameSystem<PlayerComponent>
    {
        public override void Register(PlayerComponent component)
        {
            component.BodyComponent = component.GetComponent<BodyComponent>();
            
            SystemUpdate(component)
                .Subscribe(ControlPlayer)
                .AddTo(component);
        }

        private void ControlPlayer(PlayerComponent player)
        {
            var stickControl = SharedComponentCollection.Get<ControlledByPlayerComponent>();
            
            var direction = Vector3.zero;
            if (Input.GetKey("w")) direction += Vector3.forward;
            if (Input.GetKey("s")) direction += Vector3.back;
            if (Input.GetKey("a")) direction += Vector3.left;
            if (Input.GetKey("d")) direction += Vector3.right;
            direction += new Vector3(stickControl.MovementAmount.x, 0, stickControl.MovementAmount.y);
            direction.Normalize();

            Animate(player, direction);
            
            player.movementDirection.Value = direction;

            player.BodyComponent.AddForce(direction * player.speed);
        }

        private static void Animate(PlayerComponent player, Vector3 direction)
        {
            if(direction == Vector3.zero) player.animatorComponent.Play("Death_Idle");

            var dir = math.sign(direction.x);
            if(direction.z > 0) player.animatorComponent.Play("Death_Walk_Back");
            else if(direction.z < 0 || math.abs(dir) > 0) player.animatorComponent.Play("Death_Walk");
        }
    }
}