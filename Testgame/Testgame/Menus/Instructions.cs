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
        Drawable[][] _slides;
        KeyboardState oldState;
        GamePadState elderState;
        int page;
        Button exit;

        // initializes slides, sets instructions page to none, adds slides to screen
        public Instructions(Drawable background, Drawable[][] slides, SpriteFont buttonfont)
            : base(background)
        {
            _slides = slides;
            page = 0;

            for (int i = 0; i < _slides.Length; i++)
            {
                for (int j = 0; j < _slides[i].Length; j++)
                {
                    base.Add(_slides[i][j]);
                }
            }

            exit = new Button("Back", buttonfont, new Vector2(512, 725), Color.Black);
            exit.height = 58;
            exit.Select();
            base.Add(exit);
   
            base.isPaused = true;

        }

        public void DisplayPage()
        {
            for (int i = 0; i < _slides.Length; i++)
            {
                if (i == page)
                {
                    for (int j = 0; j < _slides[i].Length; j++)
                    {
                        _slides[i][j].isSeeable = true;
                    }
                }

                else
                {
                    for (int j = 0; j < _slides[i].Length; j++)
                    {
                        _slides[i][j].isSeeable = false;
                    }
                }
            }
        }

        // update method
        public override void Update(GameTime gameTime)
        {
            if (isPaused) return;
            KeyboardUpdate();
            GamePadUpdate();
            DisplayPage();
            base.Update(gameTime);
        }

        public void KeyboardUpdate()
        {
            KeyboardState newState = Keyboard.GetState();

            if (newState.IsKeyDown(Keys.Left))
            {
                // moves back an instructions page unless on the first one
                if (!oldState.IsKeyDown(Keys.Left))
                {
                    if (page == 0) return;
                    else page--;
                }
            }

            // moves forward an instructions page unless on the last one
            if (newState.IsKeyDown(Keys.Right))
            {
                if (!oldState.IsKeyDown(Keys.Right))
                {
                    if (page == _slides.Length - 1) return;
                    else page++;
                }
            }

            if (newState.IsKeyDown(Keys.Enter))
            {
                if (!oldState.IsKeyDown(Keys.Enter))
                {
                    exit.Click();
                }
            }

            oldState = newState;
        }

        public void GamePadUpdate()
        {
            GamePadState youngState = GamePad.GetState(PlayerIndex.One);

            if (youngState.IsButtonDown(Buttons.DPadLeft))
            {
                // moves back an instructions page unless on the first one
                if (!elderState.IsButtonDown(Buttons.DPadLeft))
                {
                    if (page == 0) return;
                    else page--;
                }
            }

            if (youngState.IsButtonDown(Buttons.LeftThumbstickLeft))
            {
                // moves back an instructions page unless on the first one
                if (!elderState.IsButtonDown(Buttons.LeftThumbstickLeft))
                {
                    if (page == 0) return;
                    else page--;
                }
            }

            // moves forward an instructions page unless on the last one
            if (youngState.IsButtonDown(Buttons.LeftThumbstickRight))
            {
                if (!elderState.IsButtonDown(Buttons.LeftThumbstickRight))
                {
                    if (page == _slides.Length - 1) return;
                    else page++;
                }
            }

            if (youngState.IsButtonDown(Buttons.DPadRight))
            {
                if (!elderState.IsButtonDown(Buttons.DPadRight))
                {
                    if (page == _slides.Length - 1) return;
                    else page++;
                }
            }

            if (youngState.IsButtonDown(Buttons.A))
            {
                if (!elderState.IsButtonDown(Buttons.A))
                {
                    exit.Click();
                }
            }

            elderState = youngState;
        }

        public void ResetPage()
        {
            page = 0;
        }

        public void setButton(Button.ClickHandler process)
        {
            exit.Clicked += process;
        }
    }
}
