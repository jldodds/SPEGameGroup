
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Testgame
{
    class Pile
    {
        Stack<Card> stack;
        public readonly Vector2 position;
        public bool drawnTo { get; set; }    // boolean for whether or not drawn to

        // places a stack at the position, pile isn't drawn to
        public Pile(Vector2 p)
        {
            stack = new Stack<Card>();
            position = p;
            drawnTo = false;
        }

        // adds card to top of pile
        public void Add(Card c)
        {
            stack.Push(c);
        }

        // takes card from top of pile
        public Card Take()
        {
            Card c = stack.Pop();
            return c;
        }

        // returns amount of cards in pile
        public int Count()
        {
            return stack.Count;
        }

        // returns card at top of pile
        public Card Peek()
        {
            return stack.Peek();
        }
    }
}
