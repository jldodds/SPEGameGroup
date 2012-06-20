
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Testgame
{
    class Card : Drawable
    {
        Texture2D cardFront;
        Texture2D cardBack;
        private int cardHeight = 180;
        private int cardWidth = 130;
        public bool isFaceUp {
            get
            {
                return isFaceUp;
            }
            set
            {
                bool temp = value;
                if (temp == true)
                {
                    attributes.texture = cardFront;
                    
                }
                if (temp == false)
                {
                    attributes.texture = cardBack;
                    
                }
            }
            }
        public readonly int cardNumber;
        public readonly int cardValue;
        private bool isFlipping;

        #region Constructor
        public Card(int card, Texture2D front, Texture2D back, Vector2 position, bool faceUp)
        {
            cardNumber = card;
            cardValue = cardNumber % 13;
            cardFront = front;
            cardBack = back;
            
                attributes = new Attributes()
                    {
                        
                        position = position,
                        
                        rotation = 0,
                        
                        color = Color.White,
                    };
                isFaceUp = faceUp;
                attributes.height = cardHeight;
                attributes.width = cardWidth;
            
            
            isFlipping = false;

        }
        #endregion

        #region Methods
        

        public void Flip(bool endOrientation)
        {
            if (isFlipping) return;
            isFlipping = true;
            base.Flip(Actions.LinearMove, .15f);
            tweenerScaleX.Ended += this.Reverse;
            tweenerScaleX.Ended += delegate() { isFaceUp = endOrientation; };
            
        }

        public void Flip(bool endOrientation, float delay)
        {
            if (isFlipping) return;
            isFlipping = true;
            base.Flip(Actions.LinearMove, .15f, delay);
            tweenerScaleX.Ended += this.Reverse;
            tweenerScaleX.Ended += delegate() { isFaceUp = endOrientation; };
        }

        public override void Reverse()
        {
            base.Reverse();
            tweenerScaleX.Ended -= this.Reverse;
            tweenerScaleX.Ended += delegate() { isFlipping = false; };
        }

        public void toPile(Pile pile)
        {
            if (isMoving == false)
            {
                Move(Actions.ExpoMove, new Vector2(pile.position.X, pile.position.Y), .3f);
                pile.Add(this);
                Raise(.2f, 0);
                tweenerDepth.Ended += delegate() { Lower(.52f - pile.stack.Count * .01f, .2f); };
            }
        }
        public void toPile(Pile pile, float delay)
        {
            if (isMoving == false)
            {
                Move(Actions.ExpoMove, new Vector2(pile.position.X, pile.position.Y), .4f, delay);
                pile.Add(this);
                Raise(.2f, delay);
                tweenerDepth.Ended += delegate() { Lower(.52f - pile.stack.Count * .01f, .2f); };
            }
        }

        public void Raise(float d, float delay)
        {
            ChangeDepth(Actions.ExpoMove, 0f, d, delay);
        }

        
        public void Lower(float endDepth, float d)
        {
            ChangeDepth(Actions.ExpoMoveIn, endDepth, d, 0f);
        }


        #endregion
    }
}

