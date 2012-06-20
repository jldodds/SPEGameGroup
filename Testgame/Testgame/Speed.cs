
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
        int oppSelectedPile;
        Drawable oppSelector;
        int yourSelectedPile;
        Drawable yourSelector;

        public Speed(Card[] deck, Drawable background, Texture2D selector):base(background)
        {
            cards = deck;
            yourStack = new Pile(new Vector2(897, 650));
            opponentStack = new Pile(new Vector2(127, 150));
            lSpitStack = new Pile(new Vector2(217, 400));
            rSpitStack = new Pile(new Vector2(807, 400));
            lGameStack = new Pile(new Vector2(413, 400));
            rGameStack = new Pile(new Vector2(610, 400));

            for (int i = 0; i < yourCards.Length; i++)
            {
                opponentCards[i] = new Pile(new Vector2(opponentStack.position.X + (i + 1) * 154, opponentStack.position.Y));
                yourCards[i] = new Pile(new Vector2(yourStack.position.X - (i + 1) * 154, yourStack.position.Y));
            }

            for (int i = 0; i < cards.Length; i++)
            {
                base.Add(cards[i]);
            }

            oppSelector = new Drawable()
            {
                attributes = new Attributes()
                {
                    texture = selector,
                    position = new Vector2(-100, -100),
                    color = Color.Red,
                    height = 190,
                    width = 140,
                    depth = .01f,
                    rotation = 0
                }
            };

            yourSelector = new Drawable()
            {
                attributes = new Attributes()
                {
                    texture = selector,
                    position = new Vector2(-100, -100),
                    color = Color.LightSkyBlue,
                    height = 190,
                    width = 140,
                    depth = .01f,
                    rotation = 0
                }
            };

            base.Add(oppSelector);
            base.Add(yourSelector);

              
        }

        public enum gameState
        {
            Dealing,
            AskBegin,
            Beginning,
            GamePlay,
            
            PlayingCard
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

            //Display "3"
            Timer stopwatch = new Timer(3);
            base.Add(stopwatch);
            stopwatch.SetTimer(0, 1, delegate() {/*Display "2" */});
            stopwatch.SetTimer(1, 2, delegate() {/*Display "1" */});
            stopwatch.SetTimer(2, 3, delegate() {/*Display "SPEED!" */ BeginGame(); base.RemoveLast(); });
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
                    yourSelector.attributes.position = yourCards[yourSelectedPile].position;
                        oppSelector.attributes.position = opponentCards[oppSelectedPile].position;
                        //if (!ExistMoves()) ReBegin();
                        //if (YouWin()) YourAWinner();
                        //if (OppWin()) OppAWinner();
                    break;
                
                case gameState.PlayingCard:
                    yourSelector.attributes.position = yourCards[yourSelectedPile].position;
                        oppSelector.attributes.position = opponentCards[oppSelectedPile].position;
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
                    if (newState.IsKeyDown(Keys.Left))
                    {
                        if (!oldState.IsKeyDown(Keys.Left))
                        {
                            yourSelectedPile++;
                            if (yourSelectedPile == 5) yourSelectedPile = 0;
                        }
                    }

                    if (newState.IsKeyDown(Keys.Right))
                    {
                        if (!oldState.IsKeyDown(Keys.Right))
                        {
                            yourSelectedPile--;
                            if (yourSelectedPile == -1) yourSelectedPile = 4;
                        }
                    }

                    if (newState.IsKeyDown(Keys.Up))
                    {
                        if (!oldState.IsKeyDown(Keys.Up))
                        {
                            PlayYourCard(yourCards[yourSelectedPile], lGameStack);
                        }
                    }

                    if (newState.IsKeyDown(Keys.Down))
                    {
                        if (!oldState.IsKeyDown(Keys.Down))
                        {
                            PlayYourCard(yourCards[yourSelectedPile], rGameStack);
                        }
                    }

                    if (newState.IsKeyDown(Keys.A))
                    {
                        if (!oldState.IsKeyDown(Keys.A))
                        {
                            oppSelectedPile--;
                            if (oppSelectedPile == -1) oppSelectedPile = 4;
                        }
                    }

                    if (newState.IsKeyDown(Keys.D))
                    {
                        if (!oldState.IsKeyDown(Keys.D))
                        {
                            oppSelectedPile++;
                            if (oppSelectedPile == 5) oppSelectedPile = 0;
                        }
                    }

                    if (newState.IsKeyDown(Keys.W))
                    {
                        if (!oldState.IsKeyDown(Keys.W))
                        {
                            PlayOppCard(opponentCards[oppSelectedPile], lGameStack);
                        }
                    }

                    if (newState.IsKeyDown(Keys.S))
                    {
                        if (!oldState.IsKeyDown(Keys.S))
                        {
                            PlayOppCard(opponentCards[oppSelectedPile], rGameStack);
                        }
                    }

                    if (newState.IsKeyDown(Keys.Space))
                    {
                        if (!oldState.IsKeyDown(Keys.Space))
                        {
                            ReBegin();
                        }
                    }
                    break;

                case gameState.PlayingCard:
                    if (newState.IsKeyDown(Keys.D))
                    {
                        if (!oldState.IsKeyDown(Keys.D))
                        {
                            oppSelectedPile++;
                            if (oppSelectedPile == 5) oppSelectedPile = 0;
                        }
                    }

                    if (newState.IsKeyDown(Keys.A))
                    {
                        if (!oldState.IsKeyDown(Keys.A))
                        {
                            oppSelectedPile--;
                            if (oppSelectedPile == -1) oppSelectedPile = 4;
                        }
                    }

                    if (newState.IsKeyDown(Keys.Left))
                    {
                        if (!oldState.IsKeyDown(Keys.Left))
                        {
                            yourSelectedPile++;
                            if (yourSelectedPile == 5) yourSelectedPile = 0;
                        }
                    }

                    if (newState.IsKeyDown(Keys.Right))
                    {
                        if (!oldState.IsKeyDown(Keys.Right))
                        {
                            yourSelectedPile--;
                            if (yourSelectedPile == -1) yourSelectedPile = 4;
                        }
                    }
                    break;
                   
            }
            if (newState.IsKeyDown(Keys.P))
            {
                if (!oldState.IsKeyDown(Keys.P))
                {
                    Pause();
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

        public void BeginGame()
        {
            speedState = gameState.GamePlay;
            oppSelectedPile = 0;
            yourSelectedPile = 0;
            DrawCard(lSpitStack, lGameStack, 0f);
            DrawCard(rSpitStack, rGameStack, 0f);
        }

        public void PlayYourCard(Pile fromPile, Pile destinationPile)
        {
            if (fromPile.stack.Count == 0) return;
            speedState = gameState.PlayingCard;
            Card c = fromPile.stack.Peek();
            int cv = c.cardValue;
            int value = destinationPile.stack.Peek().cardValue;
            if ((cv == 0 && value == 12) || (cv == 12 && value == 0) || (cv == value + 1 || cv == value - 1))
            {

                Card m = fromPile.Take();
                m.toPile(destinationPile);
                m.tweenerX.Ended += delegate() { speedState = gameState.GamePlay; };
                if (yourStack.stack.Count != 0)
                {
                    if (fromPile.stack.Count == 0)
                        DrawCard(yourStack, fromPile, 0f);
                }
            }
            else speedState = gameState.GamePlay;
        }

        public void PlayOppCard(Pile fromPile, Pile destinationPile)
        {
            if(fromPile.stack.Count == 0) return;
            speedState = gameState.PlayingCard;
            Card c = fromPile.stack.Peek();
            int cv = c.cardValue;
            int value = destinationPile.stack.Peek().cardValue;
            if ((cv == 0 && value == 12) || (cv == 12 && value == 0) || (cv == value + 1 || cv == value - 1))
            {
                Card m = fromPile.Take();
                    m.toPile(destinationPile);
                    m.tweenerX.Ended += delegate() { speedState = gameState.GamePlay; };
                if (opponentStack.stack.Count != 0)
                {
                    if (fromPile.stack.Count == 0)
                        DrawCard(opponentStack, fromPile, .3f);
                }
            }
                else speedState = gameState.GamePlay;
        }

        public bool ExistMoves()
        {
            bool oppMoves = false;
            for (int i = 0; i < opponentCards.Length; i++)
            {
                if (opponentCards[i].stack.Count != 0)
                {
                    int cv = opponentCards[i].stack.Peek().cardValue;
                    int value1 = lGameStack.stack.Peek().cardValue;
                    int value2 = rGameStack.stack.Peek().cardValue;
                    if ((cv == 0 && value1 == 12) || (cv == 12 && value1 == 0) || (cv == value1 + 1 || cv == value1 - 1)) oppMoves = true;
                    if ((cv == 0 && value2 == 12) || (cv == 12 && value2 == 0) || (cv == value2 + 1 || cv == value2 - 1)) oppMoves = true;
                }
            }

            bool yourMoves = false;
            for (int i = 0; i < yourCards.Length; i++)
            {
                if (yourCards[i].stack.Count != 0)
                {
                    int cv = yourCards[i].stack.Peek().cardValue;
                    int value1 = lGameStack.stack.Peek().cardValue;
                    int value2 = rGameStack.stack.Peek().cardValue;
                    if ((cv == 0 && value1 == 12) || (cv == 12 && value1 == 0) || (cv == value1 + 1 || cv == value1 - 1)) yourMoves = true;
                    if ((cv == 0 && value2 == 12) || (cv == 12 && value2 == 0) || (cv == value2 + 1 || cv == value2 - 1)) yourMoves = true;
                }
            }

            return yourMoves || oppMoves;
        }

        public void ReBegin()
        {
            speedState = gameState.Beginning;
            //Display "NO MOVES"


            Timer stopwatch = new Timer(4);
            base.Add(stopwatch);
            stopwatch.SetTimer(3, 1, delegate() {/*Display "3" */});
            stopwatch.SetTimer(0, 2, delegate() {/*Display "2" */});
            stopwatch.SetTimer(1, 3, delegate() {/*Display "1" */});
            stopwatch.SetTimer(2, 4, delegate() {/*Display "SPEED!" */ BeginGame(); base.RemoveLast(); });
        }

        public bool YouWin()
        {
            bool winner = true;
            for (int i = 0; i < yourCards.Length; i++)
            {
                if (yourCards[i].stack.Count != 0) winner = false;
            }
            return winner;
        }

        public bool OppWin()
        {
            bool winner = true;
            for (int i = 0; i < opponentCards.Length; i++)
            {
                if (opponentCards[i].stack.Count != 0) winner = false;
            }
            return winner;
        }

        public void YourAWinner()
        {
            Deal();
        }

        public void OppAWinner()
        {
            Deal();
        }
    }
}

