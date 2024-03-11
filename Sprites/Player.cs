using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame.Sprites
{
    internal class Player
    {
        Texture2D texture;
        Vector2 position;
        Vector2 velocity;
        float speed;

        public Player(Texture2D texture)
        {
            this.texture = texture;
            speed = 4;
        }

        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                velocity.Y -= 1;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                velocity.Y += 1;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                velocity.X += 1;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                velocity.X -= 1;
            }

            if (velocity != Vector2.Zero)
                velocity.Normalize();

            position += velocity * speed;
            velocity = Vector2.Zero;
        }
            
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

    }
}
