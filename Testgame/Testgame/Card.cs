using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Testgame
{
    class Card
    {
        public Drawable cardFront;
        Texture2D cardBack;
        public bool isFaceUp { get; set; }
        private int cardHeight = 180;
        private int cardWidth = 130;
        public readonly int cardNumber;
        private bool isFlipping;

        #region Constructor
        public Card(int card, Texture2D front, Texture2D back, Vector2 position, bool faceUp)
        {
            cardNumber = card;
            cardFront = new Drawable()
            {
                attributes = new Attributes()
                    {
                        texture = front,
                        position = position,
                        height = cardHeight,
                        rotation = 0,
                        width = cardWidth,
                        color = Color.White,
                    }
            };
            cardBack = back;
            isFaceUp = faceUp;
            isFlipping = false;

        }
        #endregion

        #region Methods
        public void Draw(SpriteBatch spriteBatch, SpriteEffects spriteEffects)
        {
            if (isFaceUp)
                cardFront.Draw(spriteBatch, spriteEffects);
            else
                spriteBatch.Draw(cardBack, cardFront.attributes.position, null, cardFront.attributes.color, cardFront.attributes.rotation, cardFront.attributes.origin, new Vector2(cardFront.attributes.scale.X * cardFront.attributes.texture.Width / cardBack.Width, cardFront.attributes.scale.Y * cardFront.attributes.texture.Height / cardBack.Height), spriteEffects, cardFront.attributes.depth);
        }

        public void Flip(bool endOrientation)
        {
            if (isFlipping) return;
            isFlipping = true;
            cardFront.Flip(Actions.LinearMove, .15f);
            cardFront.tweenerScaleX.Ended += this.Reverse;
            cardFront.tweenerScaleX.Ended += delegate() { isFaceUp = endOrientation; };
            
        }

        private void Reverse()
        {
            cardFront.Reverse();
            cardFront.tweenerScaleX.Ended -= this.Reverse;
            cardFront.tweenerScaleX.Ended += delegate() { isFlipping = false; };
        }

        public void Update(GameTime gameTime)
        {
            cardFront.Update(gameTime);
        }

        public void toPile(Pile pile)
        {
            cardFront.Move(Actions.ExpoMove, pile.position, .5f);
            pile.Add(this);
        }
        #endregion
    }
}
