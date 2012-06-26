using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Testgame
{
    public class Menu : Screen
    {
        KeyboardState oldState;
        Button[] buttons;
        int buttonHeight;
        float maxButtonWidth;
        int selected;
        menuState myState;
        private bool _isPaused;
        public override bool isPaused
        {
            get
            {
                return _isPaused;
            }
            set
            {
                _isPaused = value;
                if (!value)
                {
                    myState = menuState.Off;
                    Timer state = new Timer(1);
                    base.Add(state);
                    state.SetTimer(0, .5f, delegate() { myState = menuState.On; });  
                }
                //if (value) myState = menuState.Off;
            }
        }

        public Menu(Drawable background, int numberOfButtons, Text title, String[] buttonNames, Button.ClickHandler[] buttonActions, SpriteFont buttonFont)
            : base(background)
        {
            buttonHeight = 75;
            buttons = new Button[numberOfButtons];
            base.Add(title);
            title.attributes.position = new Vector2(512, title.height / 2 + 40);
            maxButtonWidth = 0;
            for (int i = 0; i < numberOfButtons; i++)
            {
                buttons[i] = new Button(buttonNames[i], buttonFont, new Vector2(512, title.height + 140 + ((800 - title.height - 80 - buttonHeight) / numberOfButtons) * i), Color.Black);
                buttons[i].height = buttonHeight;
                buttons[i].Clicked += buttonActions[i];
                buttons[i].Clicked += delegate()
                {
                    for (int j = 0; j < numberOfButtons; j++)
                    {
                        if (j != i) buttons[j].Remove();
                    }
                };
                if (buttons[i].width > maxButtonWidth) maxButtonWidth = buttons[i].width;
                base.Add(buttons[i]);
            }

            selected = 0;
            myState = menuState.Off;
            Timer state = new Timer(1);
            base.Add(state);
            state.SetTimer(0, .5f, delegate() { myState = menuState.On; });
        }

        public Menu(Drawable background, int numberOfButtons, Text title, String[] buttonNames, Button.ClickHandler[] buttonActions, SpriteFont buttonFont, int buttonHeight)
            : base(background)
        {
            this.buttonHeight = buttonHeight; 
            buttons = new Button[numberOfButtons];
            base.Add(title);
            title.attributes.position = new Vector2(512, title.height / 2 + 40);
            maxButtonWidth = 0;
            for (int i = 0; i < numberOfButtons; i++)
            {
                buttons[i] = new Button(buttonNames[i], buttonFont, new Vector2(512, title.height + 140 + ((800 - title.height - 80 - buttonHeight) / numberOfButtons) * i), Color.Black);
                buttons[i].height = this.buttonHeight;
                buttons[i].Clicked += buttonActions[i];
                buttons[i].Clicked += delegate()
                {
                    for (int j = 0; j < numberOfButtons; j++)
                    {
                        if (j != i) buttons[j].Remove();
                    }
                };
                if (buttons[i].width > maxButtonWidth) maxButtonWidth = buttons[i].width;
                base.Add(buttons[i]);
            }

            selected = 0;
            myState = menuState.Off;
            Timer state = new Timer(1);
            base.Add(state);
            state.SetTimer(0, .5f, delegate() { myState = menuState.On; });
        }

        public enum menuState
        {
            On,
            Clicking,
            Off
        }

        public override void Update(GameTime gameTime)
        {
            if (isPaused) return;
            KeyUpdate();
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i == selected)
                {
                    buttons[i].Select();
                    if(!buttons[i].isClicked()) buttons[i].attributes.color = Color.Red;
                }
                else
                {
                    buttons[i].DeSelect();
                    if(!buttons[i].isClicked()) buttons[i].attributes.color = Color.Black;
                }
            }
            
            base.Update(gameTime);
        }

        public void KeyUpdate()
        {
            KeyboardState newState = Keyboard.GetState();
            if (myState == menuState.Off) return;
            if (myState == menuState.Clicking) return;
            if (newState.IsKeyDown(Keys.Up))
            {
                if (!oldState.IsKeyDown(Keys.Up))
                {
                    selected--;
                    if (selected == -1) selected = buttons.Length - 1;
                }
            }

            if (newState.IsKeyDown(Keys.Down))
            {
                if (!oldState.IsKeyDown(Keys.Down))
                {
                    selected++;
                    if (selected == buttons.Length) selected = 0;
                }
            }

            if (newState.IsKeyDown(Keys.Enter))
            {
                if (!oldState.IsKeyDown(Keys.Enter))
                {
                    myState = menuState.Clicking;
                    buttons[selected].WhenButtonClicked(delegate() { myState = menuState.On; });
                    buttons[selected].Click();
                }
            }

            oldState = newState;
        }

    }
}