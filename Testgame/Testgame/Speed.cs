
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


                if (i % 4 == 3 && i < 16) cards[i].toPile(rSpitStack, (float)i / 6);
                else if (i % 4 == 2 && i < 16) cards[i].toPile(lSpitStack, (float)i / 6);
                else if (i % 4 == 1 && i < 16) cards[i].toPile(yourStack, (float)i / 6);
                else if (i % 4 == 0 && i < 16) cards[i].toPile(opponentStack, (float)i / 6);
                else if (i % 2 == 1 && i >= 16) cards[i].toPile(yourStack, (float)i / 6);
                else if (i % 2 == 0 && i >= 16) cards[i].toPile(opponentStack, (float)i / 6);
            }
           
        }
    }
}

