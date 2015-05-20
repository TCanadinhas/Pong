using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1.Classes
{
    class Player : GameObject
    {
        public int score;
        public Vector2 velocity = new Vector2(0, 0);
        
        public void Collider2D(Vector2 Player, Texture2D Texture)
        {
            if (Player.Y <= 0) Player.Y = 0;
            if (Player.Y >= Game1.ScreenHeight - Texture.Height) Player.Y = Game1.ScreenHeight - Texture.Height;
        }
    }
}
