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

            private void DrawQuadrent (BasicEffect effect, int x, int y, int z, bool grid = false)
            {

                VertexPositionColor[] vectorVertices;
                vectorVertices = new VertexPositionColor[Math.Abs(x) * 4 * 3 + 6];
                
                if (!grid)
                { 
                    vectorVertices[0] = new VertexPositionColor( new Vector3(0, 0, 0), Color.Black );
                    vectorVertices[1] = new VertexPositionColor( new Vector3(x, 0, 0), Color.Black );
                    vectorVertices[2] = new VertexPositionColor( new Vector3(0, 0, 0), Color.Black );
                    vectorVertices[3] = new VertexPositionColor( new Vector3(0, y, 0), Color.Black );
                    vectorVertices[4] = new VertexPositionColor( new Vector3(0, 0, 0), Color.Black );
                    vectorVertices[5] = new VertexPositionColor( new Vector3(0, 0, z), Color.Black );

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
                else
                {
                    int j = 0;
                    int i = 0;

                    int x_sign = (x >= 0) ? 1 : -1;
                    int y_sign = (y >= 0) ? 1 : -1;
                    int z_sign = (z >= 0) ? 1 : -1;
                    for (i =x_sign; Math.Abs(i) <= Math.Abs(x); i+=x_sign)
                    {
                        // x-z axis
                        vectorVertices[j++] = new VertexPositionColor( new Vector3(i, 0, 0), Color.LightBlue );
                        vectorVertices[j++] = new VertexPositionColor( new Vector3(i, 0, z), Color.LightBlue );
                        vectorVertices[j++] = new VertexPositionColor( 
                            new Vector3(0, 0, z_sign * Math.Abs(i)), Color.LightBlue );
                        vectorVertices[j++] = new VertexPositionColor( 
                            new Vector3(x_sign * Math.Abs(z), 0, z_sign * Math.Abs(i)), Color.LightBlue );

                        // x-y axis
                        vectorVertices[j++] = new VertexPositionColor( 
                            new Vector3(0, y_sign * Math.Abs(i), 0), Color.LightBlue );
                        vectorVertices[j++] = new VertexPositionColor( 
                            new Vector3(0, y_sign * Math.Abs(i), z), Color.LightBlue );
                        vectorVertices[j++] = new VertexPositionColor(
                            new Vector3(0, 0, z_sign * Math.Abs(i)), Color.LightBlue );
                        vectorVertices[j++] = new VertexPositionColor( 
                            new Vector3(0, y_sign * Math.Abs(z), z_sign * Math.Abs(i)), Color.LightBlue );

                        // z-y axis
                        vectorVertices[j++] = new VertexPositionColor( 
                            new Vector3(0, y_sign * Math.Abs(i), 0), Color.LightBlue );
                        vectorVertices[j++] = new VertexPositionColor( 
                            new Vector3(x, y_sign * Math.Abs(i), 0), Color.LightBlue );
                        vectorVertices[j++] = new VertexPositionColor( new Vector3(i, 0, 0), Color.LightBlue );
                        vectorVertices[j++] = new VertexPositionColor( 
                            new Vector3(i, y_sign * Math.Abs(x), 0), Color.LightBlue );
                    }

                    vectorVertices[j++] = new VertexPositionColor( new Vector3(0, 0, 0), Color.Black );
                    vectorVertices[j++] = new VertexPositionColor( new Vector3(x, 0, 0), Color.Black );
                    vectorVertices[j++] = new VertexPositionColor( new Vector3(0, 0, 0), Color.Black );
                    vectorVertices[j++] = new VertexPositionColor( new Vector3(0, y, 0), Color.Black );
                    vectorVertices[j++] = new VertexPositionColor( new Vector3(0, 0, 0), Color.Black );
                    vectorVertices[j++] = new VertexPositionColor( new Vector3(0, 0, z), Color.Black );

                    foreach (var pass in effect.CurrentTechnique.Passes)
                    {
                        pass.Apply ();

                        graphics.GraphicsDevice.DrawUserPrimitives (
                            PrimitiveType.LineList,
                            vectorVertices,
                            0,
                            Math.Abs(x) * 2 * 3 + 3
                        );
                    }
                }

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
                
                DrawQuadrent(effect, 5, 5, 5);
                DrawQuadrent(effect, 5, 5, -5);
                DrawQuadrent(effect, 5, -5, 5);
                DrawQuadrent(effect, 5, -5, -5);
                DrawQuadrent(effect, -5, 5, 5);
                DrawQuadrent(effect, -5, 5, -5);
                DrawQuadrent(effect, -5, -5, 5);
                DrawQuadrent(effect, -5, 5, -5);
                
            }
        }
    }
}
