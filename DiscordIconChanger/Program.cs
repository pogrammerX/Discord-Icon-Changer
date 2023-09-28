using DiscordIconChanger;
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

        Console.WriteLine("Locating Discord...");
        string discordRoot = "C:\\Users\\" + Environment.UserName + "\\AppData\\Local\\Discord";

        string discordAppDir = "";
        foreach (var dir in Directory.GetDirectories(discordRoot))
        {
            if (Path.GetFileName(dir).StartsWith("app-"))
                discordAppDir = dir;
        }

        if(string.IsNullOrWhiteSpace(discordAppDir))
        {
            Console.WriteLine("Discord not found!");
            Console.WriteLine("We've searched in " + discordRoot + " but didn't find it, have you installed Discord?");
            return;
        }

        if (args[0] == "--restore")
        {
            File.WriteAllBytes(Environment.CurrentDirectory + "\\normal.ico", DiscordIconChanger.Properties.Resources.normal);
            DIC.Perform(discordRoot, discordAppDir, Environment.CurrentDirectory + "\\normal.ico");
            File.Delete(Environment.CurrentDirectory + "\\normal.ico");
            Console.WriteLine("Restored!");
            return;
        }

        Console.WriteLine("Changing Icon...");
        DIC.Perform(discordRoot, discordAppDir, args[0]);
        Console.WriteLine("Finished!");
    }
}