using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Physics_Sim
{
    namespace Graphics
    {
        public class CoordinateSystem
        {
            GraphicsDeviceManager graphics;

            Microsoft.Xna.Framework.Matrix coordinateSystem;

            public CoordinateSystem(GraphicsDeviceManager graphics)
            {
                this.graphics = graphics;

                coordinateSystem = new Microsoft.Xna.Framework.Matrix(
                    1, 0, 0, 0,
                    0, 1, 0, 0,
                    0, 0, 1, 0,
                    0, 0, 0, 1
                );
            }

            public void render()
            {
                BasicEffect effect = new BasicEffect(graphics.GraphicsDevice);
                effect.VertexColorEnabled = true;

                Vector3 cameraPosition = new Vector3(25, 25, 25);
                Vector3 cameraLookAtVector = Vector3.Zero;
                Vector3 cameraUpVector = Vector3.UnitY;
                effect.View = Matrix.CreateLookAt(cameraPosition, cameraLookAtVector, cameraUpVector);
            
                float aspectRatio = graphics.PreferredBackBufferWidth / (float) graphics.PreferredBackBufferHeight;
                float fieldOfView = 0.50f;
                float nearClipPlane = 1;
                float farClipPlane = 200;
                effect.Projection = Matrix.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, nearClipPlane, farClipPlane);
                
                //int width = graphics.GraphicsDevice.Viewport.Width;
                //int height = graphics.GraphicsDevice.Viewport.Height;
                //float nearClipPlane = 1;
                //float farClipPlane = 200;
                //effect.Projection = Matrix.CreateOrthographic(width, height, nearClipPlane, farClipPlane);

                VertexPositionColor[] vectorVertices;
                vectorVertices = new VertexPositionColor[6];
                vectorVertices[0].Position = new Vector3(-10, 0, 0);
                vectorVertices[1].Position = new Vector3(10, 0, 0);
                vectorVertices[2].Position = new Vector3(0, -10, 0);
                vectorVertices[3].Position = new Vector3(0, 10, 0);
                vectorVertices[4].Position = new Vector3(0, 0, -10);
                vectorVertices[5].Position = new Vector3(0, 0, 10);

                vectorVertices[0].Color = Color.Red;
                vectorVertices[1].Color = Color.Red;
                vectorVertices[2].Color = Color.Green;
                vectorVertices[3].Color = Color.Green;
                vectorVertices[4].Color = Color.Blue;
                vectorVertices[5].Color = Color.Blue;

                foreach (var pass in effect.CurrentTechnique.Passes)
                {
                    pass.Apply ();

                    graphics.GraphicsDevice.DrawUserPrimitives (
                        PrimitiveType.LineList,
                        vectorVertices,
                        0,
                        3
                    );
                }
            }
        }
    }
}
