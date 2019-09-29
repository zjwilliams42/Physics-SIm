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

            public CoordinateSystem(GraphicsDevice graphicsDevice, float[] dimensions)
            {
                this.graphicsDevice = graphicsDevice;
                this.dimensions = dimensions;
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
