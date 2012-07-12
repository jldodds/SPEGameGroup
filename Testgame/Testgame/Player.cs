using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Testgame
{
    class Player
    {
        // constructor, initializes arguments
        public Player(String name, bool isPlayer1)
        {
            playerName = name;
            score = 0;
            myState = PlayerState.Off;
            this.isPlayer1 = isPlayer1;
            freezeTimer = new Timer(1);
        }

        // possible player states
        private enum PlayerState
        {
            Off,
            Normal,
            Penalized,
            Frozen,
            PlayingCard,
        }

        public String playerName { get; set; }
        PlayerState myState;
        public int selector { get; set; }
        public readonly bool isPlayer1;
        PlayerState oldState;
        //MouseState oldstate;
        Timer freezeTimer;

        //int cardTopHeight = 585;
        //int cardBottomHeight = 765;
        //int initCardRightSide = 808;
        //int initCardLeftSide = 678;
        //int cardXShift = 154;

        public delegate void CardPlayer();
        public event CardPlayer SelectedCardLeft;
        public event CardPlayer SelectedCardRight;

        // puts player in penalty state
        public void Penalize()
        {
            oldState = myState;
            myState = PlayerState.Penalized;
        }


        // puts player back in old state
        public void RemovePenalty()
        {
            myState = oldState;
        }

        // moves selector right if called, assuming player is on and not penalized
        public void MoveSelectorRight()
        {
            if (myState == PlayerState.Off) return;
            if (myState == PlayerState.Penalized) return;
            if (myState == PlayerState.Frozen) return;
            if (!isPlayer1)
            {
                selector++;
                if (selector == 5) selector = 0;
            }
            else
            {
                selector--;
                if (selector == -1) selector = 4;
            }
        }

        // moves selector right if called, assuming player is on and not penalized
        public void MoveSelectorRight(int pileNumber)
        {
            if (myState == PlayerState.Off) return;
            if (myState == PlayerState.Penalized) return;
            if (myState == PlayerState.Frozen) return;
            if (!isPlayer1)
            {
                for (int i = 0; i < pileNumber; i++)
                {
                    selector++;
                    if (selector == 5) selector = 0;
                }
            }
            else
            {
                for (int i = 0; i < pileNumber; i++)
                {
                    selector--;
                    if (selector == -1) selector = 4;
                }
            }
        }

        // moves selector left if called, assuming player is on and not penalized
        public void MoveSelectorLeft()
        {
            if (myState == PlayerState.Off) return;
            if (myState == PlayerState.Penalized) return;
            if (myState == PlayerState.Frozen) return;
            if (!isPlayer1)
            {
                selector--;
                if (selector == -1) selector = 4;
            }
            else
            {
                selector++;
                if (selector == 5) selector = 0;
            }
        }

        // moves selector left to pile that is passed in, assuming player is on and not penalized
        public void MoveSelectorLeft(int pileNumber)
        {
            if (myState == PlayerState.Off) return;
            if (myState == PlayerState.Penalized) return;
            if (myState == PlayerState.Frozen) return;
            if (!isPlayer1)
            {
                for (int i = 0; i < pileNumber; i++)
                {
                    selector--;
                    if (selector == -1) selector = 4;
                }
            }
            else
            {
                for (int i = 0; i < pileNumber; i++)
                {
                    selector++;
                    if (selector == 5) selector = 0;
                }
            }
        }

        // if player is on, not penalized & not playing a card, moves card to a game pile depending on boolean
        public void SelectCard(bool isLeftPile)
        {
            if (myState == PlayerState.Off) return;
            if (myState == PlayerState.Penalized) return;
            if (myState == PlayerState.PlayingCard) return;
            if (myState == PlayerState.Frozen) return;
            if (isLeftPile) SelectedCardLeft();
            else SelectedCardRight();
        }

        public void Freeze()
        {
            if (myState == PlayerState.Frozen || (myState == PlayerState.Penalized && oldState == PlayerState.Frozen)) freezeTimer.ResetTimer(0);
            oldState = PlayerState.Frozen;
            if (myState != PlayerState.Penalized) myState = PlayerState.Frozen;
            freezeTimer.SetTimer(0, 4, delegate() { UnFreeze(); });
        }

        double timeLeft;

        public void PauseFreeze()
        {
            freezeTimer.isPaused = true;
            timeLeft = freezeTimer.getTimeLeft(1);
        }

        public void unPauseFreeze()
        {
            freezeTimer.isPaused = false;
            Timer newTimer = new Timer(2);
            newTimer.SetTimer(2, (float)timeLeft, delegate() { UnFreeze(); });
        }

        public void UnFreeze()
        {
            oldState = PlayerState.Normal;
            if (myState != PlayerState.Penalized) myState = PlayerState.Normal;
        }

        public int score { get; set; }

        // turns player on into the normal state
        public void TurnOn()
        {
            myState = PlayerState.Normal;
        }

        /*public void MouseUpdate()
        {
            // keeps track of what key currently being pressed
            MouseState newstate = Mouse.GetState();
            
            // move selector to left pile if left if corresp. key pressed
                if (newstate.LeftButton == ButtonState.Pressed && oldstate.LeftButton == ButtonState.Released)
                {
                    if ((newstate.X > initCardLeftSide) && (newstate.X < initCardRightSide) && (newstate.Y > cardTopHeight) && (newstate.Y < cardBottomHeight))
                    {
                        selector = 0;
                        SelectCard(true);
                    }

                    if ((newstate.X > initCardLeftSide - cardXShift) && (newstate.X < initCardRightSide - cardXShift) && (newstate.Y > cardTopHeight) && (newstate.Y < cardBottomHeight))
                    {
                        selector = 1;
                        SelectCard(true);
                    }

                    if ((newstate.X > initCardLeftSide - (2 * cardXShift)) && (newstate.X < initCardRightSide - (2 * cardXShift)) && (newstate.Y > cardTopHeight) && (newstate.Y < cardBottomHeight))
                    {
                        selector = 2;
                        SelectCard(true);
                    }

                    if ((newstate.X > initCardLeftSide - (3 * cardXShift)) && (newstate.X < initCardRightSide - (3 * cardXShift)) && (newstate.Y > cardTopHeight) && (newstate.Y < cardBottomHeight))
                    {
                        selector = 3;
                        SelectCard(true);
                    }

                    if ((newstate.X > initCardLeftSide - (4 * cardXShift)) && (newstate.X < initCardRightSide - (4 * cardXShift)) && (newstate.Y > cardTopHeight) && (newstate.Y < cardBottomHeight))
                    {
                        selector = 4;
                        SelectCard(true);
                    }
                }

            if (newstate.RightButton == ButtonState.Pressed && oldstate.RightButton == ButtonState.Released)
            {
                if ((newstate.X > initCardLeftSide) && (newstate.X < initCardRightSide) && (newstate.Y > cardTopHeight) && (newstate.Y < cardBottomHeight))
                {
                    selector = 0;
                    SelectCard(false);
                }

                if ((newstate.X > initCardLeftSide - cardXShift) && (newstate.X < initCardRightSide - cardXShift) && (newstate.Y > cardTopHeight) && (newstate.Y < cardBottomHeight))
                {
                    selector = 1;
                    SelectCard(false);
                }

                if ((newstate.X > initCardLeftSide - (2 * cardXShift)) && (newstate.X < initCardRightSide - (2 * cardXShift)) && (newstate.Y > cardTopHeight) && (newstate.Y < cardBottomHeight))
                {
                    selector = 2;
                    SelectCard(false);
                }

                if ((newstate.X > initCardLeftSide - (3 * cardXShift)) && (newstate.X < initCardRightSide - (3 * cardXShift)) && (newstate.Y > cardTopHeight) && (newstate.Y < cardBottomHeight))
                {
                    selector = 3;
                    SelectCard(false);
                }

                if ((newstate.X > initCardLeftSide - (4 * cardXShift)) && (newstate.X < initCardRightSide - (4 * cardXShift)) && (newstate.Y > cardTopHeight) && (newstate.Y < cardBottomHeight))
                {
                    selector = 4;
                    SelectCard(false);
                }
            }

            // updates old mouse state
            oldstate = newstate;
        }*/
        
        
        // This has to be implemented by the extended classes
        public virtual void Update(Pile[] Hand, Pile rgamestack, Pile lgamestack, GameTime gameTime)
        {
            freezeTimer.Update(gameTime);
        }

        public virtual void Reset()
        {
            score = 0;
            myState = PlayerState.Off;
            selector = 0;
            freezeTimer.RemoveTimers();
            SelectedCardLeft = null;
            SelectedCardRight = null;
        }
    }
}
