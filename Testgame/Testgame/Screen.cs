using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Testgame
{
    class Screen
    {
        public class Node
        {
            public Drawable drawable;
            public Node next;

            public void Draw(SpriteBatch spriteBatch, SpriteEffects spriteEffects)
            {
                
               drawable.Draw(spriteBatch, spriteEffects);
            }

            public void Update(GameTime gameTime)
            {
                drawable.Update(gameTime);
            }
        }

        private Node first;
        private Node last;
        public bool isPaused = true;
        

        public Screen(Drawable background)
        {
            first = new Node() { drawable = background };
            last = first;
        }

        public void Add(Drawable d)
        {
            last.next = new Node() { drawable = d };
            last = last.next;
        }

        public void RemoveLast()
        {
            Node s = first;
            while (s.next != last)
            {
                s = s.next;
            }

            last = s;
            last.next = null;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isPaused) return;
            for (Node i = first; i != null; i = i.next) i.Draw(spriteBatch, SpriteEffects.None);
        }

        public virtual void Update(GameTime gameTime)
        {
            if (isPaused) return;
            
            for (Node i = first; i != null; i = i.next) i.Update(gameTime);
            
        }

        public virtual void TurnOn()
        {
            isPaused = false;
        }
        

        public virtual void Pause()
        {
            isPaused = true;
        }

        public void Resume()
        {
            isPaused = false;
        }
    }
}
