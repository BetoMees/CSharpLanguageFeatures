using CSharpLanguageFeatures.Utils;
using System.Reflection;


public class Menu
{
    const int SECTION_SPACE = 50;
    int _pos = 0;
    List<Type> listExamples;
    int Length { get => listExamples.Count - 1; }
    public Menu()
    {
        listExamples = GetExampleTypes();

        if (listExamples.Count == 0)
        {
            Console.WriteLine("Sorry couldn't find any classes in this project");
            return;
        }

        RunExampleSelection();
    }

    List<Type> GetExampleTypes()
    {
        AssemblyName? assemblyName = Assembly.GetEntryAssembly()?.GetName();
        if (assemblyName == null)
        {
            Console.WriteLine("Error: Unable to retrieve the entry assembly.");
            return new List<Type>();
        }

        return AppDomain.CurrentDomain.GetAssemblies()
            .Where(assembly => assembly.FullName == assemblyName.FullName)
            .SelectMany(assembly => assembly.GetTypes()
                .Where(type => type.FullName?.StartsWith($"{assemblyName.Name}.") ?? false)
                .Where(type => type.IsDefined(typeof(ShowAttribute), false))
            )
            .OrderBy(type => type.FullName)
            .ToList();
    }
    void RunExampleSelection()
    {
        PrintExampleSelection(listExamples, _pos);
        ConsoleKeyInfo outputKey = Console.ReadKey();
        if (outputKey.Key == ConsoleKey.Enter)
        {
            ExecuteExample(listExamples[_pos]);
            RunExampleSelection();
        }
        else if(outputKey.Key != ConsoleKey.Escape)
        {
            _pos = MovePosition(outputKey.Key, _pos);
            RunExampleSelection();
        }
    }

    void PrintExampleSelection(List<Type> listExamples, int pos)
    {
        Console.Clear();
        Console.WriteLine("Select one of the examples to execute or Esc to leave:");
        string section = "";

        foreach (var (type, i) in listExamples.Select((value, i) => (value, i)))
        {
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

    void ExecuteExample(Type type)
    {
        Console.Clear();
        try
        {
            MethodInfo? mainMethod = type.GetMethod("Main");
            if (mainMethod != null && mainMethod.IsStatic)
            {
                mainMethod.Invoke(null, null);
            }
            else
            {
                Console.WriteLine("Please ensure that the Main method is both implemented and declared as public and static.");
            }
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Please select a valid option");
        }
        Console.WriteLine("Press any key to return");
        _ = Console.ReadKey();
    }
    int MovePosition(ConsoleKey key, int pos)
    {
        return key switch
        {
            ConsoleKey.UpArrow => --pos >= 0 ? pos : Length,
            ConsoleKey.DownArrow => ++pos <= Length ? pos : 0,
            _ => pos
        };
    }
}
