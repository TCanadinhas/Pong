using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using WindowsGame1.Classes;

namespace WindowsGame1
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState oldState;
        SpriteFont font;
                
        public static int ScreenWidth;
        public static int ScreenHeight;

        const int PADDLE_OFFSET = 70;
        const float BALL_START_SPEED = 8f;

        public static SoundEffect wallCollider;
        public static SoundEffect playerCollider;

        Player player1;
        Player player2;

        Ball ball;

        string wins;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            ScreenWidth = GraphicsDevice.Viewport.Width;
            ScreenHeight = GraphicsDevice.Viewport.Height;
            oldState = Keyboard.GetState();

            player1 = new Player();
            player2 = new Player();
            
            ball = new Ball();

            

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            font = Content.Load<SpriteFont>("Content/font");

            player1.Texture = Content.Load<Texture2D>("Content/Paddle");
            player2.Texture = Content.Load<Texture2D>("Content/Paddle");

            player1.Position = new Vector2(PADDLE_OFFSET, ScreenHeight / 2 - player1.Texture.Height / 2);
            player2.Position = new Vector2(ScreenWidth - player2.Texture.Width - PADDLE_OFFSET, ScreenHeight / 2 - player2.Texture.Height / 2);

            ball.Texture = Content.Load<Texture2D>("Content/Ball");
            ball.Launch(BALL_START_SPEED);

            playerCollider = Content.Load<SoundEffect>("Content/PaddleBallCollision");
            wallCollider = Content.Load<SoundEffect>("Content/BallWallCollision");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.W) && player1.Position.Y > 0)
                player1.Position.Y += -5;
            if (keyboardState.IsKeyDown(Keys.S) && player1.Position.Y < ScreenHeight - player1.Texture.Height)
                player1.Position.Y += 5;
            if (keyboardState.IsKeyDown(Keys.Up) && player2.Position.Y > 0)
                player2.Position.Y += -5;
            if (keyboardState.IsKeyDown(Keys.Down) && player2.Position.Y < ScreenHeight - player2.Texture.Height)
                player2.Position.Y += 5;
           
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            ScreenWidth = GraphicsDevice.Viewport.Width;
            ScreenHeight = GraphicsDevice.Viewport.Height;

            ball.Move(ball.Velocity);
            ball.Collider2D(player1.Position, player1.Texture, player2.Position, player2.Texture, wallCollider, playerCollider);

            player1.Collider2D(player1.Position, player1.Texture);
            player2.Collider2D(player2.Position, player2.Texture);

            if (ball.Position.X >= ScreenWidth - ball.Texture.Width)
            {
                ball.Launch(BALL_START_SPEED);
                player1.score++;

                if (player1.score == 5)
                    wins = "player1";

            }
            if(ball.Position.X <= 0)
            {
                ball.Launch(BALL_START_SPEED);
                player2.score++;

                if (player2.score == 5)
                    wins = "player2";
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
                
            spriteBatch.DrawString(font, "Player 1: " + player1.score.ToString(), new Vector2(PADDLE_OFFSET + player1.Texture.Width + 20, PADDLE_OFFSET), Color.Cyan);
            spriteBatch.DrawString(font, "Player 2: " + player2.score, new Vector2(ScreenWidth - ((PADDLE_OFFSET * 3) + player2.Texture.Width), PADDLE_OFFSET), Color.Cyan);

            player1.Draw(spriteBatch);
            player2.Draw(spriteBatch);

            ball.Draw(spriteBatch);
            
            if (wins == "player1")
                    spriteBatch.DrawString(font, "Player 1 Won!", new Vector2(ScreenWidth / 2 - PADDLE_OFFSET, ScreenHeight / 2), Color.Cyan);
            else if (wins == "player2")
                    spriteBatch.DrawString(font, "Player 2 Won!", new Vector2(ScreenWidth / 2 - PADDLE_OFFSET, ScreenHeight / 2), Color.Cyan);
            
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}