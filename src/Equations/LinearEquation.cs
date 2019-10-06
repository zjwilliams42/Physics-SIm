namespace Physics_Sim
{
    namespace Equations
    {
        class LinearEquation : Equation
        {
            private float m;
            private float b;

            public LinearEquation(float m, float b)
            {
                this.m = m;
                this.b = b;
            }

            public float Solve(float x)
            {
                return ((m * x) + b);
            }
        }
    }
}