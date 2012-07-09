using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Testgame
{
    public class ParticleEngine : Drawable
    {
        private Random random;
        private List<Particle> particles;
        public readonly List<Texture2D> textures;
        public bool isHalted { get; set; }
        float elapsed;
        float endTime;

        Vector2 maxVelocity;

        // constructor, initializes variables
        public ParticleEngine(List<Texture2D> Textures, Vector2 position, Vector2 velocity, float Depth, Color color)
        {
            random = new Random();
            attributes.position = position;
            particles = new List<Particle>();
            maxVelocity = velocity;
            textures = Textures;
            attributes.depth = Depth;
            elapsed = 0;
            attributes.color = color;
        }
        
        public ParticleEngine(List<Texture2D> Textures, Vector2 position, Vector2 velocity, float Depth, float time, Color color)
        {
            random = new Random();
            attributes.position = position;
            particles = new List<Particle>();
            maxVelocity = velocity;
            textures = Textures;
            attributes.depth = Depth;
            endTime = time;
            elapsed = 0;
            attributes.color = color;
        }

        // makes new particles
        private Particle GenerateNewParticle()
        {
            Texture2D texture = textures[random.Next(textures.Count)];
            Vector2 position = attributes.position;
            Vector2 velocity = new Vector2(
                    maxVelocity.X * (2 * (float) random.NextDouble() - 1),
                    maxVelocity.Y * (2 * (float)random.NextDouble() - 1));
            float angle = 0;
            float angularVelocity = 1.5f * (float)(random.NextDouble() * 2 - 1);
            Color color = new Color(random.Next(attributes.color.R - 20, attributes.color.R+20), random.Next(attributes.color.G - 20, attributes.color.G +20), random.Next(attributes.color.B-20,attributes.color.B+20), random.Next(attributes.color.A-20,attributes.color.A+20));
            float size = (float)random.NextDouble() * .8f;
            float ttl = .3f + (float)random.NextDouble() / 10f;

            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl, attributes.depth);
        }

        // update method
        // adds particles to list of particles using method above
        // removes "dead" particles
        public override void Update(GameTime gameTime)
        {
            elapsed += (float) gameTime.ElapsedGameTime.TotalSeconds;
            
            int total = 10;
            if ((endTime == 0 || elapsed <= endTime) && !isHalted)
            {
                for (int i = 0; i < total; i++)
                {
                    particles.Add(GenerateNewParticle());
                }
            }

            for (int particle = 0; particle < particles.Count; particle++)
            {
                particles[particle].Update(gameTime);
                if (particles[particle].isDead())
                {
                    particles.RemoveAt(particle);
                    particle--;
                }
            }

            base.Update(gameTime);
        }

        // draws particles in the list
        public override void Draw(SpriteBatch spriteBatch, SpriteEffects spriteEffects)
        {
            for (int index = 0; index < particles.Count; index++)
            {
                particles[index].Draw(spriteBatch);
            }
        }
    }
}
