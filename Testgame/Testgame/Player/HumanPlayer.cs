using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Speed
{
    class HumanPlayer : Player
    {
        Keys toLeftPile;
        Keys toRightPile;
        Keys scrollLeft;
        Keys scrollRight;
        KeyboardState oldstate;
        Buttons toLeftPileButton;
        Buttons toRightPileButton;
        Buttons scrollLeftButton;
        Buttons scrollRightButton;
        GamePadState elderState;
        Buttons thumbstickLeft;
        Buttons thumbstickRight;

        // initializes keys
        public HumanPlayer(Keys _toLeftPile, Keys _toRightPile, Keys _scrollLeft, Keys _scrollRight, string name, bool isPlayer1) : base(name, isPlayer1)
        {
            toLeftPile = _toLeftPile;
            toRightPile = _toRightPile;
            scrollLeft = _scrollLeft;
            scrollRight = _scrollRight;
            toLeftPileButton = Buttons.LeftTrigger;
            toRightPileButton = Buttons.RightTrigger;
            scrollLeftButton = Buttons.DPadLeft;
            scrollRightButton = Buttons.DPadRight;
            thumbstickLeft = Buttons.LeftThumbstickLeft;
            thumbstickRight = Buttons.LeftThumbstickRight;

        }

        // update method
        public override void Update(Pile[] Hand, Pile rgamestack, Pile lgamestack, GameTime gameTime)
        {
            //base.MouseUpdate();
            GamePadUpdate();
            KeyUpdate();
            base.Update(Hand, rgamestack,lgamestack,gameTime);
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

        public void GamePadUpdate()
        {
            // keeps track of what key currently being pressed
            GamePadState newGameState;
            if (isPlayer1) newGameState = GamePad.GetState(PlayerIndex.One);
            else newGameState = GamePad.GetState(PlayerIndex.Two);
            
            // move selector to left pile if left if corresp. key pressed
            if (newGameState.IsButtonDown(toLeftPileButton))
            {
                if (!elderState.IsButtonDown(toLeftPileButton))
                {
                    base.SelectCard(true);
                }
            }

            // move selector to right pile if left if corresp. key pressed
            if (newGameState.IsButtonDown(toRightPileButton))
            {
                if (!elderState.IsButtonDown(toRightPileButton))
                {
                    base.SelectCard(false);
                }
            }

            // move selector left if left if corresp. key pressed
            if (newGameState.IsButtonDown(scrollLeftButton))
            {
                if (!elderState.IsButtonDown(scrollLeftButton))
                {
                    base.MoveSelectorLeft();
                }
            }

            // move selector left if left if corresp. key pressed
            if (newGameState.IsButtonDown(thumbstickLeft))
            {
                if (!elderState.IsButtonDown(thumbstickLeft))
                {
                    base.MoveSelectorLeft();
                }
            }

            // move selector right if left if corresp. key pressed
            if (newGameState.IsButtonDown(scrollRightButton))
            {
                if (!elderState.IsButtonDown(scrollRightButton))
                {
                    base.MoveSelectorRight();
                }
            }

            // move selector right if left if corresp. key pressed
            if (newGameState.IsButtonDown(thumbstickRight))
            {
                if (!elderState.IsButtonDown(thumbstickRight))
                {
                    base.MoveSelectorRight();
                }
            }

            // updates old keyboard state
            elderState = newGameState;
        }

    }
}
