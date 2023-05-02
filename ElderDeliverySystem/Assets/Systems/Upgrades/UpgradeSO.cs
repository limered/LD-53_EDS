using UnityEngine;

namespace Systems.Upgrades
{
    [CreateAssetMenu(fileName = "Upgrade", menuName = "AAA/Upgrade")]
    public class UpgradeSO : ScriptableObject
    {
        public string text;
        public UpgradeType upgradeType;
        public int prize;
        public Sprite image;
        public bool isBought;
    }
}