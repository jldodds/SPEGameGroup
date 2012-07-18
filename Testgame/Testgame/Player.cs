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
        Timer freezeTimer;

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

        //
        public void Freeze()
        {
            if (myState == PlayerState.Frozen || (myState == PlayerState.Penalized && oldState == PlayerState.Frozen)) freezeTimer.ResetTimer(0);
            oldState = PlayerState.Frozen;
            if (myState != PlayerState.Penalized) myState = PlayerState.Frozen;
            freezeTimer.SetTimer(0, 4, delegate() { UnFreeze(); });
        }

        double timeLeft;

        //
        public void PauseFreeze()
        {
            freezeTimer.isPaused = true;
            timeLeft = freezeTimer.getTimeLeft(1);
        }

        //
        public void unPauseFreeze()
        {
            freezeTimer.isPaused = false;
            Timer newTimer = new Timer(2);
            newTimer.SetTimer(2, (float)timeLeft, delegate() { UnFreeze(); });
        }

        //
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
