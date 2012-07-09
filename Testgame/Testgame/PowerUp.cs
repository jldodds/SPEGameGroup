using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Testgame
{
    class PowerUp : Drawable
    {
        public readonly ParticleEngine engine;
        public delegate void PowerAction(Player player);
        public event PowerAction Played;
        Random random;
        bool isOn;

        public PowerUp(Color color, List<Texture2D> particles, bool onOff)
        {
            engine = new ParticleEngine(particles, attributes.position, new Vector2(150, 150), attributes.depth - .001f, color);
            random = new Random();
            isOn = onOff;
        }

        public void WhenPlayed(PowerAction action)
        {
            Played += action;
        }

        public void HitPlayer(Player player)
        {
            if (Played != null) Played(player);
        }

        public override void Update(GameTime gameTime)
        {
            if (isOn)
            {
                engine.attributes.depth = attributes.depth;
                float x = attributes.position.X + (float)(random.NextDouble() - .5) * attributes.width;
                float y = attributes.position.Y + (float)(random.NextDouble() - .5) * attributes.height;
                engine.attributes.position = new Vector2(x, y);
                engine.Update(gameTime);
                base.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, SpriteEffects spriteEffects)
        {
            if (isSeeable) engine.Draw(spriteBatch, spriteEffects);
            base.Draw(spriteBatch, spriteEffects);
        }
    }
}
