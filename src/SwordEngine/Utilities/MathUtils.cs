using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordEngine.Utilities
{
    /// <summary>
    /// Used for basic math equations for game engine.
    /// </summary>
    public class MathUtils
    {
        public static double CalculateHypotenuse(double side1, double side2)
        {
            double hypotenuseSquared = System.Math.Pow(side1, 2) + System.Math.Pow(side2, 2);
            return System.Math.Sqrt(hypotenuseSquared);
        }
    }
}
