using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

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

        // moves selector left if called, assuming player is on and not penalized
        public void MoveSelectorLeft()
        {
            if (myState == PlayerState.Off) return;
            if (myState == PlayerState.Penalized) return;
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

        // if player is on, not penalized & not playing a card, moves card to a game pile depending on boolean
        public void SelectCard(bool isLeftPile)
        {
            if (myState == PlayerState.Off) return;
            if (myState == PlayerState.Penalized) return;
            if (myState == PlayerState.PlayingCard) return;
            if (isLeftPile) SelectedCardLeft();
            else SelectedCardRight();
        }

        public void Freeze()
        {
            //remember to change oldstate to frozen to come out of penalty frozen
        }

        public void UnFreeze()
        {
            //remember to change oldstate to normal to come out of penalty normal
        }

        public int score { get; set; }

        // turns player on into the normal state
        public void TurnOn()
        {
            myState = PlayerState.Normal;
        }
        
        // fail, scrub
        public virtual void Update(GameTime gameTime)
        {
        }
    }
}
