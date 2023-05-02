using SystemBase.Core.Components;
using TMPro;
using UnityEngine;

namespace Systems.UI
{
    public class PauseScreenComponent: GameComponent
    {
        public TextMeshProUGUI managementMessage;
        public GameObject upgradePrefab;
        public GameObject upgradeContainer;
    }
}