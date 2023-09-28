﻿using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

// DIC!!! (DiscordIconChanger)

namespace DiscordIconChanger
{
    internal class DIC
    {
        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        public static extern int SHChangeNotify(int wEventId, int uFlags, IntPtr dwItem1, IntPtr dwItem2);

        public static void Perform(string discordRoot, string discordAppDir, string icoPath)
        {
            string shortcutPath = "C:\\Users\\" + Environment.UserName + "\\AppData\\Roaming\\Microsoft\\Windows\\Start Menu\\Programs\\Discord Inc\\Discord.lnk";
            if(!Directory.Exists("C:\\Users\\" + Environment.UserName + "\\AppData\\Roaming\\Microsoft\\Windows\\Start Menu\\Programs\\Discord Inc"))
                Directory.CreateDirectory("C:\\Users\\" + Environment.UserName + "\\AppData\\Roaming\\Microsoft\\Windows\\Start Menu\\Programs\\Discord Inc");
            string[] appIcoPaths = { discordRoot + "\\app.ico", discordAppDir + "\\app.ico" };

            Console.WriteLine("Replacing old icon files...");
            foreach (var appIco in appIcoPaths)
            {
                System.IO.File.Move(appIco, Path.GetDirectoryName(appIco) + "\\old_appIco.ico", true);
                System.IO.File.Copy(icoPath, appIco, true);
            }

            // Create a Shell object
            WshShell shell = new WshShell();

            // Load the existing shortcut
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);

            // Set the new icon path
            shortcut.IconLocation = appIcoPaths[0];
            shortcut.TargetPath = discordRoot + "\\Update.exe --processStart Discord.exe";

            Console.WriteLine("Creating new Shortcut");
            // Save the shortcut
            shortcut.Save();

            Console.WriteLine("Copying Shortcut...");
            System.IO.File.Copy(shortcutPath, "C:\\users\\" + Environment.UserName + "\\desktop\\Discord.lnk", true);

            Console.WriteLine("Notifying Shell about Change");
            // Notify the shell about the change
            SHChangeNotify(0x08000000, 0x0000, IntPtr.Zero, IntPtr.Zero);
        }
    }
}