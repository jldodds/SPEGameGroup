using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Speed
{
    public class Particle
    {
        Texture2D texture;
        Vector2 position;
        Vector2 velocity;
        float angle;
        float angularVelocity;
        Color color;
        float size;
        float timeToLive;
        float depth;

        // constructor, initialize particles' attributes
        public Particle(Texture2D Texture, Vector2 Position, Vector2 Velocity, float Angle, float AngularVelocity, Color Color, float Size, float TimeToLive, float Depth)
        {
            texture = Texture;
            position = Position;
            velocity = Velocity;
            angle = Angle;
            angularVelocity = AngularVelocity;
            color = Color;
            size = Size;
            timeToLive = TimeToLive;
            depth = Depth;
        }

        // updates method
        public void Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timeToLive -= elapsedTime;
            position += elapsedTime * velocity;
            angle += elapsedTime * angularVelocity;
        }

        // draws particles
        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(texture.Width / 2, texture.Height / 2);
            spriteBatch.Draw(texture, position, null, color, angle, origin, size, SpriteEffects.None, depth);
        }

        // returns whether or not particle engine's lifetime has passed
        public bool isDead()
        {
            return timeToLive <= 0;
        }
    }
}
