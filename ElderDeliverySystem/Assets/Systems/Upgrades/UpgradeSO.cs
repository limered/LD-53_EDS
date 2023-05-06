using System.Linq;
using UnityEngine;

namespace Systems.Upgrades
{
    [CreateAssetMenu(fileName = "Upgrade", menuName = "AAA/Upgrade")]
    public class UpgradeSO : ScriptableObject
    {
        public string text;
        public UpgradeType upgradeType;
        public int prize;
        public bool isBought;
        public UpgradeSO[] requiredUpgrades;
        public Sprite image;
        
        public bool CanBeBought()
        {
            return !isBought && requiredUpgrades.All(requiredUpgrade => requiredUpgrade.isBought);
        }
        
        public void Buy()
        {
            isBought = true;
        }
    }
}