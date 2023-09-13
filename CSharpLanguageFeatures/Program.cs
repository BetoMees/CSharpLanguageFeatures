using System.Reflection;

Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

String Name = AppDomain.CurrentDomain.FriendlyName;

List<Type> listClass = new List<Type>();

// Get the list of class from the current domain
assemblies.ToList().ForEach(assembly =>
{
    if (assembly.FullName == $"{Name}, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")
    {
        foreach (Type type in assembly.GetTypes())
        {
            if (type.FullName?.StartsWith($"{Name}.") ?? false)
            {
                listClass.Add(type);
            }
        }
    }
});

if (listClass.Count == 0)
{
    Console.WriteLine("Sorry couldn't find any class on this project");
    return;
}

String? output;
do
{
    Console.WriteLine("Select one of the examples to execute or enter to leave:");
    foreach (var (type, i) in listClass.Select((value, i) => (value, i)))
    {
        Console.WriteLine($"{i} - {type.Name}");
    }
    output = Console.ReadLine();
    if (int.TryParse(output, out int num))
    {
        try
        {
            MethodInfo? mainMethod = listClass.ElementAt(num).GetMethod("Main");
            if (mainMethod != null)
            {
                mainMethod.Invoke(null, null);
            }
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Please select a valid options");
        }
    }

    Console.WriteLine(output);
} while (!String.IsNullOrEmpty(output));

