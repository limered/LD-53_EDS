using SystemBase.Core.Components;
using Unity.Mathematics;
using UnityEngine;

namespace Systems.World
{
    [SingletonComponent]
    public class WorldComponent : GameComponent
    {
        public float2 extents;
        public GameObject victimPrefab;
        public float spawnInterval;
        public float LastSpawnTime { get; set; }
        public float shortestFrameTime = float.MaxValue;
    }
}