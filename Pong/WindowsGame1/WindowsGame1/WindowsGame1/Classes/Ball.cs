using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1.Classes
{
    class Ball : GameObject
    {
        public Vector2 Velocity;
        public Random random;

        public Ball()
        {
            random = new Random();
        }

        public void Launch(float speed)
        {
            Position = new Vector2(Game1.ScreenWidth / 2 - Texture.Width / 2, Game1.ScreenHeight / 2 - Texture.Height / 2);
            float rotation = (float) (Math.PI/2 + (random.NextDouble() * (Math.PI/1.5f) - Math.PI/3));
            
            Velocity.X = (float) Math.Sin(rotation);
            Velocity.Y = (float) Math.Cos(rotation);

            if (random.Next(2) == 1)
            {
                Velocity.X *= -1;
            }
            Velocity *= speed;
        }

        public void Collider2D(Vector2 Player1, Texture2D Texture1, Vector2 Player2, Texture2D Texture2, SoundEffect wallCollider, SoundEffect playerCollider)
        {
            if (Position.X >= Player2.X - Texture.Width && Position.X <= Player2.X && Position.Y >= Player2.Y - Texture.Height && Position.Y <= Player2.Y + Texture2.Height ||
                Position.X <= Player1.X + Texture1.Width && Position.X >= Player1.X + Texture.Width && Position.Y >= Player1.Y - Texture.Height && Position.Y <= Player1.Y + Texture1.Height)
                {
                    Velocity.X *= -1;
                    playerCollider.Play();
                }

            if (Position.Y >= Player2.Y - Texture.Height && Position.Y <= Player2.Y && Position.X >= Player2.X + Texture.Width && Position.X <= Player2.X + Texture2.Width ||
                Position.Y <= Player1.Y + Texture1.Height && Position.X >= Player2.X + Texture.Width && Position.X >= Player1.X + Texture.Width && Position.X <= Player1.X + Texture1.Width)
                {
                    Velocity.Y *= -1;
                    wallCollider.Play();
                }

            if (Position.Y >= Game1.ScreenHeight - Texture.Height || Position.Y <= 0)
                Velocity.Y *= -1;
        }
    }
}
