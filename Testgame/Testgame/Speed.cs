
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
        public gameState speedState;
        int oppSelectedPile;
        Drawable oppSelector;
        int yourSelectedPile;
        Drawable yourSelector;
        public bool playAgain = false;
        SpriteFont _font;
        public bool isHalted = false;
        YouPenaltyState youPenalty;
        OppPenaltyState oppPenalty;

        public Speed(Card[] deck, Drawable background, Texture2D selector, SpriteFont font):base(background)
        {
            cards = deck;
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i].attributes.position = new Vector2(-100, 100);
                cards[i].isFaceUp = false;
            }
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
                    depth = .11f,
                    rotation = 0
                }
            };

            base.Add(oppSelector);
            base.Add(yourSelector);

            _font = font;
            youPenalty = YouPenaltyState.None;
            oppPenalty = OppPenaltyState.None;
        }

        public enum gameState
        {
            Dealing,
            AskBegin,
            Beginning,
            GamePlay,
            ReBeginning,
            PlayingCard,
            Winner,
            PlayAgain,
        }

        public enum YouPenaltyState
        {
            YouScrewedUp,
            None
        }

        public enum OppPenaltyState
        {
            OppScrewedUp,
            None
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
            Text begin = new Text("Press Enter to Start", _font)
            {
                attributes = new Attributes()
                {
                    color = Color.Yellow,
                    position = new Vector2(512, 400),
                    rotation = 0
                },
                scale = new Vector2(.1f,.1f)
            };
            cards[51].tweenerX.Ended += delegate()
            {
                base.Add(begin);
                speedState = gameState.AskBegin;
            };
           
        }

        public void Begin()
        {
            base.RemoveLast();
            speedState = gameState.Beginning;
            for (int i = 0; i < 5; i++)
            {
                DrawCard(yourStack, yourCards[i], i * .675f);
                DrawCard(opponentStack, opponentCards[i], i * .675f);
            }

            
            Timer stopwatch = new Timer(3);
            base.Add(stopwatch);
            Text three = new Text("3", _font) { attributes = new Attributes() { color = Color.Yellow, position = new Vector2(512, 400) } };
            base.Add(three);
            three.Fade(1);
            three.tweenerA.Ended += delegate() { three.isSeeable = false; };
            stopwatch.SetTimer(0, 1, delegate()
            {
                Text two = new Text("2", _font) { attributes = new Attributes() { color = Color.Yellow, position = new Vector2(512, 400) } };
                base.Add(two);
                two.Fade(1);
                two.tweenerA.Ended += delegate() { two.isSeeable = false; };
            });
            stopwatch.SetTimer(1, 2, delegate()
            {
                Text one = new Text("1", _font) { attributes = new Attributes() { color = Color.Yellow, position = new Vector2(512, 400) } };
                base.Add(one);
                one.Fade(1);
                one.tweenerA.Ended += delegate() { one.isSeeable = false; };
            });
            stopwatch.SetTimer(2, 3, delegate()
            {
                Text title = new Text("SPEED!", _font) { attributes = new Attributes() { color = Color.Yellow, position = new Vector2(512, 400) } };
                base.Add(title);
                title.Fade(.5f);
                title.tweenerA.Ended += delegate() { title.isSeeable = false; BeginGame(); };
            });
        }

        public void DrawCard(Pile drawPile, Pile destinationPile, float delay)
        {
            if (destinationPile.drawnTo) return;
            destinationPile.drawnTo = true;
            Card temp = drawPile.Take();
            temp.Flip(true, delay);
            temp.toPile(destinationPile, delay);
            temp.tweenerX.Ended += delegate() { destinationPile.drawnTo = false; };
        }

        public override void Update(GameTime gameTime)
        {
            if (base.isPaused) return;
            if (isHalted) return;
            switch (speedState)
            {
                case gameState.Dealing:
                    break;
                case gameState.AskBegin:
                    break;
                case gameState.Beginning:
                    break;
                case gameState.GamePlay:
                    if(youPenalty != YouPenaltyState.YouScrewedUp) yourSelector.attributes.position = yourCards[yourSelectedPile].position;
                    if(oppPenalty != OppPenaltyState.OppScrewedUp) oppSelector.attributes.position = opponentCards[oppSelectedPile].position;
                        if (YouWin()) YourAWinner();
                        else if (OppWin()) OppAWinner();
                        
                        else if (!ExistMoves()) ReBegin();
                    break;
                
                case gameState.ReBeginning:
                case gameState.PlayingCard:
                    if (youPenalty != YouPenaltyState.YouScrewedUp) yourSelector.attributes.position = yourCards[yourSelectedPile].position;
                    if (oppPenalty != OppPenaltyState.OppScrewedUp) oppSelector.attributes.position = opponentCards[oppSelectedPile].position;
                    break;
                case gameState.Winner:
                    break;
                case gameState.PlayAgain:
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
                case gameState.Winner:
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
                    #region GameKeys
                    if (youPenalty != YouPenaltyState.YouScrewedUp)
                    {
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
                    }

                    if (oppPenalty != OppPenaltyState.OppScrewedUp)
                    {
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
                    }
                    
                    break;
                    #endregion
                case gameState.ReBeginning:
                case gameState.PlayingCard:
                    #region Selector Keys
                    if (oppPenalty != OppPenaltyState.OppScrewedUp)
                    {
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
                    }
                    if (youPenalty != YouPenaltyState.YouScrewedUp)
                    {
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
                    }
                    break;
                    #endregion
                case gameState.PlayAgain:
                    break;
            }
            

            oldState = newState;
        }

        public override void Resume()
        {
            isHalted = false;
            base.Resume();
        }

        public override void TurnOn()
        {
            Deal();
            base.TurnOn();
        }

        public void Halt()
        {
            isHalted = true;
        }

        public void DoNothing()
        {
            return;
        }

        public void BeginGame()
        {
            Timer timer = new Timer(1);
            base.Add(timer);
            timer.SetTimer(0, .4f, delegate() { speedState = gameState.GamePlay; });
            DrawCard(lSpitStack, lGameStack, 0f);
            DrawCard(rSpitStack, rGameStack, 0f);
        }

        public void PlayYourCard(Pile fromPile, Pile destinationPile)
        {
            if (fromPile.stack.Count == 0)
            {
                return;
            }
            if (fromPile.drawnTo) return;
            speedState = gameState.PlayingCard;
            Card c = fromPile.stack.Peek();
            int cv = c.cardValue;
            int value = destinationPile.stack.Peek().cardValue;
            if ((cv == 0 && value == 12) || (cv == 12 && value == 0) || (cv == value + 1 || cv == value - 1))
            {

                Card m = fromPile.Take();
                m.toPile(destinationPile);
                m.tweenerX.Ended += delegate()
                {
                    speedState = gameState.GamePlay;
                    if (yourStack.stack.Count != 0)
                    {
                        if (fromPile.stack.Count == 0)
                            DrawCard(yourStack, fromPile, 0f);
                    }
                };
            }
            else
            {
                speedState = gameState.GamePlay;
                YouPenalty();
            }
        }

        public void PlayOppCard(Pile fromPile, Pile destinationPile)
        {
            if(fromPile.stack.Count == 0) return;
            if (fromPile.drawnTo) return;
            speedState = gameState.PlayingCard;
            Card c = fromPile.stack.Peek();
            int cv = c.cardValue;
            int value = destinationPile.stack.Peek().cardValue;
            if ((cv == 0 && value == 12) || (cv == 12 && value == 0) || (cv == value + 1 || cv == value - 1))
            {
                Card m = fromPile.Take();
                m.toPile(destinationPile);
                m.tweenerX.Ended += delegate()
                {
                    speedState = gameState.GamePlay;
                    if (opponentStack.stack.Count != 0)
                    {
                        if (fromPile.stack.Count == 0)
                            DrawCard(opponentStack, fromPile, .3f);
                    }
                };

            }
            else
            {
                speedState = gameState.GamePlay;
                OppPenalty();
            }
        }

        public void YouPenalty()
        {
            youPenalty = YouPenaltyState.YouScrewedUp;
            Card c = yourCards[yourSelectedPile].stack.Peek();
            c.attributes.color = Color.Red;
            Timer timer = new Timer(1);
            base.Add(timer);
            timer.SetTimer(0, 1, delegate() { c.attributes.color = Color.White; youPenalty = YouPenaltyState.None; });
        }

        public void OppPenalty()
        {
            oppPenalty = OppPenaltyState.OppScrewedUp;
            Card c = opponentCards[oppSelectedPile].stack.Peek();
            c.attributes.color = Color.Red;
            Timer timer = new Timer(1);
            base.Add(timer);
            timer.SetTimer(0, 1, delegate() { c.attributes.color = Color.White; oppPenalty = OppPenaltyState.None; });
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
            speedState = gameState.ReBeginning;
            Timer watch = new Timer(1);
            base.Add(watch);
            watch.SetTimer(0, 1, delegate()
            {
                Text nomove = new Text("No Moves", _font) { attributes = new Attributes() { color = Color.Yellow, position = new Vector2(512, 400) },
                scale = new Vector2(.5f,.5f)};
                base.Add(nomove);
                nomove.Move(Actions.LinearMove, nomove.attributes.position, 1);
                nomove.tweenerX.Ended += delegate() { nomove.isSeeable = false; };
            });

            if (lSpitStack.stack.Count != 0)
            {
                Timer stopwatch = new Timer(5);
                base.Add(stopwatch);
                
                stopwatch.SetTimer(0, 3, delegate()
                {
                    Text two = new Text("2", _font) { attributes = new Attributes() { color = Color.Yellow, position = new Vector2(512, 400) } };
                    base.Add(two);
                    two.Fade(1);
                    two.tweenerA.Ended += delegate() { two.isSeeable = false; };
                });
                stopwatch.SetTimer(1, 4, delegate()
                {
                    Text one = new Text("1", _font) { attributes = new Attributes() { color = Color.Yellow, position = new Vector2(512, 400) } };
                    base.Add(one);
                    one.Fade(1);
                    one.tweenerA.Ended += delegate() { one.isSeeable = false; };
                });
                stopwatch.SetTimer(2, 5, delegate()
                {
                    Text title = new Text("SPEED!", _font) { attributes = new Attributes() { color = Color.Yellow, position = new Vector2(512, 400) } };
                    base.Add(title);
                    title.Fade(.5f);
                    title.tweenerA.Ended += delegate() { title.isSeeable = false; BeginGame(); };
                });
                stopwatch.SetTimer(3,2, delegate()
                {
                    Text three = new Text("3", _font) { attributes = new Attributes() { color = Color.Yellow, position = new Vector2(512, 400) } };
                base.Add(three);
                three.Fade(1);
                three.tweenerA.Ended += delegate() { three.isSeeable = false; };
                });
            }

            else
            {
                int yourCount = 0;
                int oppCount = 0;
                for (int i = 0; i < yourCards.Length; i++)
                {
                    yourCount += yourCards[i].stack.Count;
                    oppCount += opponentCards[i].stack.Count;
                }
                yourCount += yourStack.stack.Count;
                oppCount += opponentStack.stack.Count;
                if (oppCount > yourCount) YourAWinner();
                else if (yourCount > oppCount) OppAWinner();
                else if (yourCount == oppCount) Tie();
            }
        }

        public bool YouWin()
        {
            bool winner = true;
            for (int i = 0; i < yourCards.Length; i++)
            {
                if (yourCards[i].stack.Count != 0) winner = false;
            }
            if (yourStack.stack.Count != 0) winner = false;
            return winner;
        }

        public bool OppWin()
        {
            bool winner = true;
            for (int i = 0; i < opponentCards.Length; i++)
            {
                if (opponentCards[i].stack.Count != 0) winner = false;
            }
            if (opponentStack.stack.Count != 0) winner = false;
            return winner;
        }

        public void YourAWinner()
        {
            speedState = gameState.Winner;
            Text winner = new Text("Winner!!!", _font)
            {
                attributes = new Attributes()
                {
                    color = Color.LightSkyBlue,
                    position = new Vector2(512, 600),
                    rotation = -.2f,
                    depth = .1f,
                },
                scale = new Vector2(.8f,.8f)
            };
            Text loser = new Text("Scrub!!!", _font)
            {
                attributes = new Attributes()
                {
                    color = Color.DarkRed,
                    position = new Vector2(512, 200),
                    rotation = -.2f,
                    depth = .1f
                },
                scale = new Vector2(.8f,.8f)
            };
            winner.Fade(2);
            loser.Fade(2);
            oppSelector.Fade(2);
            yourSelector.Move(Actions.ExpoMove, new Vector2(512, 400), 2);
            yourSelector.Fade(2);
            yourSelector.tweenerX.Ended += delegate()
            {
                base.Add(winner);
                base.Add(loser);
                
            };
            yourSelector.Scale(Actions.LinearMove, yourSelector.attributes.scale * 2, 2);
            Timer endGame = new Timer(1);
            base.Add(endGame);
            endGame.SetTimer(0, 4, delegate() { Reset(); });
        }

        public void OppAWinner()
        {
            speedState = gameState.Winner;
            Text winner = new Text("Winner!!!", _font)
            {
                attributes = new Attributes()
                {
                    color = Color.Red,
                    position = new Vector2(512, 200),
                    rotation = -.2f,
                    depth = .1f
                },
                scale = new Vector2(.8f,.8f)
            };
            Text loser = new Text("Scrub!!!", _font)
            {
                attributes = new Attributes()
                {
                    color = Color.DarkBlue,
                    position = new Vector2(512, 600),
                    rotation = -.2f,
                    depth = .1f
                },
                scale = new Vector2(.8f,.8f)
            };
            winner.Fade(2);
            loser.Fade(2);
            yourSelector.Fade(2);
            oppSelector.Fade(2);
            oppSelector.Move(Actions.ExpoMove, new Vector2(512, 400), 2);
            oppSelector.tweenerX.Ended += delegate()
            {
                base.Add(winner);
                base.Add(loser);
            };
            oppSelector.Scale(Actions.LinearMove, yourSelector.attributes.scale * 2, 2);
            Timer endGame = new Timer(1);
            base.Add(endGame);
            endGame.SetTimer(0, 4, delegate() { Reset(); });
        }

        public void Tie()
        {
            speedState = gameState.Winner;
            Text tieTop = new Text("It's a tie!", _font)
            {
                attributes = new Attributes()
                {
                    position = new Vector2(512, 200),
                    color = Color.RoyalBlue,
                    rotation = -.2f,
                    depth = .1f
                },
                scale = new Vector2(.8f,.8f)
            };

            Text tieMiddle = new Text("You're both", _font)
            {
                attributes = new Attributes()
                {
                    position = new Vector2(512, 400),
                    color = Color.Red,
                    rotation = -.2f,
                    depth = .1f
                },
                scale = new Vector2(.7f,.7f)
            };

            Text tieBottom = new Text("SCRUBS!!!", _font)
            {
                attributes = new Attributes()
                {
                    position = new Vector2(512, 600),
                    color = Color.Red,
                    rotation = -.2f,
                    depth = .1f
                },
                scale = new Vector2(.7f,.7f)
            };
            yourSelector.Fade(2);
            yourSelector.Move(Actions.ExpoMove, new Vector2(512, 400), 2);
            yourSelector.Scale(Actions.LinearMove, yourSelector.attributes.scale * 2, 2);
            oppSelector.Fade(2);
            oppSelector.Move(Actions.ExpoMove, new Vector2(512, 400), 2);
            oppSelector.tweenerX.Ended += delegate()
            {
                base.Add(tieTop);
                base.Add(tieMiddle);
                base.Add(tieBottom);
            };
            oppSelector.Scale(Actions.LinearMove, yourSelector.attributes.scale * 2, 2);
            Timer endGame = new Timer(1);
            base.Add(endGame);
            endGame.SetTimer(0, 4, delegate() { Reset();});
        }

        public void Reset()
        {
            speedState = gameState.PlayAgain;
        }
    }
}