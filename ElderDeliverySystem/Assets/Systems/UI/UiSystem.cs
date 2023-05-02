using SystemBase.Core.GameSystems;
using SystemBase.GameState.Messages;
using Systems.Lifecycle;
using Systems.Souls;
using UniRx;

namespace Systems.UI
{
    [GameSystem]
    public class UiSystem : GameSystem<UiComponent>
    {
        public override void Register(UiComponent component)
        {
            SharedComponentCollection
                .Subscribe<SoulContainerComponent>(soulContainer =>
                    RegisterToSoulContainerInfo(soulContainer, component))
                .AddTo(component);

            SharedComponentCollection
                .Subscribe<RunningGameComponent>(game => RegisterToTimeUpdates(game, component))
                .AddTo(component);

            MessageBroker.Default.Receive<GameMsgPause>()
                .Subscribe(_ => PauseGame(component))
                .AddTo(component);

            MessageBroker.Default.Receive<GameMsgUnpause>()
                .Subscribe(_ => UnpauseGame(component))
                .AddTo(component);

            MessageBroker.Default.Receive<GameMsgEnd>()
                .Subscribe(_ => ShowEndScreen(component))
                .AddTo(component);
        }

        private void ShowEndScreen(UiComponent component)
        {
            component.endScreen.SetActive(true);
        }

        private static void UnpauseGame(UiComponent uiComponent)
        {
            uiComponent.PauseScreen.SetActive(false);
        }

        private static void PauseGame(UiComponent uiComponent)
        {
            uiComponent.PauseScreen.SetActive(true);
        }

        private static void RegisterToTimeUpdates(RunningGameComponent game, UiComponent uiComponent)
        {
            game.timer
                .Subscribe(t => uiComponent.timer.text = $"Time Left: {t / 60:00}:{t % 60:00}")
                .AddTo(game);
        }

        private static void RegisterToSoulContainerInfo(SoulContainerComponent soulContainer, UiComponent component)
        {
            soulContainer.soulCount
                .CombineLatest(soulContainer.soulsTargetCount, (c, t) => (c, t))
                .Subscribe(tuple =>
                    component.soulsCounter.text =
                        $"Souls: {tuple.c}/{tuple.t} (Management Target)")
                .AddTo(component);

            soulContainer.endScreenMessage
                .Subscribe(m => component.endScreenMessage.text = m)
                .AddTo(component);
        }
    }
}