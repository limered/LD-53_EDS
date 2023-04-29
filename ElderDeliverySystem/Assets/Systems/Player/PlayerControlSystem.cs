using SystemBase.Core.GameSystems;
using UniRx;
using UnityEngine;

namespace Systems.Player
{
    [GameSystem]
    public class PlayerControlSystem : GameSystem<PlayerComponent>
    {
        
        public override void Register(PlayerComponent component)
        {

        }
    }
}