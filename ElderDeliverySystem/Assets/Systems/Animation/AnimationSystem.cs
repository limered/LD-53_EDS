using SystemBase.Core.GameSystems;
using Systems.Movement;
using UniRx;

namespace Systems.Animation
{
    [GameSystem]
    public class AnimationSystem : GameSystem<SpriteFlipperComponent>
    {
        public override void Register(SpriteFlipperComponent component)
        {
            component.body = component.GetComponent<BodyComponent>();

            SystemUpdate(component)
                .Subscribe(_ => FlipSprite(component))
                .AddTo(component);
        }

        private static void FlipSprite(SpriteFlipperComponent flipper)
        {
            flipper.spriteToFlip.flipX = flipper.body.Velocity.x > 0; 
        }
    }
}