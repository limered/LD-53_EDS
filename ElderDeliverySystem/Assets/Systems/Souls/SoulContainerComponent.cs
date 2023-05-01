using SystemBase.Core.Components;
using UniRx;
using UnityEngine;

namespace Systems.Souls
{
    [SingletonComponent]
    public class SoulContainerComponent : GameComponent
    {
        public IntReactiveProperty allSoulsCount = new(0);
        public IntReactiveProperty soulCount = new(0);
        public int firstTarget = 20;
        public IntReactiveProperty soulsTargetCount = new(0);
        public GameObject soulPrefab;
        public StringReactiveProperty managementMessage = new("");
        public int strikes = 0;
        public StringReactiveProperty endScreenMessage = new("");

        public void Reset()
        {
            allSoulsCount.Value = 0;
            soulCount.Value = 0;
            soulsTargetCount.Value = firstTarget;
            managementMessage.Value = "";
            strikes = 0;
            endScreenMessage.Value = "";
        }
    }
}