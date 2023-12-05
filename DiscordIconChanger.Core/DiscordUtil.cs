using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordIconChanger.Core
{
    public struct DiscordLocation
    {
        public string Root;
        public string App;
    }

    public class DiscordUtil
    {
        public static DiscordLocation LocateDiscord()
        {
            Console.WriteLine("Locating Discord...");
            string discordRoot = "C:\\Users\\" + Environment.UserName + "\\AppData\\Local\\Discord";

            string discordAppDir = "";
            foreach (var dir in Directory.GetDirectories(discordRoot))
            {
                if (Path.GetFileName(dir).StartsWith("app-"))
                {
                    discordAppDir = dir;
                    break;
                }
            }

            if (string.IsNullOrWhiteSpace(discordAppDir) || !Directory.Exists(discordRoot))
                throw new("Discord not found.");

            return new DiscordLocation { App = discordAppDir, Root = discordRoot };
        }
    }
}
