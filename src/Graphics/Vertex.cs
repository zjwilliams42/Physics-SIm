using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Physics_Sim
{
    namespace Graphics
    {
        public class Vertex
        {
            double[] components;
            public Vertex(double x, double y, double z)
            {
                components = new double[4];
                components[0] = x;
                components[1] = y;
                components[2] = z;
                components[3] = 1.0;
            }
        }
    }
}
