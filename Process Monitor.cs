//Process Monitor for Luna and more
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ProcessMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> previousProcesses = new List<int>();

            while (true)
            {
                List<int> currentProcesses = GetRunningProcesses();

                IEnumerable<int> newProcesses = currentProcesses.Except(previousProcesses);
                IEnumerable<int> closedProcesses = previousProcesses.Except(currentProcesses);

                foreach (int processId in newProcesses)
                {
                    Process newProcess = Process.GetProcessById(processId);
                    Console.WriteLine($"New process: {newProcess.ProcessName} (PID: {newProcess.Id})");
                }

                foreach (int processId in closedProcesses)
                {
                    Console.WriteLine($"Process closed: PID {processId}");
                }

                previousProcesses = currentProcesses;

                // Esperar un segundo antes de comprobar los procesos de nuevo
                System.Threading.Thread.Sleep(1000);
            }
        }

        static List<int> GetRunningProcesses()
        {
            List<int> processIds = new List<int>();

            foreach (Process process in Process.GetProcesses())
            {
                // Agregar el ID del proceso a la lista si no es el proceso actual y si tiene un nombre
                if (process.Id != Process.GetCurrentProcess().Id && !string.IsNullOrEmpty(process.ProcessName))
                {
                    processIds.Add(process.Id);
                }
            }

            return processIds;
        }
    }
}