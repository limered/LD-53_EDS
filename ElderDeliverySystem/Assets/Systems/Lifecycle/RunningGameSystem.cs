using SystemBase;
using SystemBase.Core.GameSystems;
using SystemBase.Core.StateMachineBase;
using SystemBase.GameState.States;
using SystemBase.Utils;
using Systems.UI;
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

            IoC.Game.gameStateContext.CurrentState
                .Where(state => state is Paused)
                .Subscribe(state => ShowPauseMenue(state, component))
                .AddTo(component);

            // ShowEndScreen
        }

        private void ShowPauseMenue(BaseState<Game> state, RunningGameComponent component)
        {
        }

        private void StartGame(BaseState<Game> state, RunningGameComponent component)
        {
            var uiComponent = SharedComponentCollection.Get<UiComponent>();
            uiComponent.startScreen.SetActive(false);
            uiComponent.soulsCounter.enabled = true;

            var prefabs = IoC.Game.GetComponent<PrefabComponent>();
            Object.Instantiate(prefabs.prefabs[0]);
            Object.Instantiate(prefabs.prefabs[1]);
        }

        private void ShowStartScreen(BaseState<Game> state, RunningGameComponent component)
        {
            SharedComponentCollection.Subscribe<UiComponent>(ui =>
                {
                    ui.startScreen.SetActive(true);
                    ui.soulsCounter.enabled = false;
                })
                .AddTo(component);
        }
    }
}