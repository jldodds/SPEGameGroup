using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Testgame
{
    class Pause : Screen
    {
        Drawable background;
        
        public Pause(Drawable background):base(background)
        {

        }

        /*Drawable resume = new Drawable()
        {
            attributes = new Attributes()
            {
                position = new Vector2(400 ,400),
            }
        };
        Drawable instructions = new Drawable(){
            attributes = new Attributes()
            {
                position = new Vector2(600 ,400),
            }
        };
        Drawable mainmenu = new Drawable(){
            attributes = new Attributes()
            {
                position = new Vector2(800 ,400),
            }
        };

        const int buttonCount = 3,
            easyButton = 0,
            mediumButton = 1,
            hardButton = 2,
            buttonHeight = 40,
            buttonWidth = 88;
        Color bgColor;
        Color[] buttonColor = new Color[buttonCount];
        Rectangle[] buttonRectangle = new Rectangle[buttonCount];
        Texture2D[] buttonTexture = new Texture2D[buttonCount];
        double[] buttonTimer = new double[buttonCount];
        KeyboardState keyboardState, lastKeyboardState;



        public void buttonAction(int i)
        {
            switch (i)
            {
                case easyButton:
                    bgColor = Color.Green;
                    break;
                case mediumButton:
                    bgColor = Color.Yellow;
                    break;
                case hardButton:
                    bgColor = Color.Red;
                    break;
                default:
                    break;
            }
        }

        public void KeyUpdate(GameTime gameTime)
        {
            lastKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
            Keys[] keymap = (Keys[])keyboardState.GetPressedKeys();
            foreach (Keys k in keymap)
            {

                char key = k.ToString()[0];
                switch (key)
                {
                    case 'e':
                    case 'E':
                        buttonAction(easyButton);
                        buttonColor[easyButton] = Color.Orange;
                        buttonTimer[easyButton] = 0.25;
                        break;
                    case 'm':
                    case 'M':
                        buttonAction(mediumButton);
                        buttonColor[mediumButton] = Color.Orange;
                        buttonTimer[mediumButton] = 0.25;
                        break;
                    case 'h':
                    case 'H':
                        buttonAction(hardButton);
                        buttonColor[hardButton] = Color.Orange;
                        buttonTimer[hardButton] = 0.25;
                        break;
                    default:
                        break;
                }

            }
        }

        // convert to drawbles first
        // for (int i = 0; i < buttonCount; i++)
        // spriteBatch.Draw(buttonTexture[i], buttonRectangle[i], buttonColor[i]);


        // initialize stuff
        public void Initialize()
        {
            // window.clientbounds.width doesn't work
            int x = 1024 / 2 - buttonWidth / 2;
            int y = 800 / 2 - buttonCount / 2 * buttonHeight - (buttonCount % 2) * buttonHeight / 2;
            
            for (int i = 0; i < buttonCount; i++)
            {
                buttonColor[i] = Color.White;
                buttonTimer[i] = 0.0;
                buttonRectangle[i] = new Rectangle(x, y, buttonWidth, buttonHeight);
                y += buttonHeight;
            }
        }
        
        public override void Update(GameTime gameTime)
        {
            Pause pause = new Pause(background);
            pause.Initialize();
            KeyUpdate(gameTime);
            base.Update(gameTime);
        }*/
    }
    }