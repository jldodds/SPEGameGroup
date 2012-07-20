using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Speed
{
    class PowerUp : Drawable
    {
        public readonly ParticleEngine engine;
        public delegate void PowerAction(Player player);
        public event PowerAction Played;
        Random random;
        bool isOn;

        // constructor for powerups which makes instance of particle engine associated with it, a random number, and turns powerup off
        public PowerUp(Color color, List<Texture2D> particles, bool onOff)
        {
            engine = new ParticleEngine(particles, attributes.position, new Vector2(150, 150), attributes.depth - .001f, color);
            random = new Random();
            isOn = onOff;
        }

        // when a powerup is "played," adds the event undergone to events
        public void WhenPlayed(PowerAction action)
        {
            Played += action;
        }

        // if a player plays a card on a powerup, event plaed is called and action is taken
        public void HitPlayer(Player player)
        {
            if (Played != null) Played(player);
        }

        // overrides drawable update method to update attributes of powerup, such as position and depth
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

        // overrides drawable draw method to draw the powerup
        public override void Draw(SpriteBatch spriteBatch, SpriteEffects spriteEffects)
        {
            if (isSeeable) engine.Draw(spriteBatch, spriteEffects);
            base.Draw(spriteBatch, spriteEffects);
        }
    }
}
