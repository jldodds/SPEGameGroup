using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Testgame
{
    public class Text : Drawable
    {
        protected readonly String content;   // words of the text
        protected readonly SpriteFont _font; // text's font

        public Vector2 scale { get; set; }

        public float height
        {
            // gets scaled version of text's height
            get
            {
                return scale.Y * _font.MeasureString(content).Y;
            }
            set
            {
                scale = new Vector2(value/ _font.MeasureString(content).Y , value / _font.MeasureString(content).Y);
            }
        }

        public float width
        {
            // gets scaled version of text's width
            get
            {
                return scale.X * _font.MeasureString(content).X;
            }
            set
            {
                scale = new Vector2(value / _font.MeasureString(content).X, value / _font.MeasureString(content).X);
            }
        }

        // constructs the text based on words, font, and gives it a true scaling
        public Text(String stuff, SpriteFont font)
        {
            content = stuff;
            _font = font;
            scale = new Vector2(1, 1);
        }

        // draws the string if it's seeable based on attributes
        public override void Draw(SpriteBatch spriteBatch,SpriteEffects spriteEffects)
        {
            if (!isSeeable) return;
            spriteBatch.DrawString(_font, content, attributes.position, attributes.color, attributes.rotation, _font.MeasureString(content)/2, scale, spriteEffects, attributes.depth);
        }

       

        

    }
}
