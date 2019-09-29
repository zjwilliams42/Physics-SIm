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
            public Vector(float x, float y, float z, float a)
            {
                components = new double[4];
                components[0] = x;
                components[1] = y;
                components[2] = z;
                components[3] = a;
            }

            public double this[int index] { get => components[index]; }

            public static Vector operator *(Matrix matrix, Vector vector)
            {
                float x = (float) ( (matrix[0, 0] * vector[0]) + (matrix[0, 1] * vector[1]) + (matrix[0, 2] * vector[2]) + (matrix[0, 3] * vector[3]));
                float y = (float) ( (matrix[1, 0] * vector[0]) + (matrix[1, 1] * vector[1]) + (matrix[1, 2] * vector[2]) + (matrix[1, 3] * vector[3]));
                float z = (float) ( (matrix[2, 0] * vector[0]) + (matrix[2, 1] * vector[1]) + (matrix[2, 2] * vector[2]) + (matrix[2, 3] * vector[3]));
                float a = (float) ( (matrix[3, 0] * vector[0]) + (matrix[3, 1] * vector[1]) + (matrix[3, 2] * vector[2]) + (matrix[3, 3] * vector[3]));
                return new Vector(x, y, z, a);
            }

            public static Vector operator *(Vector vector, Matrix matrix) { return matrix * vector; }
        }
    }
}
