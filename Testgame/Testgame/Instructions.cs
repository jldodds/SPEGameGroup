﻿using System;
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
        Text _instructions1;
        Text _instructions2;
        Text _instructions3;
        SpriteFont _font;
        KeyboardState oldState;
        public instructionspage _instructionsPage;

        public enum instructionspage
        {
            none,
            one,
            two,
            three
        }


        public Instructions(Drawable background, SpriteFont font)
            : base(background)
        {
            _font = font;
            _instructionsPage = instructionspage.none;


            
                _instructions1 = new Text("Ok this is a test.", _font)
                {
                    attributes = new Attributes()
                    {
                        color = Color.Orange,
                        position = new Vector2(512, 600),
                        depth = .11f,
                    },
                    isSeeable = false,
                    scale = new Vector2(.3f, .3f),
                };
                base.Add(_instructions1);

                _instructions2 = new Text("Page2", _font)
                {
                    attributes = new Attributes()
                    {
                        color = Color.Orange,
                        position = new Vector2(512, 600),
                        depth = .1f,
                    },
                    scale = new Vector2(.8f, .8f),
                    isSeeable = false,
                };

            
                _instructions3 = new Text("Page2", _font)
                {
                    attributes = new Attributes()
                    {
                        color = Color.Orange,
                        position = new Vector2(512, 600),
                        depth = .1f,
                    },
                    scale = new Vector2(.8f, .8f),
                    isSeeable = false,
                };

        }

        public void Start()
        {
            _instructionsPage = instructionspage.one;
            base.TurnOn();
        }

        public void WriteText()
        {
            if (_instructionsPage == instructionspage.one)
            {
                _instructions1.isSeeable = true;
            }

            if (_instructionsPage == instructionspage.two)
            {
                _instructions2.isSeeable = true;
            }

            if (_instructionsPage == instructionspage.three)
            {
                _instructions3 = new Text("Page2", _font)
                {
                    attributes = new Attributes()
                    {
                        color = Color.Orange,
                        position = new Vector2(512, 600),
                        depth = .1f,
                    },
                    scale = new Vector2(.8f, .8f),
                    isSeeable = false,
                };
            }
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardUpdate();
            WriteText();
            base.Update(gameTime);
        }

        public void KeyboardUpdate()
        {
            KeyboardState newState = Keyboard.GetState();

            if (_instructionsPage == instructionspage.none) return;
            if (newState.IsKeyDown(Keys.Left))
            {
                if (!oldState.IsKeyDown(Keys.Left))
                {
                    if (_instructionsPage == instructionspage.one) return;
                    else _instructionsPage = instructionspage.two;
                }
            }

            if (newState.IsKeyDown(Keys.Right))
            {
                if (!oldState.IsKeyDown(Keys.Right))
                {
                    if (_instructionsPage == instructionspage.three) return;
                    else _instructionsPage = instructionspage.two;
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
