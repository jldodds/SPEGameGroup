<<<<<<< HEAD
﻿
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
        public bool selected;


        public Pile(Vector2 p)
        {
            stack = new Stack<Card>();
            position = p;
            selected = false;
        }

        public void Add(Card c)
        {
            stack.Push(c);
            
        }

        public Card Take()
        {
            Card c = stack.Pop();
            return c;
        }
    }
}

=======
﻿
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
        public bool selected;


        public Pile(Vector2 p)
        {
            stack = new Stack<Card>();
            position = p;
            selected = false;
        }

        public void Add(Card c)
        {
            stack.Push(c);
            
        }

        public Card Take()
        {
            Card c = stack.Pop();
            return c;
        }
    }
}

>>>>>>> f230a4a135d6a02843f2d595724e6f46d31ad5da
