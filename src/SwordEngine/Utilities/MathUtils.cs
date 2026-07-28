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
        /// <summary>
        /// Method to calculate the hypotenuse of a right triangle given the lengths of the other two sides.
        /// </summary>
        /// <param name="side1">The first side of the triangle.</param>
        /// <param name="side2">The second side of the triangle.</param>
        /// <returns>The length of the hypotenuse.</returns>
        public static double CalculateHypotenuse(double side1, double side2)
        {
            double hypotenuseSquared = System.Math.Pow(side1, 2) + System.Math.Pow(side2, 2);
            return System.Math.Sqrt(hypotenuseSquared);
        }
    }
}
