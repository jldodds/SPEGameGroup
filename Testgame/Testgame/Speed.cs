
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
        Pile yourStack;
        Pile opponentStack;
        Pile lSpitStack;
        Pile rSpitStack;
        Pile lGameStack;
        Pile rGameStack;
        Card[] cards;
        Pile[] yourCards;
        Pile[] opponentCards;
        KeyboardState oldState;
        public gameState speedState { get; set; }
        Drawable oppSelector;
        Drawable yourSelector;
        Player you;
        Player opp;
        SpriteFont _font;
        public bool isHalted { get; set; }
        public bool isShaking { get; set; }

        public Speed(Card[] deck, Drawable background, Texture2D selector, SpriteFont font, Player bottom, Player top):base(background)
        {
            isHalted = false;
            isShaking = false;

            you = bottom;
            opp = top;
            you.SelectedCardLeft += delegate(){PlayCard(you, you.selector, lGameStack);};
            you.SelectedCardRight += delegate() { PlayCard(you, you.selector, rGameStack); };
            opp.SelectedCardLeft += delegate() { PlayCard(opp, opp.selector, lGameStack); };
            opp.SelectedCardRight += delegate() { PlayCard(opp, opp.selector, rGameStack); };
            
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
            yourCards = new Pile[5];
            opponentCards = new Pile[5];

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
            cards[51].WhenDoneMoving( delegate()
            {
                base.Add(begin);
                speedState = gameState.AskBegin;
            });
           
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
           
            stopwatch.SetTimer(0, 1, delegate()
            {
                Text two = new Text("2", _font) { attributes = new Attributes() { color = Color.Yellow, position = new Vector2(512, 400) } };
                base.Add(two);
                two.Fade(1);
                
            });
            stopwatch.SetTimer(1, 2, delegate()
            {
                Text one = new Text("1", _font) { attributes = new Attributes() { color = Color.Yellow, position = new Vector2(512, 400) } };
                base.Add(one);
                one.Fade(1);
                
            });
            stopwatch.SetTimer(2, 3, delegate()
            {
                Text title = new Text("SPEED!", _font) { attributes = new Attributes() { color = Color.Yellow, position = new Vector2(512, 400) } };
                base.Add(title);
                title.Fade(.5f);
                title.WhenDoneFading(new Tweener.EndHandler(BeginGame));
            });
        }

        public void DrawCard(Pile drawPile, Pile destinationPile, float delay)
        {
            if (destinationPile.drawnTo) return;
            destinationPile.drawnTo = true;
            Card temp = drawPile.Take();
            temp.Flip(true, delay);
            temp.toPile(destinationPile, delay);
            temp.WhenDoneMoving(delegate() { destinationPile.drawnTo = false; });
        }

        public override void Update(GameTime gameTime)
        {
            if (base.isPaused) return;
            if (isHalted) return;
            switch (speedState)
            {
                case gameState.Dealing:
                case gameState.AskBegin:
                case gameState.Beginning:
                    break;
                case gameState.GamePlay:
                    you.Update(gameTime);
                    opp.Update(gameTime);
                    yourSelector.attributes.position = yourCards[you.selector].position;
                    oppSelector.attributes.position = opponentCards[opp.selector].position;
                    if (ExistWinner()) Winner(DetermineWinner());
                    else if (!ExistMoves()) ReBegin();
                    break;
                
                case gameState.ReBeginning:
                case gameState.PlayingCard:
                    you.Update(gameTime);
                    opp.Update(gameTime);
                    yourSelector.attributes.position = yourCards[you.selector].position;
                    oppSelector.attributes.position = opponentCards[opp.selector].position;
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
            if (speedState == gameState.AskBegin)
            {
                if (newState.IsKeyDown(Keys.Enter))
                {
                    if (!oldState.IsKeyDown(Keys.Enter))
                    {
                        Begin();
                    }
                }
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
            you.TurnOn();
            opp.TurnOn();
        }

        public void PlayCard(Player player, int selectedPile, Pile destinationPile)
        {
            if (speedState == gameState.PlayingCard) return;
            Pile fromPile;
            if (player.isPlayer1) fromPile = yourCards[player.selector];
            else fromPile = opponentCards[player.selector];
            if (fromPile.Count() == 0)
            {
                return;
            }
            if (fromPile.drawnTo) return;
            speedState = gameState.PlayingCard;
            Card c = fromPile.Peek();
            int cv = c.cardValue;
            int value = destinationPile.Peek().cardValue;
            if ((cv == 0 && value == 12) || (cv == 12 && value == 0) || (cv == value + 1 || cv == value - 1))
            {

                Card m = fromPile.Take();
                m.toPile(destinationPile);
                m.WhenDoneMoving(delegate()
                {
                    player.score++;
                    speedState = gameState.GamePlay;
                    if (player.isPlayer1)
                    {
                        if (yourStack.Count() != 0)
                        {
                            if (fromPile.Count() == 0)
                            {
                                DrawCard(yourStack, fromPile, 0f);
                            }
                        }
                    }
                    else
                    {
                        if (opponentStack.Count() != 0)
                        {
                            if (fromPile.Count() == 0)
                            {
                                DrawCard(opponentStack, fromPile, 0f);
                            }
                        }
                    }
                    Shake();
                });
            }
            else
            {
                speedState = gameState.GamePlay;
                c.attributes.color = Color.Red;
                Timer timer = new Timer(1);
                base.Add(timer);
                timer.SetTimer(0, 1, delegate() { c.attributes.color = Color.White; player.RemovePenalty(); });
                player.Penalize();
            }
        }
        
        public bool ExistMoves()
        {
            bool oppMoves = false;
            for (int i = 0; i < opponentCards.Length; i++)
            {
                if (opponentCards[i].Count() != 0)
                {
                    int cv = opponentCards[i].Peek().cardValue;
                    int value1 = lGameStack.Peek().cardValue;
                    int value2 = rGameStack.Peek().cardValue;
                    if ((cv == 0 && value1 == 12) || (cv == 12 && value1 == 0) || (cv == value1 + 1 || cv == value1 - 1)) oppMoves = true;
                    if ((cv == 0 && value2 == 12) || (cv == 12 && value2 == 0) || (cv == value2 + 1 || cv == value2 - 1)) oppMoves = true;
                }
            }

            bool yourMoves = false;
            for (int i = 0; i < yourCards.Length; i++)
            {
                if (yourCards[i].Count() != 0)
                {
                    int cv = yourCards[i].Peek().cardValue;
                    int value1 = lGameStack.Peek().cardValue;
                    int value2 = rGameStack.Peek().cardValue;
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
                nomove.WhenDoneMoving( delegate() { nomove.isSeeable = false; });
            });

            if (lSpitStack.Count() != 0)
            {
                Timer stopwatch = new Timer(5);
                base.Add(stopwatch);

                stopwatch.SetTimer(0, 3, delegate()
                {
                    Text two = new Text("2", _font) { attributes = new Attributes() { color = Color.Yellow, position = new Vector2(512, 400) } };
                    base.Add(two);
                    two.Fade(1);

                });
                stopwatch.SetTimer(1, 4, delegate()
                {
                    Text one = new Text("1", _font) { attributes = new Attributes() { color = Color.Yellow, position = new Vector2(512, 400) } };
                    base.Add(one);
                    one.Fade(1);

                });
                stopwatch.SetTimer(2, 5, delegate()
                {
                    Text title = new Text("SPEED!", _font) { attributes = new Attributes() { color = Color.Yellow, position = new Vector2(512, 400) } };
                    base.Add(title);
                    title.Fade(.5f);
                    title.WhenDoneFading(new Tweener.EndHandler(BeginGame));
                });
                stopwatch.SetTimer(3, 2, delegate()
                {
                    Text three = new Text("3", _font) { attributes = new Attributes() { color = Color.Yellow, position = new Vector2(512, 400) } };
                    base.Add(three);
                    three.Fade(1);

                });
            }

            else Winner(DetermineWinner());

        }

        public bool ExistWinner()
        {
            bool youWinner = true;
            for (int i = 0; i < yourCards.Length; i++)
            {
                if (yourCards[i].Count() != 0) youWinner = false;
            }
            if (yourStack.Count() != 0) youWinner = false;
            
            bool oppWinner = true;
            for (int i = 0; i < opponentCards.Length; i++)
            {
                if (opponentCards[i].Count() != 0) oppWinner = false;
            }
            if (opponentStack.Count() != 0) oppWinner = false;
            return youWinner || oppWinner;
        }

        public Player DetermineWinner()
        {
            if (you.score > opp.score) return you;
            if (opp.score > you.score) return opp;
            else return null;
        }

        public void Winner(Player winner)
        {
            if (winner == null) Tie();
            else if (winner.isPlayer1) YourAWinner();
            else OppAWinner();
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

            oppSelector.Fade(2);
            yourSelector.Move(Actions.ExpoMove, new Vector2(512, 400), 2);
            yourSelector.Fade(2);
            yourSelector.WhenDoneMoving( delegate()
            {
                base.Add(winner);
                base.Add(loser);
                
            });
            yourSelector.Scale(Actions.LinearMove, yourSelector.attributes.scale * 2, 2);
            Timer endGame = new Timer(2);
            base.Add(endGame);
            endGame.SetTimer(0, 6, delegate() { Reset(); });
            endGame.SetTimer(1, 4, delegate() { winner.Fade(2); loser.Fade(2); });
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
            yourSelector.Fade(2);
            oppSelector.Fade(2);
            oppSelector.Move(Actions.ExpoMove, new Vector2(512, 400), 2);
            oppSelector.WhenDoneMoving( delegate()
            {
                base.Add(winner);
                base.Add(loser);
            });
            oppSelector.Scale(Actions.LinearMove, yourSelector.attributes.scale * 2, 2);
            Timer endGame = new Timer(2);
            base.Add(endGame);
            endGame.SetTimer(0, 6, delegate() { Reset(); });
            endGame.SetTimer(1, 4, delegate() { winner.Fade(2); loser.Fade(2); });
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
            oppSelector.WhenDoneMoving( delegate()
            {
                base.Add(tieTop);
                base.Add(tieMiddle);
                base.Add(tieBottom);
            });
            oppSelector.Scale(Actions.LinearMove, yourSelector.attributes.scale * 2, 2);
            Timer endGame = new Timer(2);
            base.Add(endGame);
            endGame.SetTimer(0, 6, delegate() { Reset();});
            endGame.SetTimer(1, 4, delegate() { tieTop.Fade(2); tieMiddle.Fade(2); tieBottom.Fade(2); });
        }

        public void Reset()
        {
            speedState = gameState.PlayAgain;
        }

        public void Shake()
        {
            Timer timer = new Timer(1);
            base.Add(timer);
            isShaking = true;
            timer.SetTimer(0, .5f, delegate() { isShaking = false; });
        }
    }
}