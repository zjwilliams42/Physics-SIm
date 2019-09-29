using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Physics_Sim
{
    namespace Graphics
    {
        public class Matrix
        {
            Microsoft.Xna.Framework.Matrix matrix;

            public Matrix(int x, int y)
            {
                matrix = new Microsoft.Xna.Framework.Matrix(
                    1f, 0f, 0f, 0f,
                    0f, 1f, 0f, 0f,
                    0f, 0f, 1f, 0f,
                    0f, 0f, 0f, 1f
                );
            }

            public void set(int x, int y, float value)
            {
                matrix[x, y] = value;
            }

            public double get(int x, int y)
            {
                return matrix[x, y];
            }

            public static Matrix operator *(Matrix a, Matrix b)
            {
                return a * b;
            }
        }
    }
}
