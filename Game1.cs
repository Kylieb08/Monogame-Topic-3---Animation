﻿using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Topic_3___Animation
{
    enum Screen 
    { 
        Intro, 
        TribbleYard,
        BlueScreen,
        End
    }

    public class Game1 : Game
    {

        Random generator = new Random();
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D greyTribbleTexture, brownTribbleTexture, orangeTribbleTexture, creamTribbleTexture, blueScreenTexture, tribbleIntroTexture, tribbleEndTexture;
        Rectangle greyTribbleRect, brownTribbleRect, orangeTribbleRect, creamTribbleRect, window, blueScreenRect;
        Vector2 greyTribbleSpeed, brownTribbleSpeed, orangeTribbleSpeed, creamTribbleSpeed;
        int creamTribbleX, creamTribbleY, creamTribbleSpeedX = -3, creamTribbleSpeedY = -2, greyTribbleWidth = 100, greyTribbleHeight = 100, blueScreenCount = 0, bluePercent;
        MouseState mouseState, prevMouseState;
        SpriteFont blueScreenCompleteFont, introScreenFont;
        Screen screen;
        float seconds;

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
            screen = Screen.Intro;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();

            //Rectangles
            greyTribbleRect = new Rectangle(300, 200, greyTribbleWidth, greyTribbleHeight);
            greyTribbleSpeed = new Vector2 (5, 0);

            brownTribbleRect = new Rectangle(400, 60, 75, 75);
            brownTribbleSpeed = new Vector2 (0, 2);

            orangeTribbleRect = new Rectangle(300, 490, 110, 110);
            orangeTribbleSpeed = new Vector2 (4, 4);

            creamTribbleRect = new Rectangle(600, 300, 90, 90);
            creamTribbleSpeed = new Vector2 (creamTribbleSpeedX, creamTribbleSpeedY);

            blueScreenRect = new Rectangle(0, 0, 800, 600);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //Tribbles
            greyTribbleTexture = Content.Load<Texture2D>("tribbleGrey");
            brownTribbleTexture = Content.Load<Texture2D>("tribbleBrown");
            orangeTribbleTexture = Content.Load<Texture2D>("tribbleOrange");
            creamTribbleTexture = Content.Load<Texture2D>("tribbleCream");
            blueScreenTexture = Content.Load<Texture2D>("Blue screen of death");

            //Fonts
            blueScreenCompleteFont = Content.Load<SpriteFont>("BlueScreenCountFont");
            introScreenFont = Content.Load<SpriteFont>("IntroScreen");

            //Screens
            tribbleIntroTexture = Content.Load<Texture2D>("tribbleIntro");
            tribbleEndTexture = Content.Load<Texture2D>("tribbleEnd");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();

            if (screen == Screen.Intro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                    screen = Screen.TribbleYard;
            }

            else if (screen == Screen.TribbleYard)
            {
                //Grey tribble
                greyTribbleRect.X += (int)greyTribbleSpeed.X;
                greyTribbleRect.Y += (int)greyTribbleSpeed.Y;

                if (greyTribbleRect.X > window.Width)
                {
                    greyTribbleHeight = generator.Next(15, 250);
                    greyTribbleWidth = generator.Next(25, 250);
                    greyTribbleRect.Width = greyTribbleWidth;
                    greyTribbleRect.Height = greyTribbleHeight;
                    greyTribbleRect.X = window.Left - greyTribbleRect.Width;
                }

                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    if (greyTribbleRect.Contains(mouseState.Position))
                    {
                        if (blueScreenCount >= 3)
                        {
                            screen = Screen.End;
                        }
                        else
                        {
                            screen = Screen.BlueScreen;
                            bluePercent = 0;
                            blueScreenCount += 1;
                            seconds = 0f;
                        }
                    }
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

                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    if (brownTribbleRect.Contains(mouseState.Position))
                    {
                        orangeTribbleRect.X = 300;
                        orangeTribbleRect.Y = 490;
                        orangeTribbleSpeed.X = 4;
                        orangeTribbleSpeed.Y = 4;
                    }
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

                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    if (orangeTribbleRect.Contains(mouseState.Position))
                    {
                        orangeTribbleRect.X = -200;
                        orangeTribbleRect.Y = -200;
                        orangeTribbleSpeed.X = 0;
                        orangeTribbleSpeed.Y = 0;
                    }
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
                    creamTribbleSpeedX = generator.Next(1, 10);
                    creamTribbleSpeedY = generator.Next(1, 10);
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
            }

            else if (screen == Screen.BlueScreen)
            {
                seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (seconds >= 1)
                {
                    bluePercent += generator.Next(2, 12);
                    seconds = 0;
                }

                if (bluePercent >= 100)
                {
                    screen = Screen.TribbleYard;
                }

            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkOrchid);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            //Intro Screen
            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(tribbleIntroTexture, window, Color.White);
                _spriteBatch.DrawString(introScreenFont, "Press Left Click To See The Tribbles", new Vector2(10, 10), Color.White);
            }

            //Tribble Yard
            else if (screen == Screen.TribbleYard)
            {
                //Drawing Tribbles
                _spriteBatch.Draw(greyTribbleTexture, greyTribbleRect, Color.White);
                _spriteBatch.Draw(brownTribbleTexture, brownTribbleRect, Color.White);
                _spriteBatch.Draw(orangeTribbleTexture, orangeTribbleRect, Color.White);
                _spriteBatch.Draw(creamTribbleTexture, creamTribbleRect, Color.White);

            }

            //Blue Screen
            else if (screen == Screen.BlueScreen)
            {
                _spriteBatch.Draw(blueScreenTexture, blueScreenRect, Color.White);
                _spriteBatch.DrawString(blueScreenCompleteFont, bluePercent + "", new Vector2(85, 343), Color.White);
            }

            //End Screen (game ends after three blue screens)
            else if (screen == Screen.End)
            {
                _spriteBatch.Draw(tribbleEndTexture, window, Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
