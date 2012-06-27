
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
        Texture2D cardFront;            // cardfront image
        Texture2D cardBack;             // cardback image
        private int cardHeight = 180;   // card height in pixels
        private int cardWidth = 130;    // card width in pixels
        public bool isFaceUp 
        {
            // returns boolean isFaceUp
            get
            {
                return isFaceUp;
            }
            // if card isFaceUp, displays front of card, otherwise does back of card
            set
            {
                if (value == true)
                {
                    attributes.texture = cardFront;
                }
                else
                {
                    attributes.texture = cardBack;  
                }
            }
        }
        public readonly int cardNumber;
        public readonly int cardValue;
        private bool isFlipping;

        #region Constructor
        // card constructor
        public Card(int card, Texture2D front, Texture2D back, Vector2 position, bool faceUp)
        {
            // initializing variables
            cardNumber = card;
            cardValue = cardNumber % 13;
            cardFront = front;
            cardBack = back;
            
            // giving cards their attributes
            attributes = new Attributes()
               {
                    position = position,
                    rotation = 0,
                    color = Color.White,
               };
            
            isFaceUp = faceUp;                // sets cards faceUp
            attributes.height = cardHeight;   // gives cards height of 180 (instance variable)
            attributes.width = cardWidth;     // gives cards width of 130 (instance variable)
            isFlipping = false;               //cards are not flipping
        }
        #endregion

        #region Methods
        // flips card
        public void Flip(bool endOrientation)
        {
            if (isFlipping) return;
            isFlipping = true;
            base.Flip(Actions.LinearMove, .15f);
            tweenerScaleX.Ended += this.Reverse;
            tweenerScaleX.Ended += delegate() { isFaceUp = endOrientation; };           
        }

        // flips card with delay
        public void Flip(bool endOrientation, float delay)
        {
            if (isFlipping) return;
            isFlipping = true;
            base.Flip(Actions.LinearMove, .15f, delay);
            tweenerScaleX.Ended += this.Reverse;
            tweenerScaleX.Ended += delegate() { isFaceUp = endOrientation; };
        }

        // reverses card
        public override void Reverse()
        {
            base.Reverse();
            tweenerScaleX.Ended -= this.Reverse;
            tweenerScaleX.Ended += delegate() { isFlipping = false; };
        }

        // moves card toPile
        public void toPile(Pile pile)
        {
            Move(Actions.ExpoMove, new Vector2(pile.position.X, pile.position.Y), .2f);
            pile.Add(this);
            Raise(.2f, 0);
            tweenerDepth.Ended += delegate() { Lower(.52f - pile.Count() * .01f, .2f); };       
        }

        // moves card to pile with delay
        public void toPile(Pile pile, float delay)
        {
            Move(Actions.ExpoMove, new Vector2(pile.position.X, pile.position.Y), .4f, delay);
            pile.Add(this);
            Raise(.2f, delay);
            tweenerDepth.Ended += delegate() { Lower(.52f - pile.Count() * .01f, .2f); };
        }

        // raises cards
        public void Raise(float d, float delay)
        {
            ChangeDepth(Actions.ExpoMove, 0f, d, delay);
        }

        // lowers cards
        public void Lower(float endDepth, float d)
        {
            ChangeDepth(Actions.ExpoMoveIn, endDepth, d, 0f);
        }
        #endregion
    }
}

