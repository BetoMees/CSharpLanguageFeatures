using CSharpLanguageFeatures.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLanguageFeatures.PatternMatching
{
    [Show]
    internal class PositionalPatterns
    {
        static decimal GetGroupTicketPriceDiscount(int groupSize, DateTime visitDate)
            => (groupSize, visitDate.DayOfWeek) switch
            {
                (_, DayOfWeek.Saturday or DayOfWeek.Sunday) => 0.0m,
                ( >= 5 and < 10, DayOfWeek.Monday) => 0.20m,
                ( >= 10, DayOfWeek.Monday) => 0.30m,
                ( >= 5 and < 10, _) => 0.12m,
                ( >= 10, _) => 0.15m,
                ( <= 0, _) => throw new ArgumentException("Group size must be positive"),
                _ => 0.0m
            };

        readonly struct Point
        {
            public int X { get; }
            public int Y { get; }
            public Point(int x, int y) => (X, Y) = (x, y);
            public void Deconstruct(out int x, out int y) => (x, y) = (X, Y);
        }

        static string Classify(Point point) => point switch
        {
            ( > 0, > 0) => "Upper right quadrant",
            ( < 0, > 0) => "Upper left quadrant",
            ( > 0, < 0) => "Lower right quadrant",
            ( < 0, < 0) => "Lower left quadrant",
            _ => "Just a point",
        };

        public static void Main()
        {
            (int, DateTime)[] TestDiscountData = new[]{
                (4, new DateTime(2022, 9, 3)),
                (7, new DateTime(2023, 2, 6)),
                (20, new DateTime(2023, 4, 17)),
                (15, new DateTime(2023, 8, 8)),
                (9, new DateTime(2023, 8, 9)),
            };

            foreach ((var size, var date) in TestDiscountData)
            {
                decimal discount = GetGroupTicketPriceDiscount(size, date);
                Console.WriteLine($"The discount for a {size} people group on {date:ddd, MMM, d} is {discount}");
            }

            Point[] TestPointData = new[]
            {
                new Point(5,8),
                new Point(-2,7),
                new Point(1,-1),
                new Point(-2,-2),
            };

            foreach (Point p in TestPointData)
            {
                Console.WriteLine($"Point is {Classify(p)}");
            }
        }
    }
}
