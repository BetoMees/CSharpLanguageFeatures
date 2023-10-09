using CSharpLanguageFeatures.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLanguageFeatures.Classes
{
    [Show]
    internal class Comparing
    {
        public static void Main()
        {
            Point2D p1 = new Point2D() { X = 10, Y = 20 };
            Point2D p2 = new Point2D() { X = 10, Y = 20 };
            Point2D p3 = null, p4 = null;

            Console.WriteLine($"----Override----");
            Console.WriteLine($"{p1} Equals {p2}"); // Point2D X:10 Y:20 Equals Point2D X:10 Y:20
            Console.WriteLine($"{p1.Equals(p2)}");  // True

            Console.WriteLine($"{p1} == {p2}");     // Point2D X:10 Y:20 == Point2D X:10 Y:20
            Console.WriteLine($"{p1 == p2}");       // True

            Console.WriteLine($"{p1} == {p2}");     // Point2D X:10 Y:20 == Point2D X:10 Y:20
            Console.WriteLine($"{p1 != p2}");       // False

            Console.WriteLine($"{p3} == {p4}");     //  ==
            Console.WriteLine($"{p3 == p4}");       // True


            Point2DRecord recordP1 = new Point2DRecord(X: 10, Y: 20) { };
            Point2DRecord recordP2 = new Point2DRecord(X: 10, Y: 20) { };
            Point2DRecord recordP3 = null, recordP4 = null;

            Console.WriteLine($"----Record----");
            Console.WriteLine($"{recordP1} Equals {recordP2}");     // Point2DRecord { X = 10, Y = 20 } Equals Point2DRecord { X = 10, Y = 20 }
            Console.WriteLine($"{recordP1.Equals(recordP2)}");      // True

            Console.WriteLine($"{recordP1} == {recordP2}");         // Point2DRecord { X = 10, Y = 20 } == Point2DRecord { X = 10, Y = 20 }
            Console.WriteLine($"{recordP1 == recordP2}");           // True

            Console.WriteLine($"{recordP1} != {recordP2}");         // Point2DRecord { X = 10, Y = 20 } != Point2DRecord { X = 10, Y = 20 }
            Console.WriteLine($"{recordP1 != recordP2}");           // False

            Console.WriteLine($"{recordP3} == {recordP4}");         //  ==
            Console.WriteLine($"{recordP3 == recordP4}");           // True

            Point2DRecord recordP5 = recordP2 with { Y = 30 };
            Console.WriteLine($"{recordP1} == {recordP5}");         // Point2DRecord { X = 10, Y = 20 } == Point2DRecord { X = 10, Y = 30 }
            Console.WriteLine($"{recordP1 == recordP5}");           // False

        }
        public record Point2DRecord(int X, int Y);
        public class Point2D : IEquatable<Point2D>
        {
            public int X;
            public int Y;
            public override bool Equals(object? obj) => Equals(obj as Point2D);
            public bool Equals(Point2D? p) => p != null && (X == p.X && Y == p.Y);
            public override int GetHashCode() => (X, Y).GetHashCode();
            public static bool operator ==(Point2D? p1, Point2D? p2)
            {
                if (p1 is null)
                {
                    if (p2 is null)
                    {
                        return true;
                    }
                    return false;
                }
                return p1.Equals(p2);
            }

            public static bool operator !=(Point2D? p1, Point2D? p2) => !(p1 == p2);

            public override string ToString()
            {
                return $"{GetType().Name} X:{X} Y:{Y}";
            }
        }
    }
}
