using SystemBase.GameState.Messages;
using UniRx;
using UnityEngine;

namespace Systems.UI
{
    public class RestartGameButton : MonoBehaviour
    {
        public void Restart()
        {
            MessageBroker.Default.Publish(new GameMsgRestart());
        }
    }
}