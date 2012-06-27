using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Testgame
{
    class Player
    {
        public Player(String name, bool isPlayer1)
        {
            playerName = name;
            score = 0;
            myState = PlayerState.Off;
            this.isPlayer1 = isPlayer1; 
        }

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

        public void Penalize()
        {
            oldState = myState;
            myState = PlayerState.Penalized;
        }

        public void RemovePenalty()
        {
            myState = oldState;
        }

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
            //remember to chang oldstate to normal to come out of penalty normal
        }

        public int score { get; set; }

        public void TurnOn()
        {
            myState = PlayerState.Normal;
        }

        public virtual void Update(GameTime gameTime)
        {
        }




    }
}
