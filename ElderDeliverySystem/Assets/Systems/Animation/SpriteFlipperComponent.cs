using SystemBase.Core.Components;
using Systems.Movement;
using UnityEngine;

namespace Systems.Animation
{
    [RequireComponent(typeof(BodyComponent))]
    public class SpriteFlipperComponent : GameComponent
    {
        public BodyComponent body;
        public SpriteRenderer spriteToFlip;
    }
}