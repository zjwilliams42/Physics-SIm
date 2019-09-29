using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Physics_Sim
{
    namespace Graphics
    {
        public class Vector
        {
            double[] components;
            public Vector(float magnitude, float direction)
            {
                components = new double[2];
                components[0] = magnitude * System.Math.Cos(System.Math.PI * direction / 180.0);
                components[1] = magnitude * System.Math.Sin(System.Math.PI * direction / 180.0);
            }
        }
    }
}
