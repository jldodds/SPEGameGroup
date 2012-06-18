using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testgame
{
    class Speed
    {
        public static Stack<Card> yourStack = new Stack<Card>();
        public static Stack<Card> opponentStack = new Stack<Card>();
        public static Stack<Card> lSpitStack = new Stack<Card>();
        public static Stack<Card> rSpitStack = new Stack<Card>();
        public static Stack<Card> lGameStack = new Stack<Card>();
        public static Stack<Card> rGameStack = new Stack<Card>();
        public static Card[] cards;

        public Speed(Card[] deck)
        {
            cards = deck;
        }
        
        public static void Deal()
        {
            Commands.Shuffle(cards);

            for (int i = 0; i < 5; i++)
            {
                lSpitStack.Push(cards[i]);
            }

            for (int i = 5; i < 10; i++)
            {
                rSpitStack.Push(cards[i]);
            }

            for (int i = 10; i < 31; i++)
            {
                yourStack.Push(cards[i]);
            }

            for (int i = 31; i < 52; i++)
            {
                opponentStack.Push(cards[i]);
            }

            //lSpitStack
        }
    }
}
