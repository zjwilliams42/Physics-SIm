using System;

namespace Physics_Sim
{
    namespace Equations
    {
        class QuadraticEquation : Equation
        {
            private float[] constants;

            public QuadraticEquation(float[] constants)
            {
                this.constants = constants;
            }

            public float Solve(float x)
            {
                int power = 0;
                float result = 0;
                foreach (float constant in constants)
                {
                    result += constant * (float)Math.Pow(x, power++);
                }
                return result;
            }
        }
    }
}