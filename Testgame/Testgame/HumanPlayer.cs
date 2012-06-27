using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Testgame
{
    class HumanPlayer : Player
    {
        Keys toLeftPile;
        Keys toRightPile;
        Keys scrollLeft;
        Keys scrollRight;
        KeyboardState oldstate;

        // initializes keys
        public HumanPlayer(Keys _toLeftPile, Keys _toRightPile, Keys _scrollLeft, Keys _scrollRight, string name, bool isPlayer1) : base(name, isPlayer1)
        {
            toLeftPile = _toLeftPile;
            toRightPile = _toRightPile;
            scrollLeft = _scrollLeft;
            scrollRight = _scrollRight;
        }

        // update method
        public override void Update(GameTime gameTime)
        {
            KeyUpdate();
            base.Update(gameTime);
        }

        //update keys
        public void KeyUpdate()
        {
            // keeps track of what key currently being pressed
            KeyboardState newstate = Keyboard.GetState();

            // move selector to left pile if left if corresp. key pressed
            if (newstate.IsKeyDown(toLeftPile))
            {
                if (!oldstate.IsKeyDown(toLeftPile))
                {
                    base.SelectCard(true);
                }
            }

            // move selector to right pile if left if corresp. key pressed
            if (newstate.IsKeyDown(toRightPile))
            {
                if (!oldstate.IsKeyDown(toRightPile))
                {
                    base.SelectCard(false);
                }
            }

            // move selector left if left if corresp. key pressed
            if (newstate.IsKeyDown(scrollLeft))
            {
                if (!oldstate.IsKeyDown(scrollLeft))
                {
                    base.MoveSelectorLeft();
                }
            }

            // move selector right if left if corresp. key pressed
            if (newstate.IsKeyDown(scrollRight))
            {
                if (!oldstate.IsKeyDown(scrollRight))
                {
                    base.MoveSelectorRight();
                }
            }

            // updates old keyboard state
            oldstate = newstate;
        }

    }
}
