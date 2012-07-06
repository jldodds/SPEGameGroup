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
        ParticleEngine engine;
        public delegate void PowerAction(Player player);
        public event PowerAction Played;

        public PowerUp(Color color, List<Texture2D> particles)
        {
            engine = new ParticleEngine(particles, attributes.position, new Vector2(20, 20), attributes.depth, color);
        }

        public void WhenPlayed(PowerAction action)
        {
            Played += action;
        }

        public void HitPlayer(Player player)
        {
            Played(player);
        }

        public override void Update(GameTime gameTime)
        {
            engine.attributes.depth = attributes.depth;
            engine.attributes.position = attributes.position;
            base.Update(gameTime);
        }



    }
}
