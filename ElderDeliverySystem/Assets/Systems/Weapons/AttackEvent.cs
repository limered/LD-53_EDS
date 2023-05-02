using Unity.Mathematics;

namespace Systems.Weapons
{
    public class AttackEvent
    {
        public float Damage { get; set; }
        public float Range { get; set; }
        public float2 Position { get; set; }
    }
}