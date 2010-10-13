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

namespace TankGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        BasicEffect basicEffect;
        VertexPositionColor[] box;
        VertexPositionColor[] circle;
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
            basicEffect = new BasicEffect(graphics.GraphicsDevice);
            basicEffect.VertexColorEnabled = true;
            basicEffect.Projection = Matrix.CreateOrthographicOffCenter
               (0, graphics.GraphicsDevice.Viewport.Width,     // left, right
                graphics.GraphicsDevice.Viewport.Height, 0,    // bottom, top
                0, 1);                                         // near, far plane

            box = new VertexPositionColor[12];

            box[0] = new VertexPositionColor(new Vector3(10, 50, 0), Color.Red);
            box[1] = new VertexPositionColor(new Vector3(10, 10, 0), Color.Green);
            box[2] = new VertexPositionColor(new Vector3(50, 50, 0), Color.Blue);
            box[3] = new VertexPositionColor(new Vector3(50, 10, 0), Color.Red);
            box[4] = new VertexPositionColor(new Vector3(100, 50, 0), Color.Green);
            box[5] = new VertexPositionColor(new Vector3(100, 10, 0), Color.Blue);
            box[6] = new VertexPositionColor(new Vector3(150, 50, 0), Color.Red);
            box[7] = new VertexPositionColor(new Vector3(150, 10, 0), Color.Green);
            box[8] = new VertexPositionColor(new Vector3(200, 50, 0), Color.Blue);
            box[9] = new VertexPositionColor(new Vector3(200, 10, 0), Color.Red);
            box[10] = new VertexPositionColor(new Vector3(250, 50, 0), Color.Green);
            box[11] = new VertexPositionColor(new Vector3(200, 10, 0), Color.Blue);





            circle = new VertexPositionColor[100];
            for (int i = 0; i < 99; i++)
            {
                float angle = (float)(i / 100.0 * Math.PI * 2);
                circle[i].Position = new Vector3(400 + (float)Math.Cos(angle) * 100, 200 + (float)Math.Sin(angle) * 100, 0);
                circle[i].Color = Color.Green;
            }
            circle[99] = circle[0];
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

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary2
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            basicEffect.CurrentTechnique.Passes[0].Apply();
            Vector3 scale = new Vector3(50, 50, 0);
            VertexPositionColor[] temp = new VertexPositionColor[12];
            Array.Copy(box, temp, 12);
            for (int i = 0; i < 5; i++)
            {
                graphics.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, temp, 0, 10);
                for (int j = 1; j < 12; j++)
                {
                    temp[j].Position += scale;
                }
            }
            graphics.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineStrip, circle, 0, 99);
            base.Draw(gameTime);
        }
    }
}
