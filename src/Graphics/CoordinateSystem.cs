using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Physics_Sim
{
    namespace Graphics
    {
        public class CoordinateSystem
        {
            GraphicsDevice graphicsDevice;
            float[] dimensions;

            Microsoft.Xna.Framework.Matrix coordinateSystem;

            public CoordinateSystem(GraphicsDevice graphicsDevice, float[] dimensions)
            {
                this.graphicsDevice = graphicsDevice;
                this.dimensions = dimensions;

                coordinateSystem = new Microsoft.Xna.Framework.Matrix(
                    1, 0, 0, 0,
                    0, 1, 0, 0,
                    0, 0, 1, 0,
                    0, 0, 0, 1
                );
            }

            public CoordinateSystem localToWorld() { return null; } // P_world = P_local * M
            public CoordinateSystem worldToLocal() { return null; } // P_local = P_world * M^-1
            public Microsoft.Xna.Framework.Matrix worldToCamera()
            {
                // P_camera = P_world * M_world-to-camera
                int screen_width = 800;
                int screen_height = 480;
                int z_far = 10;
                int z_near = 0;

                float M_00 = ( 1 / screen_width );
                float M_11 = ( 1 / screen_height );
                float M_22 = ( -1 * 2 / (z_far - z_near) );
                float M_23 = ( -1 * (z_far + z_near) / (z_far - z_near) );

                Microsoft.Xna.Framework.Matrix M_worldtocamera = new Microsoft.Xna.Framework.Matrix(
                    M_00, 0, 0, 0,
                    0, M_11, 0, 0,
                    0, 0, M_22, M_23,
                    0, 0, 0, 1
                );

                return coordinateSystem * M_worldtocamera;
            }

            public Texture2D getGraphic()
            {
                if (dimensions.Length != 2) { return null; }
                int x = (int) (dimensions[0] + 0.5);
                int y = (int) (dimensions[1] + 0.5);

                Texture2D line = new Texture2D(graphicsDevice, x, y);
                Color[] data = new Color[x * y];

                //for (int i = 0; i < x * y; i++) { data[i] = Color.White; }

                int axis = x * (y - 1) / 2;
                for (int i = 0; i < x; i++) { data[axis + i] = Color.Red; }
                axis = (x - 1) / 2;
                for (int i = 0; i < y; i++) { data[axis + i * x] = Color.Blue; }
                
                line.SetData<Color>(data);
                return line;
            }
        }
    }
}
