﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Testgame
{
    public class Button : Text
    {
        Timer buttonTimer;
        bool selected;
        bool clicked;
        
        // gives buttons colors, position, makes buttontimer
        public Button(String stuff, SpriteFont font, Vector2 position, Color color): base(stuff, font)
        {
            
            attributes.color = color;
            attributes.position = position;
            buttonTimer = new Timer(1);
        }

        // draws buttons if they should be drawn, then makes selected buttons larger than normal
        public override void Draw(SpriteBatch spriteBatch, SpriteEffects spriteEffects)
        {
            if (!isSeeable) return;
            if (selected) spriteBatch.DrawString(_font, content, attributes.position, attributes.color, attributes.rotation, _font.MeasureString(content) / 2, scale * 1.3f, spriteEffects, attributes.depth);
            else base.Draw(spriteBatch, spriteEffects);
        }

        // delegate and event for cases where buttons are selected
        public delegate void ClickHandler();
        public event ClickHandler Clicked;

        // performs button actions .5 seconds after it's clicked, then uses delegates to make events occur
        public void Click()
        {
            clicked = true;
            attributes.color = Color.Orange;
            buttonTimer.SetTimer(0, .5f, delegate() { Clicked(); clicked = false; });
        }

        // 
        public void Remove()
        {
            buttonTimer.RemoveTimers();
            attributes.color = Color.Black;
            clicked = false;
            
        }

        // update method
        public override void Update(GameTime gameTime)
        {
            if (buttonTimer != null) buttonTimer.Update(gameTime);
            base.Update(gameTime);
        }

        // changes boolean about selection to true
        public void Select()
        {
            selected = true;
        }

        // changes boolean about selection to false
        public void DeSelect()
        {
            selected = false;
        }

        // returns wheter or not button is clicked
        public bool isClicked()
        {
            return clicked;
        }

        //
        public void WhenButtonClicked(ClickHandler process)
        {
            Clicked += process;
        }
    }
}
