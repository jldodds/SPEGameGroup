using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Testgame
{
    public class Button : Text
    {
        Timer buttonTimer;
        bool selected;
        bool clicked;
        
        // gives buttons colors, position, makes buttontimer
        public Button(String stuff, SpriteFont font, Vector2 position, Color color): base(stuff, font)
        {
            attributes.color = color;
            attributes.position = position;
            buttonTimer = new Timer(1);
        }

        // draws buttons if they should be drawn, then makes selected buttons larger than normal
        public override void Draw(SpriteBatch spriteBatch, SpriteEffects spriteEffects)
        {
            if (!isSeeable) return;
            if (selected && !clicked) attributes.color = Color.Red;
            if (selected) spriteBatch.DrawString(_font, content, attributes.position, attributes.color, attributes.rotation, _font.MeasureString(content) / 2, scale * 1.3f, spriteEffects, attributes.depth);
            else base.Draw(spriteBatch, spriteEffects);
        }

        // delegate and event for cases where buttons are selected
        public delegate void ClickHandler();
        public event ClickHandler Clicked;

        // performs button actions .5 seconds after it's clicked, then uses delegates to make events occur
        public void Click()
        {
            clicked = true;
            attributes.color = Color.Orange;
            buttonTimer.SetTimer(0, .5f, delegate() { Clicked(); clicked = false; attributes.color = Color.Black; });
        }

        // 
        public void Remove()
        {
            buttonTimer.RemoveTimers();
            attributes.color = Color.Black;
            clicked = false;        
        }

        // update method
        public override void Update(GameTime gameTime)
        {
            if (buttonTimer != null) buttonTimer.Update(gameTime);
            base.Update(gameTime);
        }

        // changes boolean about selection to true
        public void Select()
        {
            selected = true;
        }

        // changes boolean about selection to false
        public void DeSelect()
        {
            selected = false;
        }

        // returns wheter or not button is clicked
        public bool isClicked()
        {
            return clicked;
        }

        //
        public void WhenButtonClicked(ClickHandler process)
        {
            Clicked += process;
        }

        /*
        // wrapper for hit_image_alpha taking Rectangle and Texture
        Boolean hit_image_alpha(Rectangle rect, Texture2D tex, int x, int y)
        {
            return hit_image_alpha(0, 0, tex, tex.Width * (x - rect.X) /
                rect.Width, tex.Height * (y - rect.Y) / rect.Height);
        }

        // wraps hit_image then determines if hit a transparent part of image
        Boolean hit_image_alpha(float tx, float ty, Texture2D tex, int x, int y)
        {
            if (hit_image(tx, ty, tex, x, y))
            {
                uint[] data = new uint[tex.Width * tex.Height];
                tex.GetData<uint>(data);
                if ((x - (int)tx) + (y - (int)ty) *
                    tex.Width < tex.Width * tex.Height)
                {
                    return ((data[
                        (x - (int)tx) + (y - (int)ty) * tex.Width
                        ] &
                                0xFF000000) >> 24) > 20;
                }
            }
            return false;
        }

        // determine if x,y is within rectangle formed by texture located at tx,ty
        Boolean hit_image(float tx, float ty, Texture2D tex, int x, int y)
        {
            return (x >= tx &&
                x <= tx + tex.Width &&
                y >= ty &&
                y <= ty + tex.Height);
        }

        // determine state and color of button
        void update_buttons()
        {
            for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
            {

                if (hit_image_alpha(
                    button_rectangle[i], button_texture[i], mx, my))
                {
                    button_timer[i] = 0.0;
                    if (mpressed)
                    {
                        // mouse is currently down
                        button_state[i] = BState.DOWN;
                        button_color[i] = Color.Blue;
                    }
                    else if (!mpressed && prev_mpressed)
                    {
                        // mouse was just released
                        if (button_state[i] == BState.DOWN)
                        {
                            // button i was just down
                            button_state[i] = BState.JUST_RELEASED;
                        }
                    }
                    else
                    {
                        button_state[i] = BState.HOVER;
                        button_color[i] = Color.LightBlue;
                    }
                }
                else
                {
                    button_state[i] = BState.UP;
                    if (button_timer[i] > 0)
                    {
                        button_timer[i] = button_timer[i] - frame_time;
                    }
                    else
                    {
                        button_color[i] = Color.White;
                    }
                }

                if (button_state[i] == BState.JUST_RELEASED)
                {
                    take_action_on_button(i);
                }
            }
        }

        // Global variables
        enum BState
        {
            HOVER,
            UP,
            JUST_RELEASED,
            DOWN
        }
        const int NUMBER_OF_BUTTONS = 3,
            EASY_BUTTON_INDEX = 0,
            MEDIUM_BUTTON_INDEX = 1,
            HARD_BUTTON_INDEX = 2,
            BUTTON_HEIGHT = 40,
            BUTTON_WIDTH = 88;
        Color background_color;
        Color[] button_color = new Color[NUMBER_OF_BUTTONS];
        Rectangle[] button_rectangle = new Rectangle[NUMBER_OF_BUTTONS];
        BState[] button_state = new BState[NUMBER_OF_BUTTONS];
        Texture2D[] button_texture = new Texture2D[NUMBER_OF_BUTTONS];
        double[] button_timer = new double[NUMBER_OF_BUTTONS];
        //mouse pressed and mouse just pressed
        bool mpressed, prev_mpressed = false;
        //mouse location in window
        int mx, my;
        double frame_time;
         */
    }
}
