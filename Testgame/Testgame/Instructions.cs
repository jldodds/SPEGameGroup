using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Testgame
{
    class Instructions : Screen
    {
        Drawable _slide1;
        Drawable _slide2;
        Drawable _slide3;
        Drawable _slide4;
        KeyboardState oldState;
        instructionspage _instructionsPage;

        enum instructionspage
        {
            none,
            one,
            two,
            three,
            four
        }

        // initializes slides, sets instructions page to none, adds slides to screen
        public Instructions(Drawable background, Drawable slide, Drawable slide2, Drawable slide3, Drawable slide4)
            : base(background)
        {
            _slide1 = slide;
            _slide2 = slide2;
            _slide3 = slide3;
            _slide4 = slide4;
            _instructionsPage = instructionspage.none;
            base.Add(_slide1);
            base.Add(_slide2);
            base.Add(_slide3);
            base.Add(_slide4);
        }

        // turns on the first instructions page
        public void Start()
        {
            _instructionsPage = instructionspage.one;
            base.TurnOn();
        }

        public void DisplayPage()
        {
            // makes 1st slide visible
            if (_instructionsPage == instructionspage.one)
            {
                _slide2.isSeeable = false;
                _slide3.isSeeable = false;
                _slide4.isSeeable = false;
                _slide1.isSeeable = true;
            }

            // makes 2nd slide visible
            if (_instructionsPage == instructionspage.two)
            {
                _slide1.isSeeable = false;
                _slide3.isSeeable = false;
                _slide4.isSeeable = false;
                _slide2.isSeeable = true;
            }

            // makes 3rd slide visible
            if (_instructionsPage == instructionspage.three)
            {
                _slide1.isSeeable = false;
                _slide2.isSeeable = false;
                _slide4.isSeeable = false;
                _slide3.isSeeable = true;
            }
            
            // makes 4th slide visible
            if (_instructionsPage == instructionspage.four)
            {
                _slide1.isSeeable = false;
                _slide2.isSeeable = false;
                _slide3.isSeeable = false;
                _slide4.isSeeable = true;
            }
        }

        // update method
        public override void Update(GameTime gameTime)
        {
            DisplayPage();
            KeyboardUpdate();
            base.Update(gameTime);
        }

        public void KeyboardUpdate()
        {
            KeyboardState newState = Keyboard.GetState();

            if (_instructionsPage == instructionspage.none) return;
            if (newState.IsKeyDown(Keys.Left))
            {
                // moves back an instructions page unless on the first one
                if (!oldState.IsKeyDown(Keys.Left))
                {
                    if (_instructionsPage == instructionspage.one) return;
                    else if (_instructionsPage == instructionspage.two) _instructionsPage = instructionspage.one;
                    else if (_instructionsPage == instructionspage.three) _instructionsPage = instructionspage.two;
                    else if (_instructionsPage == instructionspage.four) _instructionsPage = instructionspage.three;
                }
            }

            // moves forward an instructions page unless on the last one
            if (newState.IsKeyDown(Keys.Right))
            {
                if (!oldState.IsKeyDown(Keys.Right))
                {
                    if (_instructionsPage == instructionspage.one) _instructionsPage = instructionspage.two;
                    else if (_instructionsPage == instructionspage.two) _instructionsPage = instructionspage.three;
                    else if (_instructionsPage == instructionspage.three) _instructionsPage = instructionspage.four;
                    else if (_instructionsPage == instructionspage.four) return;
                }
            }

            if (newState.IsKeyDown(Keys.Enter))
            {
                if (!oldState.IsKeyDown(Keys.Enter))
                {
                    //take back to main menu
                }
            }

            oldState = newState;
        }
    }
}
