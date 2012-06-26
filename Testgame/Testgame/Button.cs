using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Testgame
{
    public class Button : Text
    {
        private Timer buttonTimer;
        bool selected;
        bool clicked;
        public Button(String stuff, SpriteFont font, Vector2 position, Color color): base(stuff, font)
        {
            
            attributes.color = color;
            attributes.position = position;
            buttonTimer = new Timer(1);
        }

        public override void Draw(SpriteBatch spriteBatch, SpriteEffects spriteEffects)
        {
            if (!isSeeable) return;
            if (selected) spriteBatch.DrawString(_font, content, attributes.position, attributes.color, attributes.rotation, _font.MeasureString(content) / 2, scale * 1.3f, spriteEffects, attributes.depth);
            else base.Draw(spriteBatch, spriteEffects);
        }

        public delegate void ClickHandler();
        public event ClickHandler Clicked;

        public void Click()
        {
            clicked = true;
            attributes.color = Color.Orange;
            buttonTimer.SetTimer(0, .9f, delegate() { Clicked(); clicked = false; });
        }

        public void Remove()
        {
            if (buttonTimer.timer[0] != null)
            {
                buttonTimer.timer[0] = null;
                attributes.color = Color.Black;
                clicked = false;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (buttonTimer != null) buttonTimer.Update(gameTime);
            base.Update(gameTime);
        }

        public void Select()
        {
            selected = true;
        }

        public void DeSelect()
        {
            selected = false;
        }

        public bool isClicked()
        {
            return clicked;
        }
    }
}
