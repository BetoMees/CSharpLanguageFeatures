using CSharpLanguageFeatures.Utils;

namespace CSharpLanguageFeatures.General
{
    [Show]
    class DeconstructTuples
    {
        private static (decimal, decimal, decimal) GetValues(string name) => name switch
        {
            "first" => (51, 45, 20),
            "second" => (20, 10, 30),
            "third" => (22, 47, 55),
            "forth" or "last" => (17, 99, 45),
            _ => (12, 42, 87)
        };

        public static void Main()
        {
            // access value individually using the ItenN syntax
            var itemSyntax = GetValues("first");
            Console.WriteLine($"{itemSyntax.Item1}, {itemSyntax.Item2}, {itemSyntax.Item3}");

            // deconstructing the turples into variables 
            var (descVal1, descVal2, descVal3) = GetValues("second");
            Console.WriteLine($"{descVal1}, {descVal2}, {descVal3}");

            // ignoring some values
            var (_, _, notIgnored) = GetValues("last");
            Console.WriteLine($"{notIgnored}");

        }
    }
}
