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
        public Vector2 emitterLocation { get; set; }
        private List<Particle> particles;
        private List<Texture2D> textures;
        public float depth { get; set; }
        float elapsed;
        float endTime;

        // constructor, initializes variables
        public ParticleEngine(List<Texture2D> Textures, Vector2 position, float Depth, float time)
        {
            random = new Random();
            emitterLocation = position;
            particles = new List<Particle>();
            textures = Textures;
            depth = Depth;
            endTime = time;
            elapsed = 0;
        }

        // makes new particles
        private Particle GenerateNewParticle()
        {
            Texture2D texture = textures[random.Next(textures.Count)];
            Vector2 position = emitterLocation;
            Vector2 velocity = new Vector2(
                    1f * (float)(random.NextDouble() * 600 - 300),
                    1f * (float)(random.NextDouble() * 600 - 300));
            float angle = 0;
            float angularVelocity = 1.5f * (float)(random.NextDouble() * 2 - 1);
            Color color = new Color(
                    (float) random.NextDouble(),
                    (float)random.NextDouble() * .5f + .5f,
                    0);
            float size = (float)random.NextDouble() * .8f;
            float ttl = .3f + (float)random.NextDouble() / 10f;

            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl, depth);
        }

        // update method
        // adds particles to list of particles using method above
        // removes "dead" particles
        public override void Update(GameTime gameTime)
        {
            elapsed += (float) gameTime.ElapsedGameTime.TotalSeconds;
            
            int total = 10;
            if (elapsed <= endTime)
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
