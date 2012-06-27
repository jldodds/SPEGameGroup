﻿using System;
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
        
        // this is a workaround to not have public variables
        // overrides other isPaused boolean and gives it the local _isPaused boolean
        public override bool isPaused
        {
            get
            {
                return _isPaused;
            }
            // if it's paused, turns menu off
            // if not paused, turns on the menu after .5 seconds
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
                if (value) myState = menuState.Off;
            }
        }

        // menu constructor
        public Menu(Drawable background, int numberOfButtons, Text title, String[] buttonNames, Button.ClickHandler[] buttonActions, SpriteFont buttonFont)
            : base(background)
        {
            // gives buttons height & width, adds title, makes buttons & gives position in middle of screen
            buttonHeight = 75;
            buttons = new Button[numberOfButtons];
            base.Add(title);
            title.attributes.position = new Vector2(512, title.height / 2 + 40);
            maxButtonWidth = 0;
            
            // makes new buttons, adds heights, adds actions and events for if clicked
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

            // sets selected button to the top button
            selected = 0;
            // initializes menu to off, then turns it on after .5 seconds
            myState = menuState.Off;
            Timer state = new Timer(1);
            base.Add(state);
            state.SetTimer(0, .5f, delegate() { myState = menuState.On; });
        }

        // overloaded constructor to add buttonheight
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

        // different states for menus.. on, off, or a button that's clicked
        public enum menuState
        {
            On,
            Clicking,
            Off
        }

        // update method
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

        // method to keep track of keys pressed
        public void KeyUpdate()
        {
            // keeps track of current keyboard state
            KeyboardState newState = Keyboard.GetState();
            
            // only do the following if the menustate is on
            if (myState == menuState.Off) return;
            if (myState == menuState.Clicking) return;
            
            // if up is pressed, moves selector up
            if (newState.IsKeyDown(Keys.Up))
            {
                if (!oldState.IsKeyDown(Keys.Up))
                {
                    selected--;
                    if (selected == -1) selected = buttons.Length - 1;
                }
            }

            // if down is pressed, moves selector down
            if (newState.IsKeyDown(Keys.Down))
            {
                if (!oldState.IsKeyDown(Keys.Down))
                {
                    selected++;
                    if (selected == buttons.Length) selected = 0;
                }
            }

            // if enter is pressed, ...
            if (newState.IsKeyDown(Keys.Enter))
            {
                if (!oldState.IsKeyDown(Keys.Enter))
                {
                    myState = menuState.Clicking;
                    buttons[selected].WhenButtonClicked(delegate() { myState = menuState.On; });
                    buttons[selected].Click();
                }
            }

            // makes the oldstate the previous state
            oldState = newState;
        }

    }
}