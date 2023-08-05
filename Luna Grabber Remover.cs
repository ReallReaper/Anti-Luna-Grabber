using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("What action would you like to perform?");
        Console.WriteLine("1. Remove file from 'startup'.");
        Console.WriteLine("2. Uninstall Discord.");
        Console.WriteLine("3. Install Discord from the official link.");
        Console.WriteLine("Enter the corresponding number:");

        string userInput = Console.ReadLine();
        if (userInput == "1")
        {
            string mostRecentFile = GetMostRecentFileInStartup();
            if (mostRecentFile != null)
            {
                Console.WriteLine("Most recent file in 'shell:startup' folder:");
                Console.WriteLine(mostRecentFile);

                Console.WriteLine("Do you want to remove this file? (Y/N)");
                string response = Console.ReadLine();
                if (response.Equals("Y", StringComparison.OrdinalIgnoreCase))
                {
                    RemoveFileFromStartup(mostRecentFile);
                    Console.WriteLine("File removed from 'startup'.");
                }
            }
            else
            {
                Console.WriteLine("No files found in 'shell:startup' folder.");
            }
        }
        else if (userInput == "2")
        {
            UninstallDiscord();
        }
        else if (userInput == "3")
        {
            string downloadUrl = "https://dl.discordapp.net/distro/app/stable/win/x86/1.0.9016/DiscordSetup.exe";
            string downloadPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "DiscordSetup.exe");
            DownloadAndInstallDiscord(downloadUrl, downloadPath);
        }
        else
        {
            Console.WriteLine("Invalid option.");
        }
    }

    public static string GetStartupFolderPath()
    {
        return Environment.GetFolderPath(Environment.SpecialFolder.Startup);
    }

    public static List<string> GetFilesInStartupFolder()
    {
        string startupFolderPath = GetStartupFolderPath();
        return Directory.GetFiles(startupFolderPath, "*.*").ToList();
    }

    public static string GetMostRecentFileInStartup()
    {
        List<string> filesInStartup = GetFilesInStartupFolder();
        if (filesInStartup.Count == 0)
        {
            return null;
        }

        string mostRecentFile = filesInStartup.OrderByDescending(f => File.GetCreationTime(f)).First();
        return mostRecentFile;
    }

    public static void RemoveFileFromStartup(string filePath)
    {
        File.Delete(filePath);
    }

    public static void UninstallDiscord()
    {
        string discordUninstallerPath = @"C:\Users\hamster\AppData\Local\Discord\Update.exe";

        if (System.IO.File.Exists(discordUninstallerPath))
        {
            try
            {
                Process.Start(discordUninstallerPath, "--uninstall");
                Console.WriteLine("Uninstalling Discord...");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error starting the Discord uninstaller: " + ex.Message);
            }
        }
        else
        {
            Console.WriteLine("Discord uninstaller executable not found.");
        }
    }

    public static void DownloadAndInstallDiscord(string downloadUrl, string downloadPath)
    {
        try
        {
            using (WebClient webClient = new WebClient())
            {
                Console.WriteLine("Downloading Discord from the official link...");
                webClient.DownloadFile(downloadUrl, downloadPath);
                Console.WriteLine("Download completed.");
            }

            Console.WriteLine("Starting Discord installation...");
            Process.Start(downloadPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error downloading or installing Discord: " + ex.Message);
        }
    }
}
