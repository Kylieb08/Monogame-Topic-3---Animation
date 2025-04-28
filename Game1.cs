using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Topic_3___Animation
{
    public class Game1 : Game
    {

        Random generator = new Random();

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D greyTribbleTexture, brownTribbleTexture, orangeTribbleTexture, creamTribbleTexture;
        Rectangle greyTribbleRect, brownTribbleRect, orangeTribbleRect, creamTribbleRect, window;
        Vector2 greyTribbleSpeed, brownTribbleSpeed, orangeTribbleSpeed, creamTribbleSpeed;
        int creamTribbleX, creamTribbleY, creamTribbleSpeedX = -3, creamTribbleSpeedY = -2, greyTribbleWidth = 100, greyTribbleHeight = 100;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            window = new Rectangle(0, 0, 800, 600);
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();



            greyTribbleRect = new Rectangle(300, 200, greyTribbleWidth, greyTribbleHeight);
            greyTribbleSpeed = new Vector2 (3, 0);

            brownTribbleRect = new Rectangle(400, 60, 75, 75);
            brownTribbleSpeed = new Vector2 (0, 2);

            orangeTribbleRect = new Rectangle(50, 490, 110, 110);
            orangeTribbleSpeed = new Vector2 (3, 2);

            creamTribbleRect = new Rectangle(600, 300, 90, 90);
            creamTribbleSpeed = new Vector2 (creamTribbleSpeedX, creamTribbleSpeedY);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            greyTribbleTexture = Content.Load<Texture2D>("tribbleGrey");
            brownTribbleTexture = Content.Load<Texture2D>("tribbleBrown");
            orangeTribbleTexture = Content.Load<Texture2D>("tribbleOrange");
            creamTribbleTexture = Content.Load<Texture2D>("tribbleCream");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //Grey tribble
            greyTribbleRect.X += (int)greyTribbleSpeed.X;
            greyTribbleRect.Y += (int)greyTribbleSpeed.Y;

            if (greyTribbleRect.X > window.Width)
            {
                greyTribbleHeight = generator.Next(25, 250);
                greyTribbleWidth = generator.Next(25, 250);
                greyTribbleRect.Width = greyTribbleWidth;
                greyTribbleRect.Height = greyTribbleHeight;
                greyTribbleRect.X = window.Left - greyTribbleRect.Width;
            }

            //Brown tribble
            brownTribbleRect.X += (int)brownTribbleSpeed.X;
            brownTribbleRect.Y += (int)brownTribbleSpeed.Y;

            if (brownTribbleRect.Y < window.Top || brownTribbleRect.Y + brownTribbleRect.Height > window.Bottom)
            {
                brownTribbleSpeed.Y *= -2;
            }

            if (brownTribbleSpeed.Y > 1000)
            {
                brownTribbleSpeed.Y = 2;
                brownTribbleRect.X = 400;
                brownTribbleRect.Y = 60;
            }

            //Orange tribble
            orangeTribbleRect.X += (int)orangeTribbleSpeed.X;
            orangeTribbleRect.Y += (int)orangeTribbleSpeed.Y;

            if (orangeTribbleRect.X + orangeTribbleRect.Width > window.Width || orangeTribbleRect.X < window.Left)
            {
                orangeTribbleSpeed.X *= -1;
            }

            if (orangeTribbleRect.Y < window.Top || orangeTribbleRect.Y + orangeTribbleRect.Height > window.Bottom)
            {
                orangeTribbleSpeed.Y *= -1;
            }

            //Cream tribble
            creamTribbleRect.X += (int)creamTribbleSpeed.X;
            creamTribbleRect.Y += (int)creamTribbleSpeed.Y;

            if (creamTribbleRect.X + creamTribbleRect.Width > window.Width || creamTribbleRect.X < window.Left)
            {
                creamTribbleX = generator.Next(1, 710);
                creamTribbleRect.X = creamTribbleX;
                creamTribbleY = generator.Next(1, 510);
                creamTribbleRect.Y = creamTribbleY;
                creamTribbleSpeedX = generator.Next(1, 6);
                creamTribbleSpeedY = generator.Next(1, 6);
                creamTribbleSpeed.X = creamTribbleSpeedX;
                creamTribbleSpeed.Y = creamTribbleSpeedY;
                creamTribbleSpeed.X *= -1;
            }

            if (creamTribbleRect.Y < window.Top || creamTribbleRect.Y + creamTribbleRect.Height > window.Bottom)
            {
                creamTribbleX = generator.Next(1, 710);
                creamTribbleRect.X = creamTribbleX;
                creamTribbleY = generator.Next(1, 510);
                creamTribbleRect.Y = creamTribbleY;
                creamTribbleSpeedX = generator.Next(1, 6);
                creamTribbleSpeedY = generator.Next(1, 6);
                creamTribbleSpeed.X = creamTribbleSpeedX;
                creamTribbleSpeed.Y = creamTribbleSpeedY;
                creamTribbleSpeed.Y *= -1;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.Draw(greyTribbleTexture, greyTribbleRect, Color.White);
            _spriteBatch.Draw(brownTribbleTexture, brownTribbleRect, Color.White);
            _spriteBatch.Draw(orangeTribbleTexture, orangeTribbleRect, Color.White);
            _spriteBatch.Draw(creamTribbleTexture, creamTribbleRect, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
