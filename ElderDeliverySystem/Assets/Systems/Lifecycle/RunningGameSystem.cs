using SystemBase;
using SystemBase.Core.GameSystems;
using SystemBase.GameState.Messages;
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
                .Subscribe(_ => ShowStartScreen(component))
                .AddTo(component);

            MessageBroker.Default.Receive<GameMsgStart>()
                .Subscribe(_ => StartGame())
                .AddTo(component);

            MessageBroker.Default.Receive<GameMsgPause>()
                .Subscribe(_ => PauseGame())
                .AddTo(component);

            MessageBroker.Default.Receive<GameMsgUnpause>()
                .Subscribe(_ => UnpauseGame())
                .AddTo(component);
        }

        private void UnpauseGame()
        {
        }

        private void PauseGame()
        {
        }

        private void StartGame()
        {
            var uiComponent = SharedComponentCollection.Get<UiComponent>();
            uiComponent.startScreen.SetActive(false);
            uiComponent.soulsCounter.enabled = true;

            var prefabs = IoC.Game.GetComponent<PrefabComponent>();
            Object.Instantiate(prefabs.prefabs[0]);
            Object.Instantiate(prefabs.prefabs[1]);
        }

        private void ShowStartScreen(RunningGameComponent component)
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