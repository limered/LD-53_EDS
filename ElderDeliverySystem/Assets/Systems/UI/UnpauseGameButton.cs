using SystemBase.GameState.Messages;
using UniRx;
using UnityEngine;

namespace Systems.UI
{
    public class UnpauseGameButton : MonoBehaviour
    {
        public void Unpause()
        {
            MessageBroker.Default.Publish(new GameMsgUnpause());
        }
    }
}