using DiscordIconChanger.Core;
using System.Drawing;

class Prpgram
{
    static void Main(string[] args)
    {
        Console.WriteLine("DiscordIconChanger by pogrammerX (https://github.com/pogrammerX/discord-icon-changer)");

        if (args.Length != 1)
        {
            Console.WriteLine("Syntax Error!");
            Console.WriteLine("Usage:");
            Console.WriteLine("\tdiscordiconchanger <path to ico file>");
            Console.WriteLine("Or");
            Console.WriteLine("\tdiscordiconchanger --restore (Restores the default Discord Icon)");
            return;
        }

        DiscordLocation location;

        try
        {
            location = DiscordUtil.LocateDiscord();
        }catch
        {
            Console.WriteLine("Discord not found!");
            Console.WriteLine("Have you installed Discord?");
            return;
        }

        if (args[0] == "--restore")
        {
            File.WriteAllBytes(Environment.CurrentDirectory + "\\normal.ico", DiscordIconChanger.Properties.Resources.normal);
            DIC.Perform(location, Environment.CurrentDirectory + "\\normal.ico");
            File.Delete(Environment.CurrentDirectory + "\\normal.ico");
            Console.WriteLine("Restored!");
            return;
        }

        Console.WriteLine("Changing Icon...");
        DIC.Perform(location, args[0]);
        Console.WriteLine("Finished!");
    }
}