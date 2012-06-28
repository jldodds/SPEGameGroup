
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

        // constructor -- gives attributes what it has and sets drawable to isSeeable
        public Drawable()
        {
            attributes = new Attributes();
            isSeeable = true;
        }

        // draws particle engine 
        // draws any drawable which isSeeable based on its attributes
        public virtual void Draw(SpriteBatch spriteBatch, SpriteEffects spriteEffects)
        {
            if (!isSeeable) return;
            spriteBatch.Draw(attributes.texture, attributes.position, null, attributes.color, 
                attributes.rotation, attributes.origin, attributes.scale, spriteEffects, attributes.depth);

        }

        public Attributes attributes { get; set; }
        Tweener tweenerX;
        Tweener tweenerY;
        Tweener tweenerRotate;
        Tweener tweenerColorR;
        Tweener tweenerColorG;
        Tweener tweenerColorB;
        Tweener tweenerA;
        public Tweener tweenerScaleX;
        Tweener tweenerScaleY;
        public bool isMoving { get; set; }
        public Tweener tweenerDepth;
        public bool isSeeable { get; set; }
        ParticleEngine particleEngine;
        public List<Texture2D> textures { get; set; }

        // rotates drawables 
        public void Rotate(float speed, float t)
        {
             attributes.rotation += t * speed * 2 * (float)Math.PI;
        }

        // moves drawables
        public void Move(MoveDel action, Vector2 endPosition, float d)
        {
            isMoving = true;
            tweenerX = new Tweener(attributes.position.X, endPosition.X, d, action);
            tweenerY = new Tweener(attributes.position.Y, endPosition.Y, d, action);
            tweenerX.Ended += delegate() { this.isMoving = false; };
        }

        // moves drawables with a delay
        public void Move(MoveDel action, Vector2 endPosition, float d, float delay)
        {
            isMoving = true;
            tweenerX = new Tweener(attributes.position.X, endPosition.X, delay, d, action);
            tweenerY = new Tweener(attributes.position.Y, endPosition.Y, delay, d, action);
            tweenerX.Ended += delegate() { this.isMoving = false; };
        }

        // changes color of drawables as they move
        public void ChangeColor(MoveDel action, Color endColor, float d)
        {
            tweenerColorR = new Tweener(attributes.color.R, endColor.R, d, action);
            tweenerColorG = new Tweener(attributes.color.G, endColor.G, d, action);
            tweenerColorB = new Tweener(attributes.color.B, endColor.B, d, action);
            tweenerA = new Tweener(attributes.color.A, endColor.A, d, action);
        }

        // makes drawables fade away given a certain amount of time
        public void Fade(float d)
        {           
            ChangeColor(Actions.LinearMove, Color.Transparent, d);
            tweenerA.Ended += delegate() { isSeeable = false; };
        }

        // rotates drawables
        public void Rotate(MoveDel action, float endRotation, float d)
        {
            tweenerRotate = new Tweener(attributes.rotation, endRotation, d, action);
        }

        // rescales drawables
        public void Scale(MoveDel action, Vector2 endScale, float d)
        {
            tweenerScaleX = new Tweener(attributes.scale.X, endScale.X, d, action);
            tweenerScaleY = new Tweener(attributes.scale.Y, endScale.Y, d, action);
        }

        // rescales drawables with a delay
        // not needed
        public void Scale(MoveDel action, Vector2 endScale, float d, float delay)
        {
            tweenerScaleX = new Tweener(attributes.scale.X, endScale.X, delay, d, action);
            tweenerScaleY = new Tweener(attributes.scale.Y, endScale.Y, delay, d, action);
        }

        // reverses drawables
        public virtual void Reverse()
        {
            tweenerScaleX.Reverse();
        }

        // flips drawables
        public void Flip(MoveDel action, float d)
        {
            this.Scale(action, new Vector2(0, attributes.scale.Y), d);            
        }
        
        // flips drawables with delay
        public void Flip(MoveDel action, float d, float delay)
        {
            this.Scale(action, new Vector2(0, attributes.scale.Y), d, delay);
        }

        // changes depth of drawables w delay
        public void ChangeDepth(MoveDel action, float endDepth, float d, float delay)
        {
            tweenerDepth = new Tweener(attributes.depth, endDepth, delay, d, action);
        }

        // updates tweeners which aren't null
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
        
        // 
        public void WhenDoneMoving(Tweener.EndHandler process)
        {
            tweenerX.Ended += process;
        }
        
        // 
        public void WhenDoneFading(Tweener.EndHandler process)
        {
            tweenerA.Ended += process;
        }

        public void ClearTweeners()
        {
            tweenerX = null;
            tweenerColorR = null;

            tweenerY = null;
            tweenerRotate = null;
            tweenerDepth = null;
            

            tweenerColorG = null;
            tweenerColorB = null;
            tweenerA = null;
            tweenerScaleX = null;
            tweenerScaleY = null;
        }
    }
}
