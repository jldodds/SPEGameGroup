using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Testgame
{
    class Check 
    {
/*        int[] toggled;
        int[] moduloToggled;
        Drawable _checkMark;
        Button[] buttons;

        public Check(Drawable checkmark, Button[] button, int numberofButtons) : 
        {
            toggled = new int[numberofButtons];
            moduloToggled = new int[numberofButtons];
            _checkMark = checkmark;
            buttons = button;
        }

        public void Toggle()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].isClicked())
                {
                    toggled[i]++;
                    moduloToggled[i] = toggled[i] % 2;
                }
            }
        }

       /* public void Draw(SpriteBatch spBatch, SpriteEffects spEffects)
        {
            for (int i = 0; i <= buttons.Length; i++)
            {
                if (moduloToggled[i] == 1)
                {
                    _checkMark.attributes.position = new Vector2(buttons[i].attributes.position.X + 300, buttons[i].attributes.position.Y);
                    _checkMark.isSeeable = true;
                }
            }
            base.Draw(spBatch, spEffects);
        }

        public void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }*/
    }
}
