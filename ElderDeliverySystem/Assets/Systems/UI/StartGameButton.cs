using SystemBase.GameState.Messages;
using UniRx;
using UnityEngine;

namespace Systems.UI
{
    public class StartGameButton : MonoBehaviour
    {
        public void StartGame()
        {
            MessageBroker.Default.Publish(new GameMsgStart());
        }
    }
}