using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Physics_Sim
{
    namespace Graphics
    {
        public class CoordinateSystem
        {
            private GraphicsDeviceManager graphics;


            private Vector3 cameraPosition;
            private float len;
            private bool xz_grid;
            private bool xy_grid;
            private bool yz_grid;
            private int[] x_range;
            private int[] y_range;
            private int[] z_range;

            public CoordinateSystem(GraphicsDeviceManager graphics)
            {
                this.graphics = graphics;

                // default camera is 3D
                cameraPosition = new Vector3(25, 25, 25);

                // length of the notches along the axis.
                len = 0.05f;

                // default settings for axis to show the grid on.
                xz_grid = true;
                xy_grid = false;
                yz_grid = false;

                // default settings for minimum size of the system.
                x_range = new int[2];
                y_range = new int[2];
                z_range = new int[2];
                x_range[0] = y_range[0] = z_range[0] = -5;
                x_range[1] = y_range[1] = z_range[1] = 5;
            }

            private void DrawQuadrent(BasicEffect effect, int x, int y, int z)
            {
                int max = Math.Max(Math.Max(Math.Abs(x), Math.Abs(y)), Math.Abs(z));
                VertexPositionColor[] v = new VertexPositionColor[max * 12 + 6];

                int x_sign = (x >= 0) ? 1 : -1;
                int y_sign = (y >= 0) ? 1 : -1;
                int z_sign = (z >= 0) ? 1 : -1;

                int j = 0;
                float x_pos, y_pos, z_pos = 0.0f;
                Color color = Color.Black;

                for (int i = x_sign; Math.Abs(i) <= Math.Abs(x); i += x_sign)
                {
                    x_pos = i;
                    y_pos = (xy_grid) ? y : y * len;
                    z_pos = (xz_grid) ? z : z * len;

                    // xz axis
                    color = (xz_grid) ? Color.LightBlue : Color.Black;
                    v[j++] = new VertexPositionColor(new Vector3(x_pos, 0, 0), color);
                    v[j++] = new VertexPositionColor(new Vector3(x_pos, 0, z_pos), color);

                    // yz axis
                    color = (xy_grid) ? Color.LightBlue : Color.Black;
                    v[j++] = new VertexPositionColor(new Vector3(x_pos, 0, 0), color);
                    v[j++] = new VertexPositionColor(new Vector3(x_pos, y_pos, 0), color);
                }

                for (int i = y_sign; Math.Abs(i) <= Math.Abs(y); i += y_sign)
                {
                    x_pos = (xy_grid) ? x : x * len;
                    y_pos = i;
                    z_pos = (yz_grid) ? z : z * len;

                    // xy axis
                    color = (yz_grid) ? Color.LightBlue : Color.Black;
                    v[j++] = new VertexPositionColor(new Vector3(0, y_pos, 0), color);
                    v[j++] = new VertexPositionColor(new Vector3(0, y_pos, z_pos), color);

                    // yz axis
                    color = (xy_grid) ? Color.LightBlue : Color.Black;
                    v[j++] = new VertexPositionColor(new Vector3(0, y_pos, 0), color);
                    v[j++] = new VertexPositionColor(new Vector3(x_pos, y_pos, 0), color);
                }

                for (int i = z_sign; Math.Abs(i) <= Math.Abs(z); i += z_sign)
                {
                    x_pos = (xz_grid) ? x : x * len;
                    y_pos = (yz_grid) ? y : y * len;
                    z_pos = i;

                    // xz axis
                    color = (xz_grid) ? Color.LightBlue : Color.Black;
                    v[j++] = new VertexPositionColor(new Vector3(0, 0, z_pos), color);
                    v[j++] = new VertexPositionColor(new Vector3(x_pos, 0, z_pos), color);

                    // xy axis
                    color = (yz_grid) ? Color.LightBlue : Color.Black;
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
                        PrimitiveType.LineList, v, 0,
                        max * 6 + 3
                    );
                }

            }

            public void render()
            {
                BasicEffect effect = new BasicEffect(graphics.GraphicsDevice);
                effect.VertexColorEnabled = true;

                Vector3 cameraLookAtVector = Vector3.Zero;
                Vector3 cameraUpVector = Vector3.UnitY;
                effect.View = Matrix.CreateLookAt(cameraPosition, cameraLookAtVector, cameraUpVector);

                float aspectRatio = graphics.PreferredBackBufferWidth / (float)graphics.PreferredBackBufferHeight;
                float fieldOfView = 0.50f;
                float nearClipPlane = 1;
                float farClipPlane = 200;
                effect.Projection = Matrix.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, nearClipPlane, farClipPlane);


                // change settings to 2D
                xz_grid = false;
                xy_grid = true;
                cameraPosition = new Vector3(0, 0, 25);



                DrawQuadrent(effect, x_range[1], y_range[1], z_range[1]);
                DrawQuadrent(effect, x_range[1], y_range[1], z_range[0]);
                DrawQuadrent(effect, x_range[1], y_range[0], z_range[1]);
                DrawQuadrent(effect, x_range[1], y_range[0], z_range[0]);
                DrawQuadrent(effect, x_range[0], y_range[1], z_range[1]);
                DrawQuadrent(effect, x_range[0], y_range[1], z_range[0]);
                DrawQuadrent(effect, x_range[0], y_range[0], z_range[1]);
                DrawQuadrent(effect, x_range[0], y_range[0], z_range[0]);



                // draw a vector
                VertexPositionColor[] v = new VertexPositionColor[6];

                Vector a = new Vector(5, 45);
                v[0] = new VertexPositionColor(a.Origin(), Color.Red);
                v[1] = new VertexPositionColor(a.Components(), Color.Red);


                Vector b = new Vector(2, 30, a.Components());
                v[2] = new VertexPositionColor(b.Origin(), Color.Green);
                v[3] = new VertexPositionColor(b.Components(), Color.Green);

                Vector c = a + b;
                v[4] = new VertexPositionColor(c.Origin(), Color.Blue);
                v[5] = new VertexPositionColor(c.Components(), Color.Blue);

                Console.Write("a: ");
                Console.WriteLine(a.Components());
                Console.Write("b: ");
                Console.WriteLine(b.Components());
                Console.Write("c: ");
                Console.WriteLine(c.Components());

                foreach (var pass in effect.CurrentTechnique.Passes)
                {
                    pass.Apply();

                    graphics.GraphicsDevice.DrawUserPrimitives(
                        PrimitiveType.LineList, v, 0,
                        3
                    );
                }



            }
        }
    }
}
