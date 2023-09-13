using CSharpLanguageFeatures.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLanguageFeatures.PatternMatching
{
    [Show]
    internal class BasicPattern
    {
        private static void dashedline(object o)
        {
            int l = 0;
            if (o.GetType() == typeof(string))
            {
                string s = (string)o;
                if (!int.TryParse(s, out l)){
                    l = 0;
                }
            }
            if (o.GetType() == typeof(int))
            {
                l = (int)o;
            }
            if (l > 0)
            {
                string str = new string('-', l);
                Console.WriteLine(str);
            }
        }
        private static void dashedlineWithIs(object o)
        {
            if(o is int l || (o is string s && int.TryParse(s, out l)) )
            {
                string str = new string('-', l);
                Console.WriteLine(str);
            }
        }

        private static bool isFriday(DateTime dt)
        {
            return dt is { DayOfWeek: DayOfWeek.Friday };
        }

        public static void Main()
        {
            dashedline(20);
            dashedline("30");
            dashedline(20.8);

            dashedlineWithIs(20);
            dashedlineWithIs("30");
            dashedlineWithIs(20.8);

            DateTime dt1 = DateTime.Now;
            DateTime dt2 = new DateTime(DateTime.Now.Year, 9, 15);
            Console.WriteLine($"Today { (isFriday(dt1) ? "is" : "isn't") } Friday");
            Console.WriteLine($"{dt2.ToString("yyyy/MM/dd")} { (isFriday(dt2) ? "is" : "isn't") } Friday");
        }
    }
}
