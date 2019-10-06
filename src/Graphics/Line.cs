using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;
using Physics_Sim.Equations;

namespace Physics_Sim
{
    namespace Graphics
    {
        public class Line
        {
            private List<Equation> equations;

            public Line(Equation equation)
            {
                equations = new List<Equation>();
                equations.Add(equation);
            }

            public Line(Equation[] equations)
            {
                this.equations = new List<Equation>();

                foreach (Equation equation in equations)
                {
                    this.equations.Add(equation);
                }
            }

            public Vector3[] GetPoints(Vector3 min, Vector3 max, Vector3 step)
            {
                List<Vector3> points = new List<Vector3>();
                for (float i = min.X; Math.Abs(i) <= Math.Abs(max.X); i += step.X)
                {
                    Vector3 point = new Vector3(i, this.Solve(i), 0);
                    if (point.Y > min.Y && point.Y < max.Y)
                    {
                        if (points.Count != 0) { points.Add(point); }
                        points.Add(point);
                    }
                }
                return points.ToArray();
            }

            private float Solve(float x)
            {
                float result = 0;
                foreach (Equation equation in equations)
                {
                    result = equation.Solve(x);
                }
                return result;
            }

        }
    }
}
