

string getCurrentDirectory = Directory.GetCurrentDirectory();

string level1 = Path.Combine(getCurrentDirectory, "Levels", "Level1.txt");

if (File.Exists(level1))
    {
    string Level1Content = File.ReadAllText(level1);

    Console.WriteLine(Level1Content);
    }
else
    {
    Console.WriteLine("The file could not be found");
    }
