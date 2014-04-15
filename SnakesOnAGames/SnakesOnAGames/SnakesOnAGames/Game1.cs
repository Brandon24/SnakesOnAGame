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

namespace SnakesOnAGames
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Vector2 pellet = new Vector2(5, 5);
        List<Vector2> snake = new List<Vector2>();
        Texture2D pellettexture;
        Texture2D snaketexture;
        Vector2 velocity = new Vector2(0, -1);
        Random rand = new Random();
        Texture2D squareTexture;
        Rectangle currentSquare;
        int playerScore = 0;
        float timeRemaining = 0.0f;
        const float TimePerSquare = 0.75f;

        float snakeMovementTimer = 0f;
        float snakeMovementTime = 60f;
        


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
            snake.Add(new Vector2(40, 24));

            snaketexture = Content.Load<Texture2D>(@"snake");
            pellettexture = Content.Load<Texture2D>(@"pellet");
            


            // TODO: use this.Content to load your game content here
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();


            // TODO: Add your update logic here

            KeyboardState kb = Keyboard.GetState();

            if (kb.IsKeyDown(Keys.Up))
            {
                velocity = new Vector2(0, -1);
            }
            else if (kb.IsKeyDown(Keys.Down) && velocity.Y != -1)
            {
                velocity = new Vector2(0, 1);
            }
            else if (kb.IsKeyDown(Keys.Left) && velocity.X != 1)
            {
                velocity = new Vector2(-1, 0);
            }
            else if (kb.IsKeyDown(Keys.Right) && velocity.X != -1)
            {
                velocity = new Vector2(1, 0);
            }

            snake[0] += velocity;
            snakeMovementTimer += (float)gameTime.ElapsedGameTime.Milliseconds;
            if (snakeMovementTimer > snakeMovementTime)
            {
                for (int i = snake.Count - 1; i > 0; i--)
                {
                    snake[i] = snake[i - 1];
                }

                snake[0] += velocity;
                snakeMovementTimer = 0f;
            }

            if (snake[0] == pellet)
            {
                snake.Add(snake[0]);
                pellet.X = rand.Next(5, 48);
                pellet.Y = rand.Next(5, 28);
            }

            
            if (timeRemaining == 0.0f)
            {
                currentSquare = new Rectangle(
                rand.Next(0, this.Window.ClientBounds.Width - 25),
                rand.Next(0, this.Window.ClientBounds.Height - 25),
                25, 25);
                timeRemaining = TimePerSquare;
            }
            MouseState mouse = Mouse.GetState();
            if ((mouse.LeftButton == ButtonState.Pressed) &&
            (currentSquare.Contains(mouse.X, mouse.Y)))
            {
                playerScore++;
                timeRemaining = 0.0f;
            }
            timeRemaining = MathHelper.Max(0, timeRemaining -
            (float)gameTime.ElapsedGameTime.TotalSeconds);
            this.Window.Title = "Score : " + playerScore.ToString();

            



            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            for (int i = 0; i < snake.Count; i++)
            {                
                spriteBatch.Draw(snaketexture, snake[i] * 7, Color.Red);                
            }

            spriteBatch.Draw(snaketexture, pellet * 7, Color.White);

            spriteBatch.End();

            // TODO: Add your drawing code here
            
            base.Draw(gameTime);
            
        }
    }
}
