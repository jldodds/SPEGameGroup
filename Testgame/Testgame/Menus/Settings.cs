using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Speed
{
    class Settings : Screen
    {
        Switch[] switches;
        int selectedSwitch;
        Text title;
        KeyboardState oldState;
        MouseState oldstate;
        GamePadState elderState;
        Button exit;
        public override bool isPaused
        {
            get
            {
                return base.isPaused;
            }
            set
            {
                if (!value) selectedSwitch = 0;
                base.isPaused = value;
            }
        }

        public Settings(Text title, Switch[] switches, Drawable background, SpriteFont font)
            : base(background)
        {
            this.switches = switches;
            for (int i = 0; i < switches.Length; i++)
            {
                base.Add(this.switches[i]);
            }

            this.title = title;
            base.Add(this.title);

            exit = new Button("Back", font, new Vector2(512, 725), Color.Black);
            exit.height = 58;
            base.Add(exit);
        }

        public override void Update(GameTime gameTime)
        {
            if (isPaused) return;
            KeyUpdate();
            MouseUpdate();
            GamePadUpdate();
            
            for (int i = 0; i < switches.Length; i++)
            {
                if (i == selectedSwitch) switches[i].selected = true;
                else switches[i].selected = false;
            }
            if (selectedSwitch == switches.Length) exit.Select();
            else exit.DeSelect();
            
            base.Update(gameTime);
        }

        public void KeyUpdate()
        {
            KeyboardState newState = Keyboard.GetState();

            if (newState.IsKeyDown(Keys.Down))
            {
                if (!oldState.IsKeyDown(Keys.Down))
                {
                    selectedSwitch++;
                    if (selectedSwitch == switches.Length + 1) selectedSwitch = 0;
                }
            }

            if (newState.IsKeyDown(Keys.Up))
            {
                if (!oldState.IsKeyDown(Keys.Up))
                {
                    selectedSwitch--;
                    if (selectedSwitch == -1) selectedSwitch = switches.Length;
                }
            }

            if (newState.IsKeyDown(Keys.Left))
            {
                if (!oldState.IsKeyDown(Keys.Left))
                {
                    if (selectedSwitch != switches.Length) switches[selectedSwitch].moveLeft();
                }
            }


            if (newState.IsKeyDown(Keys.Right))
            {
                if (!oldState.IsKeyDown(Keys.Right))
                {
                    if (selectedSwitch != switches.Length) switches[selectedSwitch].moveRight();
                }
            }

            if (newState.IsKeyDown(Keys.Enter))
            {
                if (!oldState.IsKeyDown(Keys.Enter))
                {
                    if (selectedSwitch == switches.Length) 
                        exit.Click();
                }
            }

            oldState = newState;
        }

        public void MouseUpdate()
        {
            MouseState newstate = Mouse.GetState();
            if (newstate == oldstate) return;
            float y = newstate.Y;
            for (int i = 0; i < switches.Length; i++)
            {
                float halfHeight = switches[i].switchHeight / 2;
                float yOrigin = switches[i].height;
                if ((y < (yOrigin + halfHeight)) && (y > (yOrigin - halfHeight)))
                {
                    selectedSwitch = i;
                    switches[i].MouseSelect(newstate, oldstate);
                }
            }

            float exitHalfWidth = exit.width / 2;
            float exitHalfHeight = exit.height / 2;
            float exitOriginX = exit.attributes.position.X;
            float exitOriginY = exit.attributes.position.Y;
            float x = newstate.X;
            if ((y < (exitOriginY + exitHalfHeight)) && (y > (exitOriginY - exitHalfHeight))
                && (x < (exitOriginX + exitHalfWidth)) && (x > (exitOriginX - exitHalfWidth)))
            {
                selectedSwitch = switches.Length;
                if (newstate.LeftButton == ButtonState.Pressed)
                {
                    if (oldstate.LeftButton == ButtonState.Released)
                    {
                        exit.Click();
                    }
                }
            }
            oldstate = newstate;
        }

        public void GamePadUpdate()
        {
            GamePadState youngState = GamePad.GetState(PlayerIndex.One);

            if (youngState.IsButtonDown(Buttons.DPadDown))
            {
                if (!elderState.IsButtonDown(Buttons.DPadDown))
                {
                    selectedSwitch++;
                    if (selectedSwitch == switches.Length + 1) selectedSwitch = 0;
                }
            }

            if (youngState.IsButtonDown(Buttons.LeftThumbstickDown))
            {
                if (!elderState.IsButtonDown(Buttons.LeftThumbstickDown))
                {
                    selectedSwitch++;
                    if (selectedSwitch == switches.Length + 1) selectedSwitch = 0;
                }
            }

            if (youngState.IsButtonDown(Buttons.DPadUp))
            {
                if (!elderState.IsButtonDown(Buttons.DPadUp))
                {
                    selectedSwitch--;
                    if (selectedSwitch == -1) selectedSwitch = switches.Length;
                }
            }

            if (youngState.IsButtonDown(Buttons.LeftThumbstickUp))
            {
                if (!elderState.IsButtonDown(Buttons.LeftThumbstickUp))
                {
                    selectedSwitch--;
                    if (selectedSwitch == -1) selectedSwitch = switches.Length;
                }
            }

            if (youngState.IsButtonDown(Buttons.DPadLeft))
            {
                if (!elderState.IsButtonDown(Buttons.DPadLeft))
                {
                    if (selectedSwitch != switches.Length) switches[selectedSwitch].moveLeft();
                }
            }

            if (youngState.IsButtonDown(Buttons.LeftThumbstickLeft))
            {
                if (!elderState.IsButtonDown(Buttons.LeftThumbstickLeft))
                {
                    if (selectedSwitch != switches.Length) switches[selectedSwitch].moveLeft();
                }
            }


            if (youngState.IsButtonDown(Buttons.DPadRight))
            {
                if (!elderState.IsButtonDown(Buttons.DPadRight))
                {
                    if (selectedSwitch != switches.Length) switches[selectedSwitch].moveRight();
                }
            }

            if (youngState.IsButtonDown(Buttons.LeftThumbstickRight))
            {
                if (!elderState.IsButtonDown(Buttons.LeftThumbstickRight))
                {
                    if (selectedSwitch != switches.Length) switches[selectedSwitch].moveRight();
                }
            }

            if (youngState.IsButtonDown(Buttons.A))
            {
                if (!elderState.IsButtonDown(Buttons.A))
                {
                    if (selectedSwitch == switches.Length)
                        exit.Click();
                }
            }

            elderState = youngState;
        }

        public void SetButton(Button.ClickHandler process)
        {
            exit.Clicked += process;
        }
    }
}
