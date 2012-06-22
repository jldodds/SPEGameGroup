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
        public Button[] buttons;
        int buttonHeight = 75;
        float maxButtonWidth;
        Drawable selector;
        int selected;
        menuState myState;

        public Menu(Drawable background, int numberOfButtons, Text title, String[] buttonNames, Button.ClickHandler[] buttonActions, SpriteFont buttonFont, Texture2D selector)
            : base(background)
        {
            buttons = new Button[numberOfButtons];
            base.Add(title);
            title.attributes.position = new Vector2(512, title.height / 2 + 40);
            maxButtonWidth = 0;
            for (int i = 0; i < numberOfButtons; i++)
            {
                buttons[i] = new Button(buttonNames[i], buttonFont, new Vector2(512, title.height + 140 + ((800 - title.height - 80 - buttonHeight) / numberOfButtons) * i), Color.Black);
                buttons[i].height = 75;
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
            this.selector = new Drawable()
            {
                attributes = new Attributes()
                {
                    texture = selector,
                    position = buttons[selected].attributes.position,
                    color = Color.Red,
                    height = buttonHeight * 1.3f + 5,
                    width = maxButtonWidth * 1.3f + 40,
                    depth = 0,
                    
                }
            };
            base.Add(this.selector);
            myState = menuState.Off;
            Timer state = new Timer(1);
            base.Add(state);
            state.SetTimer(0, 1, delegate() { myState = menuState.On; });
        }

        public enum menuState
        {
            On,
            Off
        }

        public override void Update(GameTime gameTime)
        {
            if (isPaused) return;
            KeyUpdate();
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i == selected) buttons[i].selected = true;
                else buttons[i].selected = false;
            }
            selector.attributes.position = buttons[selected].attributes.position;
            base.Update(gameTime);
        }

        public void KeyUpdate()
        {
            KeyboardState newState = Keyboard.GetState();
            if (myState == menuState.Off) return;
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
                    buttons[selected].Click();
                }
            }

            oldState = newState;
        }

    }
}