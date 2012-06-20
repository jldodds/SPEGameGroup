
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Testgame
{
    class Speed : Screen
    {
        public static Pile yourStack;
        public static Pile opponentStack;
        public static Pile lSpitStack;
        public static Pile rSpitStack;
        public static Pile lGameStack;
        public static Pile rGameStack;
        public static Card[] cards;
        public Pile[] yourCards = new Pile[5];
        public Pile[] opponentCards = new Pile[5];
        KeyboardState oldState;

        public Speed(Card[] deck, Drawable background):base(background)
        {
            cards = deck;
            yourStack = new Pile(new Vector2(897, 650));
            opponentStack = new Pile(new Vector2(127, 150));
            lSpitStack = new Pile(new Vector2(217, 400));
            rSpitStack = new Pile(new Vector2(807, 400));
            lGameStack = new Pile(new Vector2(127, 150));
            rGameStack = new Pile(new Vector2(127, 150));

            for (int i = 0; i < yourCards.Length; i++)
            {
                opponentCards[i] = new Pile(new Vector2(opponentStack.position.X + (i + 1) * 154, opponentStack.position.Y));
                yourCards[i] = new Pile(new Vector2(yourStack.position.X - (i + 1) * 154, yourStack.position.Y));
            }

            for (int i = 0; i < cards.Length; i++)
            {
                base.Add(cards[i]);
            }
              
        }


        public void Deal()
        {
            Commands.Shuffle(cards);
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i].isFaceUp = false;


                if (i % 4 == 3 && i < 16) cards[i].toPile(rSpitStack, (float)i / 13);
                else if (i % 4 == 2 && i < 16) cards[i].toPile(lSpitStack, (float)i / 13);
                else if (i % 4 == 1 && i < 16) cards[i].toPile(yourStack, (float)i / 13);
                else if (i % 4 == 0 && i < 16) cards[i].toPile(opponentStack, (float)i / 13);
                else if (i % 2 == 1 && i >= 16) cards[i].toPile(yourStack, (float)i / 13);
                else if (i % 2 == 0 && i >= 16) cards[i].toPile(opponentStack, (float)i / 13);
            }
           
        }

        public void Begin()
        {
            for (int i = 0; i < 5; i++)
            {
                DrawCard(yourStack, yourCards[i], i * .675f);
                DrawCard(opponentStack, opponentCards[i], i * .675f);
            }
        }

        public void DrawCard(Pile drawPile, Pile destinationPile, float delay)
        {
            Card temp = drawPile.Take();
            temp.Flip(true, delay);
            temp.toPile(destinationPile, delay);
        }

        public override void Update(GameTime gameTime)
        {
            KeyUpdate(gameTime);
            base.Update(gameTime);
        }

        protected void KeyUpdate(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();

            if (newState.IsKeyDown(Keys.Enter))
            {
                if (!oldState.IsKeyDown(Keys.Enter))
                {
                    Deal();
                }
            }

            if (newState.IsKeyDown(Keys.Up))
            {
                if (!oldState.IsKeyDown(Keys.Up))
                {
                    Begin();
                }
            }

            if (newState.IsKeyDown(Keys.Down))
            {
                if (!oldState.IsKeyDown(Keys.Down))
                {
                    Commands.MakePile(cards, new Vector2(300, 300));
                }
            }

            if (newState.IsKeyDown(Keys.A))
            {
                if (!oldState.IsKeyDown(Keys.A))
                {
                    Commands.MakePile(cards, new Vector2(300, 300));
                }
            }

            if (newState.IsKeyDown(Keys.A))
            {
                if (!oldState.IsKeyDown(Keys.A))
                {
                    Commands.MakePile(cards, new Vector2(300, 300));
                }
            }

            if (newState.IsKeyDown(Keys.W))
            {
                if (!oldState.IsKeyDown(Keys.W))
                {
                    Commands.MakePile(cards, new Vector2(300, 300));
                }
            }

            if (newState.IsKeyDown(Keys.S))
            {
                if (!oldState.IsKeyDown(Keys.S))
                {
                    Commands.MakePile(cards, new Vector2(300, 300));
                }
            }

            if (newState.IsKeyDown(Keys.A))
            {
                if (!oldState.IsKeyDown(Keys.A))
                {
                    Commands.MakePile(cards, new Vector2(300, 300));
                }
            }

            

            if (newState.IsKeyDown(Keys.P))
            {
                if (!oldState.IsKeyDown(Keys.P))
                {
                    Pause();
                }
            }

            if (newState.IsKeyDown(Keys.R))
            {
                if (!oldState.IsKeyDown(Keys.R))
                {
                    Resume();
                }
            }

            oldState = newState;
        }

    }
}

