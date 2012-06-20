
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
        gameState speedState; 

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

        public enum gameState
        {
            Dealing,
            AskBegin,
            Beginning,
            GamePlay
        }


        public void Deal()
        {
            speedState = gameState.Dealing;
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

            cards[51].tweenerX.Ended += delegate() {// "Press Enter to Start
                speedState = gameState.AskBegin;
            };
           
        }

        public void Begin()
        {
            speedState = gameState.Beginning;
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
            if (base.isPaused) return;
            switch (speedState)
            {
                case gameState.Dealing:
                    break;
                case gameState.AskBegin:
                    break;
                case gameState.Beginning:
                    break;
                case gameState.GamePlay:
                    break;
            }

            KeyUpdate(gameTime);
            base.Update(gameTime);
        }

        protected void KeyUpdate(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();

            switch (speedState)
            {
                case gameState.Dealing:
                case gameState.Beginning:
                    DoNothing();
                    break;
                case gameState.AskBegin:
                    if (newState.IsKeyDown(Keys.Enter))
                    {
                        if (!oldState.IsKeyDown(Keys.Enter))
                        {
                            Begin();
                        }
                    }
                    break;
                
                case gameState.GamePlay:
                    break;
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

        public override void TurnOn()
        {
            Deal();
            base.TurnOn();
        }

        public void DoNothing()
        {
            return;
        }
        

    }
}

