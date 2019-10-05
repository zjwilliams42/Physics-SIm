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

            private void DrawQuadrent(BasicEffect effect, int x, int y, int z)
            {

                int max = (Math.Abs(x) > Math.Abs(y)) ? Math.Abs(x) : Math.Abs(y);
                max = (max > Math.Abs(z)) ? max : Math.Abs(z);
                VertexPositionColor[] v = new VertexPositionColor[max * 12 + 6];

                int x_sign = (x >= 0) ? 1 : -1;
                int y_sign = (y >= 0) ? 1 : -1;
                int z_sign = (z >= 0) ? 1 : -1;

                bool xz_grid = true;
                bool xy_grid = false;
                bool yz_grid = false;

                int j = 0;
                float len = 0.05f;
                float x_pos, y_pos, z_pos = 0.0f;
                Color color = Color.Black;

                for (int i = x_sign; Math.Abs(i) <= Math.Abs(x); i += x_sign)
                {
                    x_pos = i;
                    y_pos = (yz_grid) ? y : y * len;
                    z_pos = (xz_grid) ? z : z * len;

                    // xz axis
                    color = (xz_grid) ? Color.LightBlue : Color.Black;
                    v[j++] = new VertexPositionColor(new Vector3(x_pos, 0, 0), color);
                    v[j++] = new VertexPositionColor(new Vector3(x_pos, 0, z_pos), color);

                    // yz axis
                    color = (yz_grid) ? Color.LightBlue : Color.Black;
                    v[j++] = new VertexPositionColor(new Vector3(x_pos, 0, 0), color);
                    v[j++] = new VertexPositionColor(new Vector3(x_pos, y_pos, 0), color);
                }

                for (int i = y_sign; Math.Abs(i) <= Math.Abs(y); i += y_sign)
                {
                    x_pos = (yz_grid) ? x : x * len;
                    y_pos = i;
                    z_pos = (xy_grid) ? z : z * len;

                    // xy axis
                    color = (xy_grid) ? Color.LightBlue : Color.Black;
                    v[j++] = new VertexPositionColor(new Vector3(0, y_pos, 0), color);
                    v[j++] = new VertexPositionColor(new Vector3(0, y_pos, z_pos), color);

                    // yz axis
                    color = (yz_grid) ? Color.LightBlue : Color.Black;
                    v[j++] = new VertexPositionColor(new Vector3(0, y_pos, 0), color);
                    v[j++] = new VertexPositionColor(new Vector3(x_pos, y_pos, 0), color);
                }

                for (int i = z_sign; Math.Abs(i) <= Math.Abs(z); i += z_sign)
                {
                    x_pos = (xz_grid) ? x : x * len;
                    y_pos = (xy_grid) ? y : y * len;
                    z_pos = i;

                    // xz axis
                    color = (xz_grid) ? Color.LightBlue : Color.Black;
                    v[j++] = new VertexPositionColor(new Vector3(0, 0, z_pos), color);
                    v[j++] = new VertexPositionColor(new Vector3(x_pos, 0, z_pos), color);

                    // xy axis
                    color = (xy_grid) ? Color.LightBlue : Color.Black;
                    v[j++] = new VertexPositionColor(new Vector3(0, 0, z_pos), color);
                    v[j++] = new VertexPositionColor(new Vector3(0, y_pos, z_pos), color);
                }

                v[j++] = new VertexPositionColor(new Vector3(0, 0, 0), Color.Black);
                v[j++] = new VertexPositionColor(new Vector3(x, 0, 0), Color.Black);
                v[j++] = new VertexPositionColor(new Vector3(0, 0, 0), Color.Black);
                v[j++] = new VertexPositionColor(new Vector3(0, y, 0), Color.Black);
                v[j++] = new VertexPositionColor(new Vector3(0, 0, 0), Color.Black);
                v[j++] = new VertexPositionColor(new Vector3(0, 0, z), Color.Black);

                foreach (var pass in effect.CurrentTechnique.Passes)
                {
                    pass.Apply();

                    graphics.GraphicsDevice.DrawUserPrimitives(
                        PrimitiveType.LineList,
                        v,
                        0,
                        max * 6 + 3
                    );
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

                float aspectRatio = graphics.PreferredBackBufferWidth / (float)graphics.PreferredBackBufferHeight;
                float fieldOfView = 0.50f;
                float nearClipPlane = 1;
                float farClipPlane = 200;
                effect.Projection = Matrix.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, nearClipPlane, farClipPlane);

                int x_min = -5;
                int x_max = 5;
                int y_min = -5;
                int y_max = 5;
                int z_min = -5;
                int z_max = 5;

                DrawQuadrent(effect, x_max, y_max, z_max);
                DrawQuadrent(effect, x_max, y_max, z_min);
                DrawQuadrent(effect, x_max, y_min, z_max);
                DrawQuadrent(effect, x_max, y_min, z_min);
                DrawQuadrent(effect, x_min, y_max, z_max);
                DrawQuadrent(effect, x_min, y_max, z_min);
                DrawQuadrent(effect, x_min, y_min, z_min);
                DrawQuadrent(effect, x_min, y_min, z_min);

            }
        }
    }
}
