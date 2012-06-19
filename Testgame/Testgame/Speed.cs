﻿
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Testgame
{
    class Speed
    {
        public static Pile yourStack;
        public static Pile opponentStack;
        public static Pile lSpitStack;
        public static Pile rSpitStack;
        //public static Pile lGameStack;
        //public static Pile rGameStack;
        public static Card[] cards;
        public Screen gamePlay;

        public Speed(Card[] deck, Screen s)
        {
            cards = deck;
            yourStack = new Pile(new Vector2(897, 650));
            opponentStack = new Pile(new Vector2(127, 150));
            lSpitStack = new Pile(new Vector2(217, 400));
            rSpitStack = new Pile(new Vector2(807, 400));
            //lGameStack = new Pile(new Vector2(127, 150));
            //rGameStack = new Pile(new Vector2(127, 150));
            gamePlay = s;
            for (int i = 0; i < cards.Length; i++)
            {
                gamePlay.Add(cards[i]);
            }
        }

        public void Deal()
        {
            Commands.Shuffle(cards);
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i].isFaceUp = false;
            }

            for (int i = 0; i < 5; i++)
            {
                cards[i].toPile(lSpitStack);

            }

            for (int i = 5; i < 10; i++)
            {
                cards[i].toPile(rSpitStack);
            }

            for (int i = 10; i < 31; i++)
            {
                cards[i].toPile(yourStack);
            }

            for (int i = 31; i < 52; i++)
            {
                cards[i].toPile(opponentStack);
            }

            
        }
    }
}

