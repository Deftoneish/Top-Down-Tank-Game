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
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class MovableObject : Microsoft.Xna.Framework.DrawableGameComponent
    {
        GraphicsDeviceManager graphics;
        BasicEffect basicEffect;
        VertexPositionColor[] verticies;
        private float highestX, highestY, highestZ;
        private float lowestX, lowestY, lowestZ;
       

        public MovableObject(VertexPositionColor[] newVerticies, Game game, GraphicsDeviceManager graphics, BasicEffect basicEffect)
            : base(game)
        {
            verticies = new VertexPositionColor[newVerticies.Length];
            Array.Copy(newVerticies, verticies, newVerticies.Length);
      
            updateLowestHighest();
            Enabled = true;

            this.graphics = graphics;
            this.basicEffect = basicEffect;
        }

        public void updateLowestHighest()
        {
            highestX = 0;
            highestY = 0;
            highestZ = 0;
            foreach (VertexPositionColor vertex in verticies)
            {
                if (vertex.Position.X > highestX)
                {
                    highestX = vertex.Position.X;
                }
                if (vertex.Position.Y > highestY)
                {
                    highestY = vertex.Position.Y;
                }
                if (vertex.Position.Z > highestZ)
                {
                    highestZ = vertex.Position.Z;
                }
            }
            lowestX = highestX;
            lowestY = highestY;
            lowestZ = highestZ;
            foreach (VertexPositionColor vertex in verticies)
            {
                if (vertex.Position.X < lowestX)
                {
                    lowestX = vertex.Position.X;
                }
                if (vertex.Position.Y < lowestY)
                {
                    lowestY = vertex.Position.Y;
                }
                if (vertex.Position.Z < lowestZ)
                {
                    lowestZ = vertex.Position.Z;
                }
            }

        }
        private void move(Vector3 amount)
        {

            int maxX = graphics.PreferredBackBufferWidth;
            int maxY = graphics.PreferredBackBufferHeight;
            int maxZ = 0;
            if (amount.X > 0)
            {
     
                if (maxX > 0 && (highestX + amount.X) > maxX)
                {
                    amount.X = (maxX-highestX);
                }
            }
            else if (amount.X < 0)
            {
                if ((lowestX + amount.X) < 0)
                {
                    amount.X -= (lowestX + amount.X);
                }
            }
            if (amount.Y > 0)
            {
                if (maxY > 0 && highestY + amount.Y > maxY)
                {
                    amount.Y = maxY - highestY;
                }
            }
            else if (amount.Y < 0)
            {
                if (lowestY + amount.Y < 0)
                {
                    amount.Y -= (lowestY + amount.Y);
                }
            }

            if (amount.Z > 0)
            {
                if (maxZ > 0 && highestZ + amount.Z > maxZ)
                {
                    amount.Z = (maxZ - highestZ);
                }
            }
            else if (amount.Z < 0)
            {
                if (lowestZ + amount.Z < 0)
                {
                    amount.Z -= (lowestZ + amount.Z);
                }
            }


            for (int i = 0; i < verticies.Length; i++)
            {
                verticies[i].Position += amount;
            }

            updateLowestHighest();
        }
        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            Vector3 change;

            if (Keyboard.GetState().IsKeyDown(Keys.Left) && !Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                change.X = -5;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right) && !Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                change.X = 5;
            }
            else
            {
                change.X = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up) && !Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                change.Y = -5;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down) && !Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                change.Y = 5;
            }
            else
            {
                change.Y = 0;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.PageDown) && !Keyboard.GetState().IsKeyDown(Keys.PageUp))
            {
                change.Z = 1;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.PageUp) && !Keyboard.GetState().IsKeyDown(Keys.PageDown))
            {
                change.Z = -1;
            }
            else
            {
                change.Z = 0;
            }

            if(change.X != 0 || change.Y != 0 || change.Z != 0)
                move(change);
           
        }
        public override void Draw(GameTime gameTime)
        {
            basicEffect.CurrentTechnique.Passes[0].Apply();
            graphics.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, verticies, 0, verticies.Length-2);
            
        }

    }
}
