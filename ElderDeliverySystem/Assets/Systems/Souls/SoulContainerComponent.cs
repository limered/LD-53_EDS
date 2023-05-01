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
        public IntReactiveProperty soulsTargetCount = new(0);
        public GameObject soulPrefab;
    }
}