internal class Program
{
    private static void Main(string[] args)
    {
        string version = "1.0.0";
        string? menuNumber;

        Console.WriteLine("|------------------------------|");
        Console.WriteLine("|---- EasySave Console Mode ---|");
        Console.WriteLine("|------------------------------|");
        Console.WriteLine("Version: " + version + "\n");
        Console.WriteLine("Options :");
        Console.WriteLine("1 - MENU - Save");
        Console.WriteLine("2 - MENU - Backup");
        Console.WriteLine("3 - MENU - Settings");
        Console.WriteLine("\n");
        Console.Write("Options (1-3):");
        menuNumber = Console.ReadLine();
        if (menuNumber != null && menuNumber == "1") {
            Console.WriteLine("-- MENU - Save --");
        
        }






        Console.ReadKey();
    }
}