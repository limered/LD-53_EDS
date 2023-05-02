using System;
using System.Collections.Generic;
using SystemBase;
using SystemBase.Core.GameSystems;
using SystemBase.GameState.Messages;
using SystemBase.GameState.States;
using SystemBase.Utils;
using Systems.Player;
using Systems.Souls;
using Systems.UI;
using Systems.World;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Systems.Lifecycle
{
    [GameSystem]
    public class RunningGameSystem : GameSystem<RunningGameComponent>
    {
        private readonly List<IDisposable> _disposables = new();

        public override void Register(RunningGameComponent component)
        {
            IoC.Game.gameStateContext.CurrentState
                .Where(state => state is StartScreen)
                .Subscribe(_ => ShowStartScreen())
                .AddTo(component);

            MessageBroker.Default.Receive<GameMsgStart>()
                .Subscribe(_ => StartGame(component))
                .AddTo(component);

            MessageBroker.Default.Receive<GameMsgUnpause>()
                .Subscribe(_ => UnpauseGame(component))
                .AddTo(component);

            MessageBroker.Default.Receive<GameMsgRestart>()
                .Subscribe(_ => RestartGame())
                .AddTo(component);
        }

        private void UnpauseGame(RunningGameComponent runningGameComponent)
        {
            runningGameComponent.timer.Value = runningGameComponent.gameDuration;
        }

        private void StartGame(RunningGameComponent runningGameComponent)
        {
            var uiComponent = SharedComponentCollection.Get<UiComponent>();
            uiComponent.startScreen.SetActive(false);
            uiComponent.soulsCounter.enabled = true;
            uiComponent.timer.enabled = true;

            var prefabs = IoC.Game.GetComponent<PrefabComponent>();
            Object.Instantiate(prefabs.prefabs[0]);
            Object.Instantiate(prefabs.prefabs[1]);

            runningGameComponent.timer.Value = runningGameComponent.gameDuration;
            _disposables
                .Add(SystemUpdate(runningGameComponent)
                    .Where(_ => IoC.Game.gameStateContext.CurrentState.Value is Running)
                    .Where(_ => runningGameComponent.timer.Value > 0)
                    .Subscribe(_ => UpdateTimer(runningGameComponent)));
        }

        private static void UpdateTimer(RunningGameComponent runningGameComponent)
        {
            runningGameComponent.timer.Value -= Time.deltaTime;
            if (runningGameComponent.timer.Value <= 0)
                MessageBroker.Default.Publish(new GameMsgPause());
        }

        private void RestartGame()
        {
            _disposables.ForEach(d => d.Dispose());
            _disposables.Clear();
            
            var world = Object.FindObjectOfType<WorldComponent>();
            if (world) Object.Destroy(world.gameObject);
            var player = Object.FindObjectOfType<PlayerComponent>();
            if (player) Object.Destroy(player.gameObject);

            SharedComponentCollection.Get<SoulContainerComponent>().Reset();
            SharedComponentCollection.Get<UiComponent>().ShowStartScreen();
        }

        private void ShowStartScreen()
        {
            _disposables
                .Add(SharedComponentCollection
                    .Subscribe<UiComponent>(ui => { ui.ShowStartScreen(); }));
        }
    }
}