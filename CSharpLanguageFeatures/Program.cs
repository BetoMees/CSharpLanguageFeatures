using CSharpLanguageFeatures.Utils;
using System.Reflection;

List<Type> listExamples = new List<Type>();

#region methods
// Get the list of class from the current domain
Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
String Name = AppDomain.CurrentDomain.FriendlyName;
assemblies.ToList().ForEach(assembly =>
{
    if (assembly.FullName == $"{Name}, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")
    {
        foreach (Type type in assembly.GetTypes())
        {
            if ((type.FullName?.StartsWith($"{Name}.") ?? false) && type.IsDefined(typeof(ShowAttribute), false))
            {
                listExamples.Add(type);
            }
        }
    }
});

const int SECTION_SPACE = 50;
void Print(int pos)
{
    string section = "";
    Console.Clear();
    Console.WriteLine("Select one of the examples to execute or Esc to leave:");
    foreach (var (type, i) in listExamples.OrderBy(s => s.FullName).Select((value, i) => (value, i)))
    {
        // Ignore current domain and class name
        if (type.FullName?.Split(".")[1..^1] is string[] splited && splited.Length > 0)
        {
            var joinSection = String.Join(" ", splited);
            if (!joinSection.Equals(section))
            {
                section = joinSection;
                string strSpace = new string('-', (SECTION_SPACE - joinSection.Length) / 2);
                Console.WriteLine($"{strSpace + joinSection + strSpace}");
            }
        }
        Console.WriteLine($"{(pos == i ? "=>" : "  ")} {type.Name}");
    }
}
void Execute(int num)
{
    Console.Clear();
    try
    {
        MethodInfo? mainMethod = listExamples.ElementAt(num).GetMethod("Main");
        if (mainMethod != null)
        {
            mainMethod.Invoke(null, null);
        }
    }
    catch (ArgumentOutOfRangeException)
    {
        Console.WriteLine("Please select a valid options");
    }
    Console.WriteLine("Press any key to return");
    _ = Console.ReadKey();
}

int? MovePosition(ConsoleKey key, int pos, int length) => key switch
{
    ConsoleKey.UpArrow => --pos >= 0 ? pos : length,
    ConsoleKey.DownArrow => ++pos <= length ? pos : 0,
    ConsoleKey.Escape => null,
    _ => pos
};

#endregion


if (listExamples.Count == 0)
{
    Console.WriteLine("Sorry couldn't find any class on this project");
    return;
}

ConsoleKeyInfo outputKey;
int? pos = 0;
int listLength = listExamples.Count - 1;
do
{
    Print(pos ?? 0);
    outputKey = Console.ReadKey();
    if (outputKey.Key == ConsoleKey.Enter)
        Execute(pos ?? 0);
    else
        pos = MovePosition(outputKey.Key, pos ?? 0, listLength);
} while (pos != null);

