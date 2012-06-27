
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Testgame
{
    class Timer : Drawable
    {
        Tweener[] timer;
        
        // constructor, allows for array of timers to be built
        public Timer(int x)
        {
            timer = new Tweener[x];
        }

        // uses tweeners to tell when times are begun and ended, semi-roundabout
        public void SetTimer(int timerNumber, float endTime, Tweener.EndHandler whenEnded)
        {
            timer[timerNumber] = new Tweener(0, 1, endTime, Actions.LinearMove);
            timer[timerNumber].Ended += whenEnded;
        }
      
        // overrides drawable's draw method so that nothing is ever drawn
        public override void Draw(SpriteBatch spriteBatch, SpriteEffects spriteEffects)
        {
            return;
        }

        // if the timer exists, updates it based on gameTime
        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < timer.Length; i++)
            {
                if(timer[i] != null) timer[i].Update(gameTime);
            }
        }

        // removes timers
        public void RemoveTimers()
        {
            for (int i = 0; i < timer.Length; i++)
            {
                timer[i] = null;
            }
        }
    }
}
