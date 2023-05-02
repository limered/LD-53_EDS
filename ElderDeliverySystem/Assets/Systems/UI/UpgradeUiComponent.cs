using System;
using SystemBase.Core.Components;
using Systems.Souls;
using Systems.Upgrades;
using TMPro;
using UniRx;
using UnityEngine.UI;

namespace Systems.UI
{
    public class UpgradeUiComponent : GameComponent
    {
        public TextMeshProUGUI prize;
        public TextMeshProUGUI description;
        private UpgradeSO _upgrade;
        public bool CanBeBought => _sharedComponentCollection.Get<SoulContainerComponent>().soulCount.Value >= _upgrade.prize;
        private ISharedComponentCollection _sharedComponentCollection;
        private Button _button; 

        public void SetUpgrade(UpgradeSO upgrade, ISharedComponentCollection sharedComponentCollection)
        {
            _upgrade = upgrade;
            _sharedComponentCollection = sharedComponentCollection;
            prize.text = upgrade.prize.ToString();
            description.text = upgrade.text;
            _button = GetComponentInChildren<Button>();
        }

        public void Buy()
        {
            if(_upgrade.isBought || !CanBeBought) return;
            
            _upgrade.isBought = true;
            MessageBroker.Default.Publish(new UpgradeMessage { upgrade = _upgrade });
        }

        private void Update()
        {
            _button.interactable = CanBeBought && !_upgrade.isBought;
        }
    }
}