namespace CSharpLanguageFeatures.General
{
    class DeconstructCustom
    {
        private class Planet
        {
            public string Name { get; set; }
            public int Radius { get; set; }
            public int MoonCount { get; set; }
            public int DistanceFromSunKm { get; set; }
            public Planet(string name, int radius, int moonCount, int distanceFromSunKm)
            {
                Name = name;
                Radius = radius;
                MoonCount = moonCount;
                DistanceFromSunKm = distanceFromSunKm;
            }
            public void Deconstruct(out string name, out int moons)
            {
                name = Name;
                moons = MoonCount;
            }
            public void Deconstruct(out string name, out int moons, out int radius)
            {
                name = Name;
                moons = MoonCount;
                radius = Radius;
            }
        }

        public static void Main()
        {
            Planet Earth = new Planet("Earth", 6371, 1, 150_980_000);
            Planet Venus = new Planet("Venus", 6371, 1, 150_980_000);

            var (name, moons) = Earth;
            Console.WriteLine($"Name: {name}, Moons: {moons}");

            (name, moons, var radius) = Venus;
            Console.WriteLine($"Name: {name}, Moons: {moons}, Radius: {radius}");

        }
    }
}
