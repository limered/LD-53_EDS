using SystemBase;
using SystemBase.Core.GameSystems;
using SystemBase.Core.StateMachineBase;
using SystemBase.GameState.Messages;
using SystemBase.GameState.States;
using SystemBase.Utils;
using UniRx;
using UnityEngine;

namespace Systems.Lifecycle
{
    [GameSystem]
    public class RunningGameSystem : GameSystem<RunningGameComponent>
    {
        public override void Register(RunningGameComponent component)
        {
            IoC.Game.gameStateContext.CurrentState
                .Where(state => state is StartScreen)
                .Subscribe(state => ShowStartScreen(state, component))
                .AddTo(component);
            
            IoC.Game.gameStateContext.CurrentState
                .Where(state => state is Running)
                .Subscribe(state => StartGame(state, component))
                .AddTo(component);
            
            // ShowEndScreen
        }

        private void StartGame(BaseState<Game> state, RunningGameComponent component)
        {
            // Init World etc..
            var prefabs = IoC.Game.GetComponent<PrefabComponent>();
            Object.Instantiate(prefabs.prefabs[0]);
            Object.Instantiate(prefabs.prefabs[1]);
        }

        private void ShowStartScreen(BaseState<Game> state, RunningGameComponent component)
        {
            MessageBroker.Default.Publish(new GameMsgStart());
        }
    }
}