using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Testgame
{
    class HumanPlayer:Player
    {
        Keys toLeftPile;
        Keys toRightPile;
        Keys scrollLeft;
        Keys scrollRight;
        KeyboardState oldstate;

        public HumanPlayer(Keys _toLeftPile, Keys _toRightPile, Keys _scrollLeft, Keys _scrollRight, string name, bool isPlayer1):base(name, isPlayer1)
        {
            toLeftPile = _toLeftPile;
            toRightPile = _toRightPile;
            scrollLeft = _scrollLeft;
            scrollRight = _scrollRight;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            KeyUpdate();
            base.Update(gameTime);
        }

        public void KeyUpdate()
        {
            KeyboardState newstate = Keyboard.GetState();

            if (newstate.IsKeyDown(toLeftPile))
            {
                if (!oldstate.IsKeyDown(toLeftPile))
                {
                    base.SelectCard(true);
                }
            }

            if (newstate.IsKeyDown(toRightPile))
            {
                if (!oldstate.IsKeyDown(toRightPile))
                {
                    base.SelectCard(false);
                }
            }

            if (newstate.IsKeyDown(scrollLeft))
            {
                if (!oldstate.IsKeyDown(scrollLeft))
                {
                    base.MoveSelectorLeft();
                }
            }

            if (newstate.IsKeyDown(scrollRight))
            {
                if (!oldstate.IsKeyDown(scrollRight))
                {
                    base.MoveSelectorRight();
                }
            }

            oldstate = newstate;
        }

    }
}
