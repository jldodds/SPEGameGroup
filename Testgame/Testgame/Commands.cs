using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Testgame
{
    class Commands
    {
        public static void MakePile(Card[] cards, Vector2 position)
        {
            Random random = new Random();
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i].Move(Actions.ExpoMove, position + new Vector2(random.Next(-52 + i,52-i),random.Next(-52+i,52-i)), ((float)i+1)/3);
                cards[i].Rotate(Actions.ExpoMove, (float)(random.NextDouble() - .5)/2, ((float)i+1)/3);
            }
        }

        public static void Shuffle(Card[] cards)
        {
            Random random = new Random();
            int N = cards.Length;
            for (int i = 0; i < N; i++)
            {
                int r = i + (int)(random.NextDouble() * (N - i));
                Card temp = cards[r];
                cards[r] = cards[i];
                cards[i] = temp;
            };
        }


    }
}
