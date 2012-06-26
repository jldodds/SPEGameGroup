
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Testgame
{
    public delegate float MoveDel(float start, float end, float d, float t);

    public class Drawable
    {
        public Drawable()
        {
            attributes = new Attributes();
            isSeeable = true;
        }
        public virtual void Draw(SpriteBatch spriteBatch, SpriteEffects spriteEffects)
        {
            if (!isSeeable) return;
            spriteBatch.Draw(attributes.texture, attributes.position, null, attributes.color, attributes.rotation, attributes.origin, attributes.scale, spriteEffects, attributes.depth);
            //if (particleEngine != null) particleEngine.Draw(spriteBatch);
        }

        public Attributes attributes { get; set; }
        
        Tweener tweenerX;
        Tweener tweenerY;
        Tweener tweenerRotate;
        Tweener tweenerColorR;
        Tweener tweenerColorG;
        Tweener tweenerColorB;
        Tweener tweenerA;
        protected Tweener tweenerScaleX;
        Tweener tweenerScaleY;
        protected Tweener tweenerDepth;

        public bool isMoving { get; set; }
        public bool isSeeable { get; set; }
        
        //ParticleEngine particleEngine;
        //List<Texture2D> textures;





         public void Rotate(float speed, float t)
         {
             attributes.rotation += t * speed * 2 * (float)Math.PI;
         }

        public void Move(MoveDel action, Vector2 endPosition, float d)
        {
            isMoving = true;
            tweenerX = new Tweener(attributes.position.X, endPosition.X, d, action);
            tweenerY = new Tweener(attributes.position.Y, endPosition.Y, d, action);
            tweenerX.Ended += delegate() { this.isMoving = false; };

        }

        public void Move(MoveDel action, Vector2 endPosition, float d, float delay)
        {
            isMoving = true;
            tweenerX = new Tweener(attributes.position.X, endPosition.X, delay, d, action);
            tweenerY = new Tweener(attributes.position.Y, endPosition.Y, delay, d, action);
            tweenerX.Ended += delegate() { this.isMoving = false; };
        }

        public void ChangeColor(MoveDel action, Color endColor, float d)
        {
            tweenerColorR = new Tweener(attributes.color.R, endColor.R, d, action);
            tweenerColorG = new Tweener(attributes.color.G, endColor.G, d, action);
            tweenerColorB = new Tweener(attributes.color.B, endColor.B, d, action);
            tweenerA = new Tweener(attributes.color.A, endColor.A, d, action);
        }

        public void Fade(float d)
        {
            
            ChangeColor(Actions.LinearMove, Color.Transparent, d);
            tweenerA.Ended += delegate() { isSeeable = false; };
        }

        public void Rotate(MoveDel action, float endRotation, float d)
        {
            tweenerRotate = new Tweener(attributes.rotation, endRotation, d, action);
        }

        public void Scale(MoveDel action, Vector2 endScale, float d)
        {
            tweenerScaleX = new Tweener(attributes.scale.X, endScale.X, d, action);
            tweenerScaleY = new Tweener(attributes.scale.Y, endScale.Y, d, action);
        }

        public void Scale(MoveDel action, Vector2 endScale, float d, float delay)
        {
            tweenerScaleX = new Tweener(attributes.scale.X, endScale.X, delay, d, action);
            tweenerScaleY = new Tweener(attributes.scale.Y, endScale.Y, delay, d, action);
        }

        public virtual void Reverse()
        {
            tweenerScaleX.Reverse();
        }


        public void Flip(MoveDel action, float d)
        {
            this.Scale(action, new Vector2(0, attributes.scale.Y), d);
            
    }
        public void Flip(MoveDel action, float d, float delay)
        {
            this.Scale(action, new Vector2(0, attributes.scale.Y), d, delay);
        }

        public void ChangeDepth(MoveDel action, float endDepth, float d, float delay)
        {
            tweenerDepth = new Tweener(attributes.depth, endDepth, delay, d, action);
        }

        public virtual void Update(GameTime gameTime)
        {
            if (tweenerX != null)
            {
                tweenerX.Update(gameTime);
                tweenerY.Update(gameTime);
                attributes.position = new Vector2(tweenerX.Position, tweenerY.Position);
            }
            if (tweenerRotate != null)
            {
                tweenerRotate.Update(gameTime);
                attributes.rotation = tweenerRotate.Position;
            }
            if (tweenerColorR != null)
            {
                tweenerColorR.Update(gameTime);
                tweenerColorG.Update(gameTime);
                tweenerColorB.Update(gameTime);
                tweenerA.Update(gameTime);
                attributes.color = new Color((int) tweenerColorR.Position, (int) tweenerColorG.Position,(int) tweenerColorB.Position, (int)tweenerA.Position);
            }
            if (tweenerScaleX != null)
            {
                tweenerScaleX.Update(gameTime);
                tweenerScaleY.Update(gameTime);
                attributes.scale = new Vector2(tweenerScaleX.Position,tweenerScaleY.Position);
            }
            if (tweenerDepth != null)
            {
                tweenerDepth.Update(gameTime);
                attributes.depth = tweenerDepth.Position;
            }
        }

        public void WhenDoneMoving(Tweener.EndHandler process)
        {
            tweenerX.Ended += process;
        }

        public void WhenDoneFading(Tweener.EndHandler process)
        {
            tweenerA.Ended += process;
        }
    }
}
