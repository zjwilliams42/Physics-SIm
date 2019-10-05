using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Physics_Sim
{
    namespace Graphics
    {
        public class Vector
        {
            Vector3 components;
            Vector3 origin;

            public Vector(float magnitude, float direction)
            {
                // if no origin is specified, it is assumed to be (0, 0, 0)
                origin = new Vector3(0, 0, 0);

                // the direction provided is assumed the be a degree offset from the +x axis.
                float x = magnitude * (float)System.Math.Cos(System.Math.PI * direction / 180.0);
                float y = magnitude * (float)System.Math.Sin(System.Math.PI * direction / 180.0);
                components = new Vector3(x, y, 0);
            }

            public Vector(float magnitude, float direction, Vector3 origin)
            {
                this.origin = origin;

                float x = magnitude * (float)System.Math.Cos(System.Math.PI * direction / 180.0);
                float y = magnitude * (float)System.Math.Sin(System.Math.PI * direction / 180.0);
                components = new Vector3(x, y, 0);
            }

            public Vector(Vector3 origin, float magnitude, float direction)
            {
                this.origin = origin;

                float x = magnitude * (float)System.Math.Cos(System.Math.PI * direction / 180.0);
                float y = magnitude * (float)System.Math.Sin(System.Math.PI * direction / 180.0);
                components = new Vector3(x, y, 0) + this.origin;
            }

            public Vector(Vector3 origin, Vector3 components)
            {
                this.origin = origin;

                this.components = components;
            }

            public Vector Add(Vector b)
            {
                Vector3 components = this.components + b.Components();
                return new Vector(origin, components);
            }

            public Vector3 Origin() { return origin; }
            public Vector3 Components() { return components; }

            public static Vector operator +(Vector a, Vector b)
            {
                return a.Add(b);
            }
        }
    }
}
