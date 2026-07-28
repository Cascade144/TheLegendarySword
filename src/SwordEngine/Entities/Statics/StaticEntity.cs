using SkiaSharp;
using SwordEngine.Weapons;

namespace SwordEngine.Entities.Statics
{
    /// <summary>
    /// Class is reserved for entities that are not moving.
    /// </summary>
    public class StaticEntity : Entity
    {
        /// <summary>
        ///  Creates a Static Entity and passes variables to the Entity class
        /// </summary>
        /// <param name="handler">The main handler.</param>
        /// <param name="x">This entity's x position in tixels.</param>
        /// <param name="y">This entity's y position in tixels.</param>
        /// <param name="width">The width of this entity.</param>
        /// <param name="height">The height of this entity.</param>
        public StaticEntity(Handler handler, float x, float y, int width, int height) : base(handler, x, y, width, height)
        {
        }

        public override void Die()
        {
            throw new NotImplementedException();
        }

        public override void Render(SKCanvas canvas)
        {
            throw new NotImplementedException();
        }

        public override void SetNewWeapon(Weapon w)
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
