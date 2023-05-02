using SystemBase.Core.Components;
using Unity.Mathematics;
using UnityEngine;

namespace Systems.Movement
{
    public class BodyComponent : GameComponent
    {
        public float airFriction;

        public float3 Acceleration { get; set; } = new(0.01f, 0, 0.01f);
        public float3 Velocity { get; set; } = new(0.01f, 0, 0.01f);
        
        public void AddForce(Vector3 force)
        {
            Acceleration += (float3) force * Time.deltaTime;
        }
    }
}