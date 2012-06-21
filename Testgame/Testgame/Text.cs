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
       
        public float scale;

        public Text(String stuff, SpriteFont font)
        {
            content = stuff;
            _font = font;
            scale = 1;
        }

        public override void Draw(SpriteBatch spriteBatch,SpriteEffects spriteEffects)
        {
            if (!isSeeable) return;
            spriteBatch.DrawString(_font, content, attributes.position, attributes.color, attributes.rotation, _font.MeasureString(content)/2, scale, spriteEffects, attributes.depth);
        }

       

        

    }
}
