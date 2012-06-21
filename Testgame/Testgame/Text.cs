using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Testgame
{
    class Text : Drawable
    {
        public String content;
        SpriteFont _font;
        public Tweener tweenerA;

        public Text(String stuff, SpriteFont font)
        {
            content = stuff;
            _font = font;
        }

        public override void Draw(SpriteBatch spriteBatch,SpriteEffects spriteEffects)
        {
            if (!isSeeable) return;
            spriteBatch.DrawString(_font, content, attributes.position, attributes.color);
        }

        public void Fade(float d)
        {
            tweenerA = new Tweener(attributes.color.A, 0, d, Actions.LinearMove);
        }

        public override void Update(GameTime gameTime)
        {
            if (tweenerA != null)
            {
                tweenerA.Update(gameTime);
                //attributes.color.A = (byte)tweenerA.Position;
                attributes.color = new Color(attributes.color.R, attributes.color.G, attributes.color.B,(byte) tweenerA.Position);
            }
            base.Update(gameTime);
        }

    }
}
