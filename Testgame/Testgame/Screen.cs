
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Speed
{
    public class Screen
    {
        public class Node
        {
            public Drawable drawable;
            public Node next;

            // draws drawable
            public void Draw(SpriteBatch spriteBatch, SpriteEffects spriteEffects)
            {
               drawable.Draw(spriteBatch, spriteEffects);
            }

            // updates drawable
            public void Update(GameTime gameTime)
            {
                
                drawable.Update(gameTime);
            }
        }

        public readonly Node first;
        private Node last;
        public virtual bool isPaused { get; set; }

        
        // creates linked list of screens using drawables
        public Screen(Drawable background)
        {
            first = new Node() { drawable = background };
            last = first;
            isPaused = true;
        }

        // adds drawable to screen
        public void Add(Drawable d)
        {
            last.next = new Node() { drawable = d };
            last = last.next;
        }

        // removes last node from linked list
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

        // draws all screens in the linked list
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isPaused) return;
            for (Node i = first; i != null; i = i.next) i.Draw(spriteBatch, SpriteEffects.None);
        }

        // updates all screens in the linked list
        public virtual void Update(GameTime gameTime)
        {
            if (isPaused) return;
            for (Node i = first; i != null; i = i.next) i.Update(gameTime);
        }

        // turns screen on 
        public virtual void TurnOn()
        {
            isPaused = false;
        }
        
        // pauses screen
        public virtual void Pause()
        {
            isPaused = true;
        }

        // turns screen on if prev. paused
        public virtual void Resume()
        {
            isPaused = false;
        }

    }
}

