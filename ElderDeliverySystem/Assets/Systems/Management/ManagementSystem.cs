using System;
using System.Collections.Generic;
using SystemBase.Core.GameSystems;
using SystemBase.GameState.Messages;
using SystemBase.Utils;
using Systems.Souls;
using Systems.World;
using UniRx;
using Unity.Mathematics;
using UnityEngine;

namespace Systems.Management
{
    [GameSystem]
    public class ManagementSystem : GameSystem<WorldComponent>
    {
        private List<IDisposable> _disposables = new();

        private readonly string[] _endScreenMessages =
        {
            "You managed to not kill anyone! Congratulations!",
            "You succumbed to the pressure of management! Sorry..."
        };

        private readonly string[] _managementMessages =
        {
            "Management is happy with your delivery! Next Target INCREASED!",
            "Management is not happy with your delivery! First STRIKE!",
            "Management is not happy with your delivery! Second STRIKE!",
            "Management is not happy with your delivery! Third STRIKE! You're FIRED!",
            "Management is not happy with your delivery! Fourth STRIKE! You're FIRED!",
        };

        public override void Register(WorldComponent component)
        {
            SharedComponentCollection
                .Subscribe<SoulContainerComponent>(souls => SetupManagement(souls, component))
                .AddTo(component);
        }

        private void SetupManagement(SoulContainerComponent souls, WorldComponent world)
        {
            souls.soulsTargetCount.Value = souls.firstTarget;

            MessageBroker.Default.Receive<GameMsgUnpause>()
                .Subscribe(_ => SetupNewTarget(souls))
                .AddTo(world);

            MessageBroker.Default.Receive<GameMsgPause>()
                .Subscribe(_ => SubtractTarget(souls))
                .AddTo(world);
        }

        private void SubtractTarget(SoulContainerComponent souls)
        {
            if (souls.soulCount.Value < souls.soulsTargetCount.Value)
            {
                souls.strikes++;
                PrintNegativeMessage(souls);
            }
            else
            {
                souls.managementMessage.Value = _managementMessages[0];
                souls.soulCount.Value = math.max(souls.soulCount.Value - souls.soulsTargetCount.Value, 0);
            }
        }

        private void PrintNegativeMessage(SoulContainerComponent souls)
        {
            souls.managementMessage.Value = _managementMessages[souls.strikes];
        }

        private void SetupNewTarget(SoulContainerComponent souls)
        {
            if (souls.managementMessage.Value.Equals(_managementMessages[0]))
                souls.soulsTargetCount.Value += (int)(souls.allSoulsCount.Value * 0.25f);

            if (souls.strikes != 3) return;
            
            souls.endScreenMessage.Value = _endScreenMessages[1];
            if (souls.allSoulsCount.Value == 0)
                souls.endScreenMessage.Value = _endScreenMessages[0];
            
            MessageBroker.Default.Publish(new GameMsgEnd());
            Debug.Log(IoC.Game.gameStateContext.CurrentState.Value);
        }
    }
}