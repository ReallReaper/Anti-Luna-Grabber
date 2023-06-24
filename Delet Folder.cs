//anti luna folder and more
using System;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        string tempFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Temp");
        string[] deletedFolders = new string[0];

        Console.WriteLine("working...");

        while (true)
        {
            if (Directory.Exists(tempFolderPath))
            {
                string[] tempSubfolders = Directory.GetDirectories(tempFolderPath);

                foreach (string subfolder in tempSubfolders)
                {
                    // Use regex to check if the folder name matches the pattern of 7 to 14 alphanumeric characters
                    if (Regex.IsMatch(Path.GetFileName(subfolder), @"^[a-zA-Z0-9]{7,14}$"))
                    {
                        try
                        {
                            Directory.Delete(subfolder, true);
                            Console.WriteLine($"Deleted folder {subfolder}");
                            Array.Resize(ref deletedFolders, deletedFolders.Length + 1);
                            deletedFolders[deletedFolders.Length - 1] = subfolder;
                        }
                        catch (UnauthorizedAccessException ex)
                        {
                            //Console.WriteLine($"Access denied to folder {subfolder}: {ex.Message}");
                        }
                        catch (IOException ex)
                        {
                            //Console.WriteLine($"Failed to delete folder {subfolder}: {ex.Message}");
                        }
                    }
                }
            }

            // Save deleted folder names to a text file in the same location as the program
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DeletedFolders.txt");
            File.WriteAllLines(filePath, deletedFolders);

            System.Threading.Thread.Sleep(10);
        }
    }
}