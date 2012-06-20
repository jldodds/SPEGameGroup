using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

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
        private bool isPaused;

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

        public void Draw(SpriteBatch spriteBatch)
        {
            for (Node i = first; i != null; i = i.next) i.Draw(spriteBatch, SpriteEffects.None);
        }

        public void Update(GameTime gameTime)
        {
            if (!isPaused)
            {
                for (Node i = first; i != null; i = i.next) i.Update(gameTime);
            }
        }

        public void Pause()
        {
            isPaused = true;
        }

        public void Resume()
        {
            isPaused = false;
        }
    }
}
