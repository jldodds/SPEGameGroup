
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
        public Stack<Card> stack;
        public Vector2 position;

        public Pile(Vector2 p)
        {
            stack = new Stack<Card>();
            position = p;
        }

        public void Add(Card c)
        {
            stack.Push(c);
        }

        public Card Take()
        {
            return stack.Pop();
        }
    }
}

