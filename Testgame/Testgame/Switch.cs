using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Testgame
{
    class Switch : Drawable
    {
        Text title;
        Text[] states;
        public bool selected { get; set; }
        int selectedState;
        public readonly float height;
        public readonly int switchHeight = 50;

        SpriteFont font;
        

        public Switch(String title, String[] names, float height, SpriteFont font, int defaultState)
            : base()
        {
            
            this.font = font;
            selectedState = defaultState;

            this.title = new Text(title + ":", font)
            {
                attributes = new Attributes()
                {
                    color = Color.Black,
                    position = new Vector2(200, height),
                    
                },
                height = 75
            };
            
            this.height = height;

            String[] stateNames = names;
            states = new Text[stateNames.Length];
            for (int i = 0; i < stateNames.Length; i++)
            {
                states[i] = new Text(stateNames[i], font)
                {
                    attributes = new Attributes()
                    {
                        color = Color.Black,
                        position = new Vector2(200 + 824 / (stateNames.Length + 1) * (i + 1), height),

                    },
                    height = switchHeight
                };
            }

        }

        public Switch(String title, float height, SpriteFont font, int defaultState)
            : base()
        {
            this.height = height;
            this.font = font;
            selectedState = defaultState;

            this.title = new Text(title + ":", font)
            {
                attributes = new Attributes()
                {
                    color = Color.Black,
                    position = new Vector2(200, height),

                },
                height = 75
            };

            states = new Text[2];
            states[0] = new Text("On", font)
            {
                attributes = new Attributes()
                {
                    color = Color.Black,
                    position = new Vector2(500, height)
                },
                height = 75
            };
            states[1] = new Text("Off", font)
            {
                attributes = new Attributes()
                {
                    color = Color.Black,
                    position = new Vector2(800, height)
                },
                height = 75
            };
        }


        public int getState()
        {
            return selectedState;
        }

        public bool getOnOff()
        {
            return selectedState == 0;
        }

        public override void Draw(SpriteBatch spriteBatch, SpriteEffects spriteEffects)
        {
            title.Draw(spriteBatch, spriteEffects);
            for (int i = 0; i < states.Length; i++)
            {
                if (i == selectedState)
                {
                    if (selected)
                    {
                        spriteBatch.DrawString(font, states[i].content, states[i].attributes.position, Color.Red, states[i].attributes.rotation,
                            font.MeasureString(states[i].content) / 2, states[i].scale * 1.3f, spriteEffects, states[i].attributes.depth);
                    }
                    else spriteBatch.DrawString(font, states[i].content, states[i].attributes.position, Color.Orange, states[i].attributes.rotation,
                            font.MeasureString(states[i].content) / 2, states[i].scale * 1.3f, spriteEffects, states[i].attributes.depth);
                }
                else states[i].Draw(spriteBatch, spriteEffects);
            }
        }

        public void moveRight()
        {
            selectedState++;
            if (selectedState == states.Length) selectedState = 0;
        }

        public void moveLeft()
        {
            selectedState--;
            if (selectedState == -1) selectedState = states.Length - 1;
        }

        public void MouseSelect(MouseState mousestate, MouseState oldstate)
        {
            float x = mousestate.X;
            for (int i = 0; i < states.Length; i++)
            {
                float halfWidth = states[i].width / 2;
                float halfHeight = states[i].height / 2;
                float xOrigin = states[i].attributes.position.X;
                float yOrigin = states[i].attributes.position.Y;
                if ((x > (xOrigin - halfWidth)) && (x < (xOrigin + halfWidth)))
                {
                    if (mousestate.LeftButton == ButtonState.Pressed)
                    {
                        if (oldstate.LeftButton == ButtonState.Released)
                        {
                            selectedState = i;
                        }
                    }
                }
            }
        }
    }
}
