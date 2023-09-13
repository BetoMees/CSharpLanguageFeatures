using CSharpLanguageFeatures.Utils;

namespace CSharpLanguageFeatures.General
{
    [Show]
    class IndexAndRanges
    {
        public static void Main()
        {

            string[] words = new string[]{
                "Some","list","of", "words", "without", "any", "context"
            };

            // The index operator provides access to array elements
            Console.WriteLine(words[0]);

            // The index-from-end operator indexes from the end of a sequence
            Console.WriteLine(words[^1]);

            // The range operator (..) defines a range
            Console.WriteLine(string.Join(" ", words[0..3]));

            // Works with variable as well

            Index index = ^2;
            Console.WriteLine(words[index]);

            Range range = 2..index;
            Console.WriteLine(string.Join(" ", words[range]));
        }
    }
}
