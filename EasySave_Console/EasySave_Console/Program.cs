using EasySave_Console;
using EasySave_Console.Languages;
using System;
using System.Resources;

internal class Program
{
    private static void Main(string[] args)
    {
        string version = "1.0.0";
        string? menuNumber;
        string? LanguageNumber;

        Console.WriteLine("|------------------------------|");
        Console.WriteLine("|---- EasySave Console Mode ---|");
        Console.WriteLine("|------------------------------|");
        Console.WriteLine("Version: " + version + "\n");
        Console.WriteLine("What Language would you like to use?: ");
        Console.WriteLine("1 - English");
        Console.WriteLine("2 - Français");
        Console.WriteLine("\n");
        Console.Write("Options (1-2):");
        LanguageNumber = Console.ReadLine();
        Console.Clear();
        if (LanguageNumber != null && LanguageNumber == "1")
        {
            LangHelper.ChangeLanguage("en");
        }
        if (LanguageNumber != null && LanguageNumber == "2")
        {
            LangHelper.ChangeLanguage("de");
        }
        Console.WriteLine($"{LangHelper.GetString("Hello")} {LangHelper.GetString("Sir")}!");
        Console.WriteLine($"{LangHelper.GetString("Here are your options:")}");
        Console.WriteLine($"1 - MENU - {LangHelper.GetString("Saves")}");
        Console.WriteLine($"2 - MENU - {LangHelper.GetString("Backup")}");
        Console.WriteLine($"3 - MENU - {LangHelper.GetString("Settings")}");
        Console.WriteLine($"4 - MENU - {LangHelper.GetString("Languages")}");
        Console.WriteLine("\n");
        Console.Write("Options (1-4):");
        menuNumber = Console.ReadLine();
        if (menuNumber != null && menuNumber == "1")
        {
            Console.WriteLine("-- MENU - Save --");

        }


        Console.ReadKey();
    }
}